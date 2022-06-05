using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    [SerializeField, Range(0.25f, 0.95f)]
    private float _hitPercentage = 0.25f;

    [SerializeField, Range(0.10f, 0.45f)]
    private float _timeBetweenTry = 0.05f;
    
    [SerializeField, ReadOnly]
    private float _currentTimeBetweenTries;

    [SerializeField]
    private TurnVariableSO _turnVariable;

    [SerializeField, ReadOnly]
    private WordObject _currentWordObject;
    
    [SerializeField]
    private BoolVariableSO _pauseVariable;

    [SerializeField]
    private BoolVariableSO _gameEndVariable;

    [SerializeField]
    private Transform _spawnPoint;

    [SerializeField, ReadOnly]
    private bool _enemyTurn;

    private StringBuilder _stringBuilder;
    private readonly string _alphabet = "abcdefghijklmnopqrstuvwxyz";

    private void Awake() 
    {
        _stringBuilder = new StringBuilder();    
    }

    private void Update() 
    {
        if (_gameEndVariable.RuntimeValue)
        {
            return;
        }
        if (_pauseVariable.RuntimeValue)
        {
            return;
        }
        if (_enemyTurn)
        {
            _currentTimeBetweenTries += Time.deltaTime;
            if (_currentTimeBetweenTries >= _timeBetweenTry)
            {
                ExecuteTry();
            }
        }
    }

    private void ExecuteTry()
    {
        string currentString = _stringBuilder.ToString();
        char letter = GetLetter(currentString.Length, _currentWordObject.Word.Value);
        if (_currentWordObject.Word.MatchStart(currentString + letter))
        {
            _stringBuilder.Append(letter);
            _currentWordObject.Event_OnInputValueChanged(_stringBuilder.ToString());
        }
        else
        {
            // TODO shake word
        }
        _currentTimeBetweenTries = 0f;
    }

    private char GetLetter(int currentIndex, string value)
    {
        float randomValue = Random.Range(0f, 1f);
        if (randomValue > _hitPercentage)
        {
            return _alphabet[Random.Range(0, _alphabet.Length)];
        }
        return value[currentIndex];
    }

    public void Event_TurnShift(object eventData)
    {
        switch (_turnVariable.RuntimeValue)
        {
            case Turn.Enemy:
            case Turn.Both:
                StartTurn();
                break;
        }
    }

    public void Event_EndTurn()
    {
        EndTurn();
    }

    private void EndTurn()
    {
        _enemyTurn = false;
        _currentTimeBetweenTries = 0f;
        _stringBuilder.Clear();
        if (_currentWordObject == null)
        {
            return;
        }
        _currentWordObject.GetComponent<Listener>().enabled = true;
        ReturnObjectToPool(_currentWordObject);
        _currentWordObject = null;
    }

    private void StartTurn()
    {
        _enemyTurn = true;
        _currentTimeBetweenTries = 0f;
        _stringBuilder.Clear();
        _currentWordObject = GetNewObject();
        _currentWordObject.transform.position = _spawnPoint.position;
        _currentWordObject.GetComponent<Listener>().enabled = false;
        _currentWordObject.Target = CharacterType.Player;
        _currentWordObject.gameObject.SetActive(true);
    }

    public void Event_OnWordFinished(object eventData)
    {
        if (_turnVariable.RuntimeValue != Turn.Enemy
            && _turnVariable.RuntimeValue != Turn.Both)
        {
            return;
        }

        if (eventData is object[] dataArray)
        {
            if (dataArray.Length != 3)
            {
                Debug.LogError("Wrong amount of data received.");
                return;
            }
            if (dataArray[0] is string value)
            {
                WordObject newWordObject = GetNewObject();
                ReturnObjectToPool(_currentWordObject);
                _currentWordObject = newWordObject;
                newWordObject.transform.position = _spawnPoint.position;
                newWordObject.GetComponent<Listener>().enabled = false;
                newWordObject.gameObject.SetActive(true);
                newWordObject.Target = CharacterType.Player;
                _stringBuilder.Clear();
                return;
            }
            Debug.LogError("Received value is not a string.");
        }
        Debug.LogError("Received value is not an array.");
    }

    private void ReturnObjectToPool(WordObject wordObject)
    {
        WordObjectsManager.ReturnToPool(wordObject);
    }

    private WordObject GetNewObject()
    {
        return WordObjectsManager.GetRandom();
    }
}

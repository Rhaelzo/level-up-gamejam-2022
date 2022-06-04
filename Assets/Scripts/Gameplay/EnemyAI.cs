using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    [SerializeField, Range(0.25f, 0.95f)]
    private float _hitPercentage = 0.25f;

    [SerializeField, Range(0.05f, 0.15f)]
    private float _timeBetweenTry = 0.05f;
    
    [SerializeField, ReadOnly]
    private float _currentTimeBetweenTries;

    [SerializeField]
    private TurnVariableSO _turnVariable;

    [SerializeField, ReadOnly]
    private WordObject _currentWordObject;
    
    [SerializeField]
    private BoolVariableSO _pauseVariable;

    [SerializeField, ReadOnly]
    private string _currentlyFormedWord;

    [SerializeField, ReadOnly]
    private bool _enemyTurn;

    private readonly string _alphabet = "abcdefghijklmnopqrstuvwxyz";

    private void Update() 
    {
        if (_pauseVariable.RuntimeValue)
        {
            return;
        }
        if (_enemyTurn)
        {
            _currentTimeBetweenTries += Time.deltaTime;
            if (_currentTimeBetweenTries >= _timeBetweenTry)
            {
                Debug.Log("I'm working. Did I hit? " + (Random.Range(0f, 1f) >= _hitPercentage));
                // ExecuteTry();
            }
        }
    }

    private void ExecuteTry()
    {
        char letter = GetLetter(_currentlyFormedWord.Length, _currentWordObject.Word.Value);
        if (_currentWordObject.Word.MatchStart(_currentlyFormedWord + letter))
        {
            _currentlyFormedWord += letter;
            _currentWordObject.Event_OnInputValueChanged(_currentlyFormedWord);
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
            case Turn.Player:
                EndTurn();
                break;
            case Turn.Enemy:
                StartTurn();
                break;
        }
    }

    private void EndTurn()
    {
        _enemyTurn = false;
        _currentTimeBetweenTries = 0f;
        _currentlyFormedWord = string.Empty;
        // TODO Return current object to the pool.
    }

    private void StartTurn()
    {
        _enemyTurn = true;
        _currentTimeBetweenTries = 0f;
        _currentlyFormedWord = string.Empty;
        // TODO Load an object to start playing.
    }

    public void Event_OnWordFinished(object eventData)
    {
        if (_turnVariable.RuntimeValue != Turn.Enemy)
        {
            return;
        }

        if (eventData is object[] dataArray)
        {
            if (dataArray.Length != 2)
            {
                Debug.LogError("Wrong amount of data received.");
                return;
            }
            if (dataArray[0] is string value)
            {
                WordObject newWordObject = GetNewObject();
                ReturnObjectToPool(_currentWordObject);
                _currentWordObject = newWordObject;
                _currentlyFormedWord = string.Empty;
                return;
            }
            Debug.LogError("Received value is not a string.");
        }
        Debug.LogError("Received value is not an array.");
    }

    private void ReturnObjectToPool(WordObject wordObject)
    {
        // TODO Return object to pool
    }

    private WordObject GetNewObject()
    {
        // TODO Load new object from pool
        return null;
    }
}

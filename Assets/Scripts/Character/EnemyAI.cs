using System;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour, TControllable, IMessageable<CharacterEvent>, IUpdateable, IPoolUser<WordObject>
{
    [field: SerializeField]
    public MessageCallbackData<CharacterEvent>[] CallbackDatas { get; private set; }
    
    [SerializeField, Range(0.25f, 0.95f)]
    private float _hitPercentage = 0.25f;

    [SerializeField, Range(0.10f, 0.45f)]
    private float _timeBetweenTry = 0.10f;

    [SerializeField]
    private Transform _spawnPoint;

    [SerializeField, ReadOnly]
    private bool _enemyTurn;

    [SerializeField, ReadOnly]
    private float _currentTimeBetweenTries;

    [SerializeField, ReadOnly]
    private WordObject _currentWordObject;

    public Action<IConnectable> Connect { get; set; }
    public Action<IConnectable> Disconnect { get; set; }

    private StringBuilder _stringBuilder;
    private const string _alphabet = "abcdefghijklmnopqrstuvwxyz";

    private void Awake()
    {
        _stringBuilder = new StringBuilder();
    }

    private void OnEnable() 
    {
        Connect?.Invoke(this);    
    }

    public void CustomUpdate()
    {
        if (_enemyTurn)
        {
            _currentTimeBetweenTries += Time.deltaTime;
            if (_currentTimeBetweenTries >= _timeBetweenTry)
            {
                ExecuteTry();
            }
        }
    }

    private void OnDisable() 
    {
        Disconnect?.Invoke(this);    
    }

    public void Message_StartTurn(MessageContentPayload contentPayload)
    {
        if (contentPayload is EnemyStartTurnPayload enemyStartTurnPayload)
        {
            if (enemyStartTurnPayload.Turn == Turn.Both || enemyStartTurnPayload.Turn == Turn.Enemy)
            {
                StartTurn();
            }
        }
    }

    public void Message_OnWordFinished(MessageContentPayload contentPayload)
    {
        if (contentPayload is EnemyOnWordFinishedPayload enemyOnWordFinishedPayload)
        {
            if (enemyOnWordFinishedPayload.Turn != Turn.Enemy
                && enemyOnWordFinishedPayload.Turn != Turn.Both)
            {
                return;
            }
            ReturnObjectToPool(_currentWordObject);
            StartUpNewRound();
            return;
        }
    }

    public void Message_EndTurn(MessageContentPayload contentPayload)
    {
        EndTurn();
    }

    private void StartTurn()
    {
        _enemyTurn = true;
        StartUpNewRound();
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

    private void StartUpNewRound()
    {
        _currentWordObject = LoadNewObjectFromPool();
        _stringBuilder.Clear();
        _currentTimeBetweenTries = 0f;
        SetupWordObject();
    }

    private void SetupWordObject()
    {
        _currentWordObject.transform.position = _spawnPoint.position;
        _currentWordObject.GetComponent<Listener>().enabled = false;
        _currentWordObject.gameObject.SetActive(true);
        _currentWordObject.Target = CharacterType.Player;
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

    public WordObject LoadNewObjectFromPool()
    {
        return WordObjectsManager.GetRandom();
    }

    public void ReturnObjectToPool(WordObject wordObject)
    {
        WordObjectsManager.ReturnToPool(wordObject);
    }
}

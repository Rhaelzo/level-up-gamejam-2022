using System;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Controllable class (<see cref="TControllable"/>) responsible for the enemy's AI
/// </summary>
public class EnemyAI : MonoBehaviour, TControllable, IMessageable<CharacterEvent>
    , IUpdatable, IPoolUser<WordObject>
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
    private const string _allowedCharacters = "abcdefghijklmnopqrstuvwxyz- ";

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

    /// <summary>
    /// Message that receives the payload related to starting the
    /// enemy's turn by checking whose current turn it is
    /// </summary>
    /// <param name="contentPayload">
    /// Content payload of type <see cref="CurrentTurnPayload"/>
    /// </param>
    public void Message_StartTurn(MessageContentPayload contentPayload)
    {
        if (contentPayload is CurrentTurnPayload currentTurnPayload)
        {
            if (currentTurnPayload.Turn == Turn.Both || currentTurnPayload.Turn == Turn.Enemy)
            {
                StartTurn();
            }
        }
    }


    /// <summary>
    /// Message related to when a word is finished, using the turn type
    /// to decide whether it should return the object to the pool and get
    /// a new one or not.
    /// </summary>
    /// <param name="contentPayload">
    /// Content payload of type <see cref="CurrentTurnPayload"/>
    /// </param>
    public void Message_OnWordFinished(MessageContentPayload contentPayload)
    {
        if (contentPayload is CurrentTurnPayload currentTurnPayload)
        {
            if (currentTurnPayload.Turn != Turn.Enemy
                && currentTurnPayload.Turn != Turn.Both)
            {
                return;
            }
            ReturnObjectToPool(_currentWordObject);
            StartUpNewRound();
            return;
        }
    }

    /// <summary>
    /// Message related to when the enemy turn ends (when the timer
    /// goes to 0 (<see cref="Timer"/>))
    /// </summary>
    /// <param name="contentPayload">
    /// Content payload is unnecessary
    /// </param>
    public void Message_EndTurn(MessageContentPayload contentPayload)
    {
        EndTurn();
    }

    /// <summary>
    /// Starts the turn by setting up the word for the enemy to start typing
    /// </summary>
    private void StartTurn()
    {
        _enemyTurn = true;
        StartUpNewRound();
    }

    /// <summary>
    /// Ends the enemy's current turn, resetting all of its internal
    /// variables and returning the word back to the pool
    /// </summary>
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

    /// <summary>
    /// Ends the enemy's current turn, resetting all of its internal
    /// variables and returning the word back to the pool
    /// </summary>
    private void StartUpNewRound()
    {
        _currentWordObject = LoadNewObjectFromPool();
        _stringBuilder.Clear();
        _currentTimeBetweenTries = 0f;
        SetupWordObject();
    }

    /// <summary>
    /// Sets up a word object to be used by the AI
    /// </summary>
    private void SetupWordObject()
    {
        _currentWordObject.transform.position = _spawnPoint.position;
        _currentWordObject.GetComponent<Listener>().enabled = false;
        _currentWordObject.gameObject.SetActive(true);
        _currentWordObject.Target = CharacterType.Player;
    }

    /// <summary>
    /// Executes a 'try', by using its hit percentage (<see cref="_hitPercentage"/>) 
    /// to get the correct letter or just guess out of the available characters to it.
    /// <para/>
    /// This happens every complete cycle of the given time in <see cref="_currentTimeBetweenTries"/>
    /// </summary>
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

    /// <summary>
    /// Gets the correct next character/letter based on the <see cref="_hitPercentage"/>
    /// or gets the a random value from the available characters
    /// </summary>
    /// <param name="currentIndex">
    /// Current character index of the word in use
    /// </param>
    /// <param name="value">
    /// Word value currently in use
    /// </param>
    private char GetLetter(int currentIndex, string value)
    {
        float randomValue = Random.Range(0f, 1f);
        if (randomValue > _hitPercentage)
        {
            return _allowedCharacters[Random.Range(0, _allowedCharacters.Length)];
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

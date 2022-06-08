using UnityEngine;

/// <summary>
/// Class that manages the player's word objects,
/// from spawning them from the pool when loading
/// objects after each turn and clearing them when
/// needed
/// </summary>
public class WordsManager : MonoBehaviour, IPoolUser<WordObject>
{
    [Header("Listenables")]
    [SerializeField]
    private TurnVariableSO _turnVariable;

    [SerializeField]
    private GameEvent _wordFinished;

    [Header("Others")]
    [SerializeField, Range(3, 5)]
    private int _maxWordsOnScreen = 3;

    [SerializeField]
    private Transform[] _spawnPoints;

    [Header("Read only")]
    [SerializeField, ReadOnly]
    private WordObject[] _currentlyLoadedObjects;

    [SerializeField, ReadOnly]
    private bool _isInitialized;

    /// <summary>
    /// Event callback to initialize the words manager
    /// </summary>
    /// <param name="eventData">
    /// Event data with no relevant payload data
    /// </param>
    public void Event_Initialize(object eventData)
    {
        Initialize();
    }

    /// <summary>
    /// Event callback to initialize the pause manager
    /// </summary>
    /// <param name="eventData">
    /// Event data with an object array containing the
    /// basic info of the completed word
    /// </param>
    public void Event_OnWordFinished(object eventData)
    {
        if (!_isInitialized || (_turnVariable.RuntimeValue != Turn.Player
            && _turnVariable.RuntimeValue != Turn.Both))
        {
            return;
        }
        if (eventData is object[] dataArray)
        {
            if (dataArray.Length == 3 && dataArray[0] is string value)
            {
                WordObject wordObject = FindWordObjectWithValue(value, out int index);
                if (wordObject == null)
                {
                    Debug.LogWarning("No word object found with value: " + value);
                    return;
                }
                _currentlyLoadedObjects[index] = null;
                ReturnObjectToPool(wordObject);
                WordObject newObject = LoadNewObjectFromPool();
                SetUpWordObject(newObject, _spawnPoints[index].position);
                _currentlyLoadedObjects[index] = newObject;
                _wordFinished.Event_Raise();
                return;
            }
            Debug.LogError("Received value is not a string");
            return;
        }
        Debug.LogError("Received value is not an object[]");
    }

    /// <summary>
    /// Event callback to change functionality based
    /// on the current turn owner
    /// </summary>
    /// <param name="eventData">
    /// Event data with no relevant payload data
    /// </param>
    public void Event_OnTurnShift(object eventData)
    {
        if (!_isInitialized)
        {
            return;
        }
        switch (_turnVariable.RuntimeValue)
        {
            case Turn.Player:
            case Turn.Both:
                LoadWordObjects();
                break;
            case Turn.Enemy:
                ClearWordsArray();
                break;
        }
    }

    /// <summary>
    /// Initializes the words manager
    /// </summary>
    private void Initialize()
    {
        if (_isInitialized)
        {
            Debug.LogError("Word manager is already initialized");
            return;
        }
        _currentlyLoadedObjects = new WordObject[_maxWordsOnScreen];
        _isInitialized = true;
    }

    /// <summary>
    /// Event callback to change functionality based
    /// on the current turn owner
    /// </summary>
    /// <param name="value">
    /// String value of the word (as in, the actual word)
    /// </param>
    /// <param name="indexFoundAt">
    /// Index where the word object was found at
    /// </param>
    /// <returns>
    /// Returns the found word object component and the
    /// index found, or null and -1 respectivelly otherwise
    /// </returns>
    private WordObject FindWordObjectWithValue(string value
        , out int indexFoundAt)
    {
        for (int i = 0; i < _maxWordsOnScreen; i++)
        {
            if (_currentlyLoadedObjects[i] != null
                && _currentlyLoadedObjects[i].Word.Value == value)
            {
                indexFoundAt = i;
                return _currentlyLoadedObjects[i];
            }
        }
        indexFoundAt = -1;
        return null;
    }

    /// <summary>
    /// Clears the words array and returns the objects
    /// to the pool
    /// </summary>
    private void ClearWordsArray()
    {
        for (int i = 0; i < _maxWordsOnScreen; i++)
        {
            if (_currentlyLoadedObjects[i] != null)
            {
                ReturnObjectToPool(_currentlyLoadedObjects[i]);
                _currentlyLoadedObjects[i] = null;
            }
        }
    }

    /// <summary>
    /// Loads word objects from pool equal to
    /// the maximum words allowed on screen
    /// </summary>
    private void LoadWordObjects()
    {
        for (int i = 0; i < _maxWordsOnScreen; i++)
        {
            WordObject newObject = LoadNewObjectFromPool();
            SetUpWordObject(newObject, _spawnPoints[i].position);
            _currentlyLoadedObjects[i] = newObject;
        }
    }

    /// <summary>
    /// Event callback to change functionality based
    /// on the current turn owner
    /// </summary>
    /// <param name="wordObject">
    /// Word object to be set up
    /// </param>
    /// <param name="position">
    /// Position where the word object should be
    /// moved to
    /// </param>
    private void SetUpWordObject(WordObject wordObject, Vector2 position)
    {
        wordObject.WordObjectTransform.position = position;
        wordObject.GetComponent<Listener>().enabled = true;
        wordObject.Target = CharacterType.Enemy;
        wordObject.gameObject.SetActive(true);
    }

    /// <summary>
    /// Load a new object from the pool
    /// </summary>
    /// <returns>
    /// New word object from pool
    /// </returns>
    public WordObject LoadNewObjectFromPool()
    {
        return WordObjectsManager.GetRandom();
    }

    /// <summary>
    /// Returns an object to the pool
    /// </summary>
    /// <param name="objectToReturn">
    /// Word object to be returned to the pool
    /// </param>
    public void ReturnObjectToPool(WordObject objectToReturn)
    {
        WordObjectsManager.ReturnToPool(objectToReturn);
    }
}

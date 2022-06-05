using System.Collections.Generic;
using UnityEngine;

public class WordsManager : MonoBehaviour 
{
    [SerializeField, Range(3, 5)]
    private int _maxWordsOnScreen = 3;

    [SerializeField, ReadOnly]
    private int _currentWordsCount;
    
    [SerializeField, ReadOnly]
    private WordObject[] _currentlyLoadedObjects;

    [SerializeField]
    private List<Transform> _spawnPoints;

    [SerializeField]
    private TurnVariableSO _turnVariable;

    [SerializeField]
    private GameEvent _myWordFinished;

    [SerializeField, ReadOnly]
    private bool _isInitialized;

    public void Event_Initialize(object eventData)
    {
        if (_isInitialized)
        {
            Debug.LogError("Word processor is already initialized.");
            return;
        }
        _currentlyLoadedObjects = new WordObject[_maxWordsOnScreen];
        _isInitialized = true;
    }

    public void Event_OnWordFinished(object eventData)
    {
        if (_turnVariable.RuntimeValue != Turn.Player
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
                int index = 0;
                WordObject wordObject = null;
                for (index = 0; index < _maxWordsOnScreen; index++)
                {
                    if (_currentlyLoadedObjects[index].Word.Value == value)
                    {
                        wordObject = _currentlyLoadedObjects[index];
                        break;
                    }
                }
                if (wordObject == null)
                {
                    Debug.LogWarning("No word object found with value: " + value);
                    return;
                }
                _currentlyLoadedObjects[index] = null;
                ReturnObjectToPool(wordObject);

                WordObject newObject = LoadNewObject();
                newObject.gameObject.transform.position = _spawnPoints[index].position;
                newObject.GetComponent<Listener>().enabled = true;
                newObject.gameObject.SetActive(true);
                newObject.Target = CharacterType.Enemy;
                _currentlyLoadedObjects[index] = newObject;

                _myWordFinished.Event_Raise();
                return;
            }
            Debug.LogError("Received value is not a string.");
        }
        Debug.LogError("Received value is not an array.");
    }

    private WordObject LoadNewObject()
    {
        return WordObjectsManager.GetRandom();
    }

    private void ReturnObjectToPool(WordObject wordObject)
    {
        WordObjectsManager.ReturnToPool(wordObject);
    }

    public void Event_OnTurnShift(object eventData)
    {
        switch (_turnVariable.RuntimeValue)
        {
            case Turn.Player:
            case Turn.Both:
                LoadWordObjects();
                break;
            case Turn.Enemy:
                ClearWords();
                break;
        }
    }

    private void ClearWords()
    {
        foreach (WordObject wordObject in _currentlyLoadedObjects)
        {
            if (wordObject == null)
            {
                continue;
            }
            ReturnObjectToPool(wordObject);
        }
        for (int i = 0; i < _maxWordsOnScreen; i++)
        {
            _currentlyLoadedObjects[i] = null;
        }
        _currentWordsCount = 0;
    }

    private void LoadWordObjects()
    {
        for (int i = 0; i < _maxWordsOnScreen; i++)
        {
            WordObject newObject = LoadNewObject();
            newObject.gameObject.transform.position = _spawnPoints[i].position;
            newObject.GetComponent<Listener>().enabled = true;
            newObject.gameObject.SetActive(true);
            newObject.Target = CharacterType.Enemy;
            _currentlyLoadedObjects[i] = newObject;
        }
        _currentWordsCount = _maxWordsOnScreen;
    }
}

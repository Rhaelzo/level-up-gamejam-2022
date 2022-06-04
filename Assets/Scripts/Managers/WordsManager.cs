using System.Collections.Generic;
using UnityEngine;

public class WordsManager : MonoBehaviour 
{
    [SerializeField, Range(3, 5)]
    private int _maxWordsOnScreen = 3;

    [SerializeField, ReadOnly]
    private int _currentWordsCount;
    
    [SerializeField, ReadOnly]
    private List<WordObject> _currentlyLoadedObjects;

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
        _currentlyLoadedObjects = new List<WordObject>();
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
            if (dataArray.Length != 2)
            {
                Debug.LogError("Wrong amount of data received.");
                return;
            }
            if (dataArray[0] is string value)
            {
                int index = 0;
                WordObject wordObject = null;
                for (index = 0; index < _currentlyLoadedObjects.Count; index++)
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
                _currentlyLoadedObjects.Remove(wordObject);
                _currentWordsCount = _currentlyLoadedObjects.Count;
                if (_currentWordsCount < _maxWordsOnScreen)
                {
                    WordObject newObject = LoadNewObject();
                    newObject.gameObject.transform.position = _spawnPoints[index].position;
                    newObject.gameObject.SetActive(true);
                    _currentlyLoadedObjects.Add(newObject);
                }
                _myWordFinished.Event_Raise();
                ReturnObjectToPool(wordObject);
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
            Debug.Log(wordObject.Word.Value);
            ReturnObjectToPool(wordObject);
        }
        _currentlyLoadedObjects.Clear();
        _currentWordsCount = 0;
    }

    private void LoadWordObjects()
    {
        for (int i = 0; i < _maxWordsOnScreen; i++)
        {
            WordObject newObject = LoadNewObject();
            newObject.gameObject.transform.position = _spawnPoints[i].position;
            newObject.gameObject.SetActive(true);
            _currentlyLoadedObjects.Add(newObject);
        }
        _currentWordsCount = _maxWordsOnScreen;
    }
}

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
        LoadWordObjects();
        _isInitialized = true;
    }

    public void Event_OnWordFinished(object eventData)
    {
        if (_turnVariable.RuntimeValue != Turn.Player)
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
                WordObject wordObject = _currentlyLoadedObjects.Find(word => word.Word.Value == value);
                if (wordObject == null)
                {
                    Debug.LogError("No word object found with value: " + value);
                    return;
                }
                _currentlyLoadedObjects.Remove(wordObject);
                _currentWordsCount = _currentlyLoadedObjects.Count;
                if (_currentWordsCount < _maxWordsOnScreen)
                {
                    LoadNewObject();
                }
                ReturnObjectToPool(wordObject);
                return;
            }
            Debug.LogError("Received value is not a string.");
        }
        Debug.LogError("Received value is not an array.");
    }

    private void LoadNewObject()
    {
        // Loads new object to pool
    }

    private void ReturnObjectToPool(WordObject wordObject)
    {
        // Returns object to pool
    }

    public void Event_OnTurnShift(object eventData)
    {
        switch (_turnVariable.RuntimeValue)
        {
            case Turn.Player:
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
            ReturnObjectToPool(wordObject);
        }
        _currentlyLoadedObjects.Clear();
        _currentWordsCount = 0;
    }

    private void LoadWordObjects()
    {
        for (int i = 0; i < _maxWordsOnScreen; i++)
        {
            LoadNewObject();
        }
        _currentWordsCount = _maxWordsOnScreen;
    }
}

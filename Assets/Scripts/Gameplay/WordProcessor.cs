using System.Collections.Generic;
using UnityEngine;

public class WordProcessor : MonoBehaviour 
{
    [SerializeField, Range(3, 5)]
    private int _maxWordsOnScreen = 3;

    [SerializeField, ReadOnly]
    private int _currentWordsCount;
    
    [SerializeField, ReadOnly]
    private List<WordObject> _currentlyLoadedObjects;

    [SerializeField]
    private List<Transform> _spawnPoints;

    [SerializeField, ReadOnly]
    private bool _isInitialized;

    public void Event_Initialize(object eventData)
    {
        if (_isInitialized)
        {
            Debug.LogError("Word processor is already initialized.");
        }
        _currentlyLoadedObjects = new List<WordObject>();
        for (int i = 0; i < _maxWordsOnScreen; i++)
        {
            // Load pool object
        }
        _currentWordsCount = _maxWordsOnScreen;
        _isInitialized = true;
    }
}

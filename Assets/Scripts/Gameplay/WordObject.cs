using UnityEngine;

public class WordObject : MonoBehaviour 
{
    [field: SerializeField]
    public Word Word { get; private set; }

    [SerializeField, ReadOnly]
    private bool _isInitialized;

    private void Awake() 
    {
        if (!_isInitialized)
        {
            Debug.LogWarning("Word object was not initialized.");
            return;
        }
        Debug.Log("Do stuff");    
    }

    public void Initialize(Word word)
    {
        Word = word;
        _isInitialized = true;
    }

    public void Event_OnInputValueChanged(object eventData)
    {
        if (eventData is string value)
        {
            if (string.IsNullOrWhiteSpace(value) 
                || value == string.Empty)
            {
                Debug.LogWarning("Received invalid value from input.");
                return;
            }
            if (!Word.MatchStart(value))
            {
                ResetVisuals();
            }
            UpdateVisuals(value);
        }
        Debug.LogError("Received value is not a string.");
    }

    private void ResetVisuals()
    {
        // TODO
    }

    private void UpdateVisuals(string value)
    {
        // TODO
    }

    private void ResetObject()
    {

    }
}

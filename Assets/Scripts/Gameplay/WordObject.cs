using UnityEngine;
using TMPro;

public class WordObject : MonoBehaviour 
{
    [field: SerializeField]
    public Word Word { get; private set; }

    [SerializeField]
    private TextMeshProUGUI _wordText;
    
    [SerializeField]
    private GameEvent _onWordFinished;

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
        _wordText.text = Word.Value;
    }

    public void Event_OnInputValueChanged(object eventData)
    {
        if (!_isInitialized)
        {
            Debug.LogWarning("Word object was not initialized.");
            return;
        }
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
            if (Word.IsMatch(value))
            {
                FinishWord();
            }
        }
        Debug.LogError("Received value is not a string.");
    }

    private void FinishWord()
    {
        int result = (int)(Word.Points * Word.Multiplier);
        _onWordFinished.Event_Raise(result);
        // TODO wait a few seconds, blow up, deal damage and back
        // to pool
    }

    private void ResetVisuals()
    {
        _wordText.text = Word.Value;
    }

    private void UpdateVisuals(string value)
    {
        string textToShow = $"<color = red>{value}</color>{Word.Substring(value)}";
        _wordText.text = textToShow;
        // TODO Add some shaking
    }

    private void ResetObject()
    {
        // TODO
    }
}

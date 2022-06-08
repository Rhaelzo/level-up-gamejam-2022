using UnityEngine;
using TMPro;
using UnityEngine.Events;

/// <summary>
/// Class responsible for holding the main gameplay
/// mechanic, the words. It serves as a sort of wrapper
/// that displays the given word
/// </summary>
public class WordObject : MonoBehaviour
{
    [Header("Listenables")]
    [SerializeField]
    private GameEvent _onWordFinished;

    [Header("UI update callbacks")]
    [SerializeField]
    private UnityEvent<string> _updateText;

    [SerializeField]
    private UnityEvent<TMP_FontAsset> _updateFont;

    [SerializeField]
    private UnityEvent<FontWeight> _updateFontWeight;

    [field: Header("Others")]
    [field: SerializeField]
    public CharacterType Target { get; set; } = CharacterType.None;

    [field: Header("Read only")]
    [field: SerializeField, ReadOnly]
    public Word Word { get; private set; }

    [field: SerializeField, ReadOnly]
    public Transform WordObjectTransform { get; private set; }

    [SerializeField, ReadOnly]
    private bool _isInitialized;

    private object[] _dataArray;

    /// <summary>
    /// Event callback for when the input value from the
    /// input field changed 
    /// </summary>
    /// <param name="eventData">
    /// Event data with the string value from the input
    /// field
    /// </param>
    public void Event_OnInputValueChanged(object eventData)
    {
        if (!_isInitialized)
        {
            Debug.LogWarning("Word object was not initialized.");
            return;
        }
        if (eventData is string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value == string.Empty)
            {
                Debug.LogWarning("Received invalid value from input");
                return;
            }
            if (!Word.MatchStart(value))
            {
                ResetVisuals();
                return;
            }
            UpdateVisuals(value);
            if (Word.IsMatch(value))
            {
                FinishWord();
            }
            return;
        }
        Debug.LogError("Received value is not a string");
    }

    /// <summary>
    /// Initializes the word object with a word
    /// </summary>
    /// <param name="word">
    /// Unique word that the word object will hold
    /// </param>
    public void Initialize(Word word)
    {
        if (_isInitialized)
        {
            return;
        }
        Word = word;
        _isInitialized = true;
        _updateText.Invoke(Word.Value);
        _updateFont.Invoke(Word.Font);
        if (Word.Multiplier != 1f)
        {
            _updateFontWeight.Invoke(FontWeight.Bold);
        }
        WordObjectTransform = GetComponent<Transform>();
        _dataArray = new object[3];
        _dataArray[0] = Word.Value;
    }

    /// <summary>
    /// Finishes the word and raises an event with
    /// it's data as a payload
    /// </summary>
    private void FinishWord()
    {
        int result = (int)(Word.Points * Word.Multiplier);
        _dataArray[1] = result;
        _dataArray[2] = Target;
        _onWordFinished.Event_Raise(_dataArray);
        // TODO wait a few seconds, blow up
    }

    /// <summary>
    /// Resets the UI visuals
    /// </summary>
    private void ResetVisuals()
    {
        _updateText.Invoke(Word.Value);
        // TODO stop shaking
    }

    /// <summary>
    /// Updates the visuals with colored text
    /// for correct words
    /// </summary>
    /// <param name="value">
    /// Value to update
    /// </param>
    private void UpdateVisuals(string value)
    {
        string textToShow = $"<color=red>{value}</color>{Word.Substring(value)}";
        _updateText.Invoke(textToShow);
        // TODO Add shaking to colored letters
    }

    /// <summary>
    /// Resets the object, such as visuals and
    /// payload values
    /// </summary>
    public void ResetObject()
    {
        ResetVisuals();
        _dataArray[1] = null;
        _dataArray[2] = null;
    }
}

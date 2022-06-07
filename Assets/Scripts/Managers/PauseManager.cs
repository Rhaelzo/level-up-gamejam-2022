using UnityEngine;

/// <summary>
/// Game's pause manager, keeps track of Escape input
/// and pauses it by changing the Pause Variable SO
/// runtime value
/// </summary>
public class PauseManager : MonoBehaviour
{
    [Header("Listenables")]
    [SerializeField]
    private BoolVariableSO _pauseVariable;

    [SerializeField]
    private BoolVariableSO _gameEndVariable;
    
    [Header("Read only")]
    [SerializeField, ReadOnly]
    private bool _isInitialized;

    private void Update() 
    {
        if (!_isInitialized && _gameEndVariable.RuntimeValue)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseVariable.RuntimeValue = !_pauseVariable.RuntimeValue;
        }
    }

    /// <summary>
    /// Event callback to initialize the pause manager
    /// </summary>
    /// <param name="eventData">
    /// Event data with no relevant payload data
    /// </param>
    public void Event_Initialize(object eventData)
    {
        Initialize();
    }

    /// <summary>
    /// Initializes pause manager
    /// </summary>
    private void Initialize()
    {
        _isInitialized = true;
    }   
}
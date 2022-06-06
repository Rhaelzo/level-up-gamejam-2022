using System;
using UnityEngine;

/// <summary>
/// Class responsible for handling the turn timer
/// </summary>
public class Timer : MonoBehaviour, IPausable, IBarUser
{
    [field: SerializeField, Header("Listenables")]
    public ListenableSO UpdateBar { get; private set; }

    [field: SerializeField]
    public BoolVariableSO PauseVariable { get; private set; }

    [SerializeField]
    private BoolVariableSO _gameEndVariable;

    [SerializeField]
    private GameEvent _endTurn;

    [Header("Others")]
    [SerializeField, Range(10, 20)]
    private float _startingTurnsTime = 10f;

    [Header("Read only")]
    [SerializeField, ReadOnly]
    private float _currentTime;

    [SerializeField, ReadOnly]
    private bool _isCounting;

    private object[] _dataArray;

    private void Awake()
    {
        UpdateBar.UseLogs = false;
        _currentTime = _startingTurnsTime;
        _dataArray = new object[2];
        _dataArray[1] = _startingTurnsTime;
    }

    private void Update()
    {
        if (_gameEndVariable.RuntimeValue || PauseVariable.RuntimeValue)
        {
            return;
        }
        if (_isCounting)
        {
            Tick();
        }
    }

    /// <summary>
    /// Tick the current timer based on the time passed since
    /// last frame
    /// </summary>
    private void Tick()
    {
        _currentTime = Math.Max(_currentTime - Time.deltaTime, 0f);
        _dataArray[0] = _currentTime;
        UpdateBar.Event_Raise(_dataArray);
        if (_currentTime == 0f)
        {
            _isCounting = false;
            _endTurn.Event_Raise();
        }
    }

    /// <summary>
    /// Event to reset the timer
    /// </summary>
    /// <param name="eventData">
    /// Event data containing the current turn value 
    /// </param>
    public void Event_ResetTimer(object eventData)
    {
        if (eventData is Turn value)
        {
            if (value == Turn.Both)
            {
                Debug.Log("Turn is Both, skipping timer reset");
                return;
            }
            ResetTimer();
            return;
        }
        Debug.LogError("Received value is not of type Turn");
    }

    /// <summary>
    /// Resets timer
    /// </summary>
    private void ResetTimer()
    {
        _currentTime = _startingTurnsTime;
        _dataArray[0] = _currentTime;
        UpdateBar.Event_Raise(_dataArray);
        _isCounting = true;
    }
}

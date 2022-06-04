using System;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour, IPausable
{
    [field: SerializeField]
    public BoolVariableSO PauseVariable { get; private set; }

    [SerializeField, Range(10, 20)]
    private float _startingTurnsTime = 10f;

    [SerializeField]
    private GameEvent _onTurnEnd;

    [SerializeField]
    private UnityEvent _changeToOverloadUI;
    
    [SerializeField, ReadOnly]
    private float _currentTime;

    [SerializeField, ReadOnly]
    private bool _isCounting;

    [SerializeField, ReadOnly]
    private bool _overLoadReached;

    private void Awake()
    {
        _currentTime = _startingTurnsTime;
        _isCounting = false;
    }

    private void Update() 
    {
        if (PauseVariable.RuntimeValue || _overLoadReached)
        {
            return;
        }
        if (_isCounting)
        {
            _currentTime = Math.Max(_currentTime - Time.deltaTime, 0f);
            if (_currentTime == 0f)
            {
                _isCounting = false;
                _onTurnEnd.Event_Raise();
            }
        }
    }

    public void Event_ResetTime(object eventData)
    {
        ResetTime();
    }

    private void ResetTime()
    {
        _currentTime = _startingTurnsTime;
        _isCounting = true;
    }

    public void Event_AddTime(object eventData)
    {
        if (eventData is float timeToAdd)
        {
            AddTime(timeToAdd);
        }
        Debug.LogError("Time to add was NaN");
    }

    private void AddTime(float timeToAdd)
    {
        float processedTimeToAdd = Math.Abs(timeToAdd);
        _currentTime = Math.Min(_currentTime + timeToAdd, _startingTurnsTime);
    }

    public void Event_ReduceTime(object eventData)
    {
        if (eventData is float timeToReduce)
        {
            ReduceTime(timeToReduce);
        }
        Debug.LogError("Time to add was NaN");
    }

    private void ReduceTime(float timeToReduce)
    {
        float processedTimeToAdd = Math.Abs(timeToReduce);
        _currentTime = Math.Max(_currentTime - timeToReduce, 0f);
    }

    public void Event_OverloadStart(object eventData)
    {
        _isCounting = false;
        _overLoadReached = true;
        _changeToOverloadUI.Invoke();
    }
}

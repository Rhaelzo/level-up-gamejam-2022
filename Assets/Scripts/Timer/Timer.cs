using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField, ReadOnly]
    private float _currentTime;

    [SerializeField, Range(10, 20)]
    private float _startingTurnsTime = 10f;

    [SerializeField]
    private GameEvent _onTurnEnd;

    private bool _isCounting;

    private void Awake()
    {
        _currentTime = _startingTurnsTime;
        _isCounting = false;
    }

    private void Update() 
    {
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
}

using System;
using UnityEngine;

/// <summary>
/// Class responsible for handling the turn timer
/// </summary>
public class Timer : MonoBehaviour, TControllable, IMessenger<TimerEvent>, IMessageable<TimerEvent>, IUpdatable
{
    [SerializeField]
    private GameEvent _endTurn;

    [Header("Others")]
    [SerializeField, Range(10, 20)]
    private float _startingTurnsTime = 10f;

    [field: SerializeField]
    public MessageCallbackData<TimerEvent>[] CallbackDatas { get; private set; }

    [Header("Read only")]
    [SerializeField, ReadOnly]
    private float _currentTime;

    [SerializeField, ReadOnly]
    private bool _isCounting;

    public Action<IConnectable> Connect { get; set; }
    public Action<IConnectable> Disconnect { get; set; }
    public Action<TimerEvent, MessageContentPayload> SendCustomMessage { get; set; }

    private void Awake()
    {
        _currentTime = _startingTurnsTime;
    }

    private void OnEnable()
    {
        Connect?.Invoke(this);
    }

    private void OnDisable()
    {
        Disconnect?.Invoke(this);
    }

    public void CustomUpdate()
    {
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
        SendCustomMessage.Invoke(TimerEvent.UpdateTimerUI, new UpdateTimerUIPayload(_currentTime, _startingTurnsTime));
        if (_currentTime == 0f)
        {
            _isCounting = false;
            _endTurn.Event_Raise();
        }
    }

    /// <summary>
    /// Resets timer
    /// </summary>
    public void Message_ResetTimer(MessageContentPayload contentPayload)
    {
        _currentTime = _startingTurnsTime;
        SendCustomMessage?.Invoke(TimerEvent.UpdateTimerUI, new UpdateTimerUIPayload(_currentTime, _startingTurnsTime));
        _isCounting = true;
    }
}

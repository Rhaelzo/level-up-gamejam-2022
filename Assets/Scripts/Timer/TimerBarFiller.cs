using System;
using UnityEngine;

/// <summary>
/// Controllable class (<see cref="TUIControllable"/>), responsible for
/// updating the timer's bar
/// </summary>
public class TimerBarFiller : GenericBarFiller, TUIControllable, IMessageable<TimerEvent>
{
    [field: SerializeField]
    public MessageCallbackData<TimerEvent>[] CallbackDatas { get; private set; }

    public Action<IConnectable> Connect { get; set; }
    public Action<IConnectable> Disconnect { get; set; }

    private void OnEnable()
    {
        Connect?.Invoke(this);
    }

    private void OnDisable()
    {
        Disconnect?.Invoke(this);
    }

    /// <summary>
    /// Message that receives the payload related to updating
    /// the timer's bar
    /// </summary>
    /// <param name="contentPayload">
    /// Content payload of type <see cref="UpdateTimerUIPayload"/>
    /// </param>
    public void Message_UpdateBar(MessageContentPayload contentPayload)
    {
        if (contentPayload is UpdateTimerUIPayload updateTimerUIPayload)
        {
            UpdateBar(updateTimerUIPayload.CurrentTime, updateTimerUIPayload.TimePerRound);
            return;
        }
    }
}

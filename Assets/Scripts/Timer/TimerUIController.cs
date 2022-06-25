using System;
using UnityEngine;

/// <summary>
/// Controller class (<see cref="GenericController{T,V}"/>) responsible for controlling the 
/// timer's UI related controllables <see cref="TUIControllable"/>
/// <para/>
/// Communicates with the <see cref="TimerController"/> to receive updates from
/// the <see cref="Timer"/>, making the bridge between view and model
/// </summary>
public class TimerUIController : GenericController<TUIControllable, TimerEvent>, IMessageable<TimerEvent>, TControllable
{
    [field: SerializeField]
    public MessageCallbackData<TimerEvent>[] CallbackDatas { get; private set; }

    public Action<IConnectable> Connect { get; set; }
    public Action<IConnectable> Disconnect { get; set; }

    protected override void OnEnable()
    {
        base.OnEnable();
        Connect?.Invoke(this);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Disconnect?.Invoke(this);
    }

    /// <summary>
    /// Message callback that routes the received message
    /// from the controller to the timer bar
    /// </summary>
    /// <param name="contentPayload">
    /// Content payload of no relevant type
    /// </param>
    public void Message_UpdateTimerBarUI(MessageContentPayload contentPayload)
    {
        SendCustomMessage(TimerEvent.UpdateTimerUI, contentPayload);
    }
}

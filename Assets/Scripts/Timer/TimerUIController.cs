using System;
using UnityEngine;

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
    
    public void Message_UpdateTimerBarUI(MessageContentPayload contentPayload)
    {
        SendCustomMessage(TimerEvent.UpdateTimerUI, contentPayload);
    }
}

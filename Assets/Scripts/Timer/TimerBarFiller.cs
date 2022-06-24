using System;
using UnityEngine;

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

    public void Message_UpdateBar(MessageContentPayload contentPayload)
    {
        if (contentPayload is UpdateTimerUIPayload updateTimerUIPayload)
        {
            UpdateBar(updateTimerUIPayload.CurrentTime, updateTimerUIPayload.TimePerRound);
            return;
        }
    }
}

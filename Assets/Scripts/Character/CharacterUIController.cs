using System;
using UnityEngine;

public class CharacterUIController : GenericController<TUIControllable, CharacterEvent>, IMessageable<CharacterEvent>, TControllable
{
    [field: SerializeField]
    public MessageCallbackData<CharacterEvent>[] CallbackDatas { get; private set; }

    public Action<IConnectable> Connect { get; set; }
    public Action<IConnectable> Disconnect { get; set; }

    protected override void OnEnable()
    {
        base.OnEnable();
        Connect?.Invoke(this);
    }

    protected override void OnDisable()
    {
        base.OnEnable();
        Disconnect?.Invoke(this);
    }

    public void Message_UpdateHealthUI(MessageContentPayload contentPayload)
    {
        SendCustomMessage(CharacterEvent.UpdateHealthUI, contentPayload);
    }
}

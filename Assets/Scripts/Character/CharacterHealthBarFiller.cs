using System;
using UnityEngine;

public class CharacterHealthBarFiller : GenericBarFiller, TUIControllable, IMessageable<CharacterEvent>
{
    [field: SerializeField]
    public MessageCallbackData<CharacterEvent>[] CallbackDatas { get; private set; }

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
        if (contentPayload is UpdateHealthUIPayload updateHealthUIPayload)
        {
            UpdateBar((float)updateHealthUIPayload.Health, (float)updateHealthUIPayload.MaxHealth);
            return;
        }
    }
}

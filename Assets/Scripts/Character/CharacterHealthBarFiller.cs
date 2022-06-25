using System;
using UnityEngine;

/// <summary>
/// Controllable class (<see cref="TUIControllable"/>) responsible for updating the 
/// characters health bar
/// </summary>
public class CharacterHealthBarFiller : GenericBarFiller, TUIControllable
    , IMessageable<CharacterEvent>
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

    /// <summary>
    /// Message that receives the payload related to updating
    /// the character's health bar
    /// </summary>
    /// <param name="contentPayload">
    /// Content payload of type <see cref="UpdateHealthUIPayload"/>
    /// </param>
    public void Message_UpdateBar(MessageContentPayload contentPayload)
    {
        if (contentPayload is UpdateHealthUIPayload updateHealthUIPayload)
        {
            UpdateBar((float)updateHealthUIPayload.Health, (float)updateHealthUIPayload.MaxHealth);
            return;
        }
    }
}

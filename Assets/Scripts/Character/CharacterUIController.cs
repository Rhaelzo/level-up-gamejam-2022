using System;
using UnityEngine;

/// <summary>
/// Controller class (<see cref="GenericController{T,V}"/>) responsible for controlling the 
/// characters UI related controllables <see cref="TUIControllable"/>
/// <para/>
/// Communicates with the <see cref="GameCharacterController"/> to receive updates from
/// the <see cref="Character"/>, making the bridge between view and model
/// </summary>
public class CharacterUIController : GenericController<TUIControllable, CharacterEvent>
    , IMessageable<CharacterEvent>, TControllable
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

    /// <summary>
    /// Message that receives the payload related to updating
    /// the character's health bar
    /// </summary>
    /// <param name="contentPayload">
    /// Content payload being sent to the UI Controllables
    /// (does not need filtering)
    /// </param>
    public void Message_UpdateHealthUI(MessageContentPayload contentPayload)
    {
        SendCustomMessage(CharacterEvent.UpdateHealthUI, contentPayload);
    }
}

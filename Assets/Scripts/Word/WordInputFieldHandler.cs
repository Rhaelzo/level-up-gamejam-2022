using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controllable class (<see cref="TUIControllable"/>) responsible for the handling
/// changes in the input field
/// </summary>
public class WordInputFieldHandler : MonoBehaviour, TUIControllable, IMessageable<WordInputUIEvent>
{
    [field: SerializeField]
    public MessageCallbackData<WordInputUIEvent>[] CallbackDatas { get; private set; }

    [SerializeField]
    private InputField _inputField;

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
    /// Message that receives the payload related to clearing the
    /// input field
    /// </summary>
    /// <param name="contentPayload">
    /// Content payload of no relevant type
    /// </param>
    public void Message_ClearInput(MessageContentPayload contentPayload)
    {
        _inputField.text = string.Empty;
    }

    /// <summary>
    /// Message that receives the payload related to updating
    /// the input field state depending on the current turn
    /// </summary>
    /// <param name="contentPayload">
    /// Content payload of type <see cref="CurrentTurnPayload"/>
    /// </param>
    public void Message_TurnChanged(MessageContentPayload contentPayload)
    {
        if (contentPayload is CurrentTurnPayload currentTurnPayload)
        {
            _inputField.interactable = currentTurnPayload.Turn == Turn.Player
                || currentTurnPayload.Turn == Turn.Both;
            if (_inputField.interactable)
            {
                _inputField.ActivateInputField();
            }
        }
    }
}

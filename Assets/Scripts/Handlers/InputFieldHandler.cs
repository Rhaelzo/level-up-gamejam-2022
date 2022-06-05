using UnityEngine;
using UnityEngine.UI;

public class InputFieldHandler : MonoBehaviour
{
    [SerializeField]
    private InputField _inputField;

    public void Event_ClearInputField(object eventData)
    {
        _inputField.text = string.Empty;
    }

    public void Event_TurnShift(object eventData)
    {
        if (eventData is Turn turn)
        {
            _inputField.interactable = turn == Turn.Player || turn == Turn.Both;
            if (_inputField.interactable)
            {
                _inputField.ActivateInputField();
            }
        }
    }
}

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
}

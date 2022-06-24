using UnityEngine.UI;

public class FocusedInputField : InputField 
{
    private void Update() 
    {
        if (!isFocused)
        {
            Select();
        }    
    }
}
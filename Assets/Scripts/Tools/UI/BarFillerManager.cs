using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class responsible for updating a bar fill amount
/// </summary>
public class BarFillerManager : MonoBehaviour 
{
    [SerializeField]
    private Image _barImage;

    /// <summary>
    /// Event to update the bar fill amount
    /// </summary>
    /// <param name="eventData">
    /// Event data containing the current value and the total value
    /// of whatever the bar is tracking
    /// </param>
    public void Event_UpdateBar(object eventData)
    {
        if (eventData is object[] dataArray)
        {
            if (dataArray.Length == 2 && dataArray[0] is float current 
                && dataArray[1] is float total)
            {
                UpdateBar(current, total);
                return;
            }
            Debug.LogError("Invalid object[] received");
            return;
        }
        Debug.LogError("Received value is not of type object[]");
    }

    /// <summary>
    /// Updates the bar fill amount. 
    /// Won't update if any of the values is either bellow 
    /// or equal to 0f
    /// </summary>
    /// <param name="current">
    /// Current value of whatver the bar is tracking
    /// </param>
    /// <param name="total">
    /// Total value of whatver the bar is tracking
    /// </param>
    private void UpdateBar(float current, float total)
    {
        if (total <= 0 || current < 0)
        {
            Debug.LogError("Invalid value(s) received");
            return;
        }
        _barImage.fillAmount = current / total;
    }
}
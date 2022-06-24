using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class responsible for updating a bar fill amount
/// </summary>
public abstract class GenericBarFiller : MonoBehaviour
{
    [SerializeField]
    private Image _barImage;

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
    protected void UpdateBar(float current, float total)
    {
        if (total <= 0 || current < 0)
        {
            Debug.LogError("Invalid value(s) received");
            return;
        }
        _barImage.fillAmount = current / total;
    }
}
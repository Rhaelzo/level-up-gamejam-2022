using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Class that handles the pause menu UI interactions
/// </summary>
public class PauseMenuManager : MonoBehaviour
{
    [Header("Listenables")]
    [SerializeField]
    public BoolVariableSO _pauseVariable;

    [SerializeField]
    private BoolVariableSO _gameEnd;

    [Header("UI callbacks")]
    [SerializeField]
    private UnityEvent<bool> _onPaused;

    /// <summary>
    /// Event callback when pressing the continue button.
    /// Continues the game
    /// </summary>
    public void Event_Continue()
    {
        _pauseVariable.RuntimeValue = false;
    }

    /// <summary>
    /// Event callback when pressing the exit button.
    /// Send the player to the main menu
    /// </summary>
    public void Event_Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Event callback to trigger pause logic, invoking the
    /// callback that sets the pause menu either active or not
    /// </summary>
    /// <param name="eventData">
    /// Event data with no relevant payload data
    /// </param>
    public void Event_OnPause(object eventData)
    {
        if (_gameEnd.RuntimeValue)
        {
            return;
        }
        _onPaused.Invoke(_pauseVariable.RuntimeValue);
    }
}

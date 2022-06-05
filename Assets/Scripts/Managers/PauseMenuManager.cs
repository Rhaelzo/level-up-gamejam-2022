using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour, IPausable
{
    [field: SerializeField]
    public BoolVariableSO PauseVariable { get; private set; }

    [SerializeField]
    private BoolVariableSO _gameEnd;

    [SerializeField]
    private GameObject _pauseMenu;

    [SerializeField]
    private Button _continueButton;

    public void Event_Continue()
    {
        PauseVariable.RuntimeValue = false;
        _pauseMenu.SetActive(false);
    }

    public void Event_Exit()
    {
        _continueButton.interactable = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void Event_OnPausePressed(object eventData)
    {
        if (_gameEnd.RuntimeValue)
        {
            return;
        }
        
        if (PauseVariable.RuntimeValue)
        {
            _pauseMenu.SetActive(true);
        } 
        else
        {
            _pauseMenu.SetActive(false);
        }
    }
}

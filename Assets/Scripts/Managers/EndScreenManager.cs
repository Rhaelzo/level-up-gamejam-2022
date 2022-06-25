using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    [field: SerializeField]
    public TextMeshProUGUI WinMessageTMPro { get; private set; }

    [field: SerializeField]
    public TextMeshProUGUI ScoreTMPro { get; private set; }

    public void Awake()
    {
        string winMessage = PlayerPrefs.GetInt("playerWon") == 0 ? "You lost!" : "You won!";
        WinMessageTMPro.text = winMessage;
        ScoreTMPro.text += PlayerPrefs.GetFloat("score").ToString();
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

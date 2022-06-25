using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameEvent _initializeSystems;

    [SerializeField]
    private BoolVariableSO _gameEnd;

    [SerializeField]
    public Character _character;

    private void Start()
    {
        _initializeSystems.Event_Raise();
    }

    public void Event_GameEnd(object eventData)
    {
        if (eventData is object[] dataArray)
        {
            if (dataArray[0] is CharacterType characterType)
            {
                GameEnd(characterType);
                return;
            }
            Debug.LogError("Received data was not a character type.");
        }
    }

    private void GameEnd(CharacterType characterType)
    {
        bool gameWon = characterType == CharacterType.Enemy;

        if (gameWon)
        {
            PlayerPrefs.SetInt("playerWon", 1);
        }
        else
        {
            PlayerPrefs.SetInt("playerWon", 0);
        }

        PlayerPrefs.SetFloat("score", CalculateScore());

        _gameEnd.RuntimeValue = true;
        SceneManager.LoadScene("EndScreen");
    }

    private float CalculateScore()
    {
        return _character.Health * ScoreVariables.damageDone / (DateTimeOffset.Now.ToUnixTimeSeconds() - ScoreVariables.unixTimeStarted);
    }
}

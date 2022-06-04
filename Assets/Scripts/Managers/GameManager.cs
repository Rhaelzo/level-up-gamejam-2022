using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameEvent _initializeSystems;

    [SerializeField]
    private GameEvent _gameEnd;

    private void Start()
    {
        _initializeSystems.Event_Raise();
    }

    public void Event_GameEnd(object eventData)
    {
        if (eventData is CharacterType characterType)
        {
            GameEnd(characterType);
        }
        Debug.LogError("Received data was not a character type.");
    }

    private void GameEnd(CharacterType characterType)
    {
        bool gameWon = characterType == CharacterType.Enemy;
        _gameEnd.Event_Raise(gameWon);
    }
}

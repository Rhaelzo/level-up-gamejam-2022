using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameEvent _initializeSystems;

    [SerializeField]
    private BoolVariableSO _gameEnd;

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
        Debug.Log("Hello");
        _gameEnd.RuntimeValue = true;
    }
}

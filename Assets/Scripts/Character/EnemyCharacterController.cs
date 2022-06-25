using UnityEngine;

public class EnemyCharacterController : GameCharacterController
{
    [SerializeField]
    private TurnVariableSO _turnVariableSO;

    [SerializeField]
    private BoolVariableSO _pauseVariableSO;

    [SerializeField]
    private BoolVariableSO _gameEndVariableSO;

    protected override void Update() 
    {
        if (_pauseVariableSO.RuntimeValue 
            || _gameEndVariableSO.RuntimeValue)
        {
            return;
        }
        base.Update();    
    }

    public void Event_OnSwitchTurn(object eventData)
    {
        if (eventData is Turn currentTurn)
        {
            SendCustomMessage(CharacterEvent.EnemyAITurnStart, new CurrentTurnPayload(currentTurn));
            return;
        }
        Debug.LogError("Received value is not a turn.");
    }

    public override void Event_OnWordFinished(object eventData)
    {
        if (eventData is object[] dataArray)
        {
            if (dataArray.Length != 3)
            {
                Debug.LogError("Wrong amount of data received.");
                return;
            }
            if (dataArray[1] is int value && dataArray[2] is CharacterType targetType)
            {
                SendCustomMessage(CharacterEvent.UpdateHealth, new UpdateHealthPayload(targetType, value));
                SendCustomMessage(CharacterEvent.EnemyAIWordFinished, new CurrentTurnPayload(_turnVariableSO.RuntimeValue));
                return;
            }
            Debug.LogError("Received value is invalid.");
        }
        Debug.LogError("Received value is not an array.");
    }

    public void Event_OnEndOfTurn(object eventData)
    {
        SendCustomMessage(CharacterEvent.EnemyAITurnEnd, null);
    }
}
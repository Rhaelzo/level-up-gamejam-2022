using UnityEngine;

public class TurnManager : MonoBehaviour 
{
    [SerializeField]
    private TurnVariableSO _turnVariable;

    [SerializeField, Range(3, 5)]
    private int _numberOfTurns = 3;
    
    [SerializeField]
    private Turn _levelDefault = Turn.Player;

    [SerializeField]
    private GameEvent _onOverloadTurnsReached;

    private int _currentTurnCount;

    private void Awake() 
    {
        _currentTurnCount = 0;
    }

    public void Event_InitializeTurns(object eventData)
    {
        LoadDefaultTurn();
    }

    private void LoadDefaultTurn()
    {
        _turnVariable.RuntimeValue = _levelDefault;
    }

    public void Event_ChangeTurn(object eventData)
    {
        ChangeTurn();
    }

    private void ChangeTurn()
    {
        switch (_turnVariable.RuntimeValue)
        {
            case Turn.Player:
                _turnVariable.RuntimeValue = Turn.Enemy;
                break;
            case Turn.Enemy:
                _currentTurnCount++;
                if (_currentTurnCount >= _numberOfTurns)
                {
                    _turnVariable.RuntimeValue = Turn.Both;
                    _onOverloadTurnsReached.Event_Raise();
                    return;
                }
                _turnVariable.RuntimeValue = Turn.Player;
                break;
            case Turn.None:
            default:
                _turnVariable.RuntimeValue = _levelDefault;
                break;
        }
    }

}
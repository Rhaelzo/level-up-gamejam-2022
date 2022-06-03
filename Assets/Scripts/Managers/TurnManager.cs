using UnityEngine;

public class TurnManager : MonoBehaviour 
{
    [SerializeField]
    private TurnVariableSO _turnVariable;
    
    [SerializeField]
    private Turn _levelDefault = Turn.Player;

    private void Start() 
    {
        Event_InitializeTurns(null);    
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
                _turnVariable.RuntimeValue = Turn.Player;
                break;
            case Turn.None:
            default:
                _turnVariable.RuntimeValue = _levelDefault;
                break;
        }
    }

}
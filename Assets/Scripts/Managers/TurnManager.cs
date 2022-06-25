using System;
using UnityEngine;
using UnityEngine.Events;

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

    [SerializeField]
    private GameEvent _stopEnemyAI;

    [SerializeField]
    private UnityEvent _emergencyDisable;

    [SerializeField]
    private IntVariableSO _turnCountVariable;

    public void Event_InitializeTurns(object eventData)
    {
        ChangeTurn();
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
                _turnCountVariable.RuntimeValue++;
                _emergencyDisable.Invoke();
                _stopEnemyAI.Event_Raise();
                // Here we wait for the response
                break;
            case Turn.None:
            default:
                _turnVariable.RuntimeValue = _levelDefault;
                break;
        }
    }

    public void Event_SpecialChangeTurn(object eventData)
    {
        if (_turnVariable.RuntimeValue == Turn.Enemy)
        {
            if (_turnCountVariable.RuntimeValue == 1)
            {
                ScoreVariables.unixTimeStarted = DateTimeOffset.Now.ToUnixTimeSeconds();
            }
            if (_turnCountVariable.RuntimeValue >= _numberOfTurns)
            {
                _turnVariable.RuntimeValue = Turn.Both;
                _onOverloadTurnsReached.Event_Raise();
                return;
            }
            _turnVariable.RuntimeValue = Turn.Player;
        }
    }
}

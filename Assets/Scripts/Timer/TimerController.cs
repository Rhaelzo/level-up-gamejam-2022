using UnityEngine;

public class TimerController : GenericController<TControllable, TimerEvent>, IPausable
{
    [field: SerializeField]
    public BoolVariableSO PauseVariable { get; private set; }

    [SerializeField]
    private BoolVariableSO _gameEndVariable;

    protected override void Update()
    {
        if (PauseVariable.RuntimeValue || _gameEndVariable.RuntimeValue)
        {
            return;
        }
        base.Update();
    }

    /// <summary>
    /// Event to reset the timer
    /// </summary>
    /// <param name="eventData">
    /// Event data containing the current turn value 
    /// </param>
    public void Event_ResetTimer(object eventData)
    {
        if (eventData is Turn value)
        {
            if (value == Turn.Both)
            {
                Debug.Log("Turn is Both, skipping timer reset");
                return;
            }
            SendCustomMessage(TimerEvent.ResetTimer, null);
            return;
        }
        Debug.LogError("Received value is not of type Turn");
    }
}

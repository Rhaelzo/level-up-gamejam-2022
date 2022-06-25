using UnityEngine;

/// <summary>
/// Controller class (<see cref="GenericController{T,V}"/>) responsible for controlling the 
/// word input UI related controllables <see cref="TUIControllable"/>
/// </summary>
public class WordInputUIController : GenericController<TUIControllable, WordInputUIEvent> 
{
    
    /// <summary>
    /// Event callback for when the input needs to be cleared, namely
    /// after the player completes a word
    /// </summary>
    /// <param name="eventData">
    /// Event data with no relevant payload data
    /// </param>
    public void Event_ClearInput(object eventData)
    {
        SendCustomMessage(WordInputUIEvent.ClearInput, null);
    }

    public void Event_TurnChanged(object eventData)
    {
        if (eventData is Turn turn)
        {
            SendCustomMessage(WordInputUIEvent.TurnChanged, new CurrentTurnPayload(turn));
            SendCustomMessage(WordInputUIEvent.ClearInput, null);
            return;
        }
        Debug.LogWarning("Received data is not of type Turn");
    }
}
using UnityEngine;

/// <summary>
/// Controller class (<see cref="GenericController{T,V}"/>) responsible for controlling
/// everything character related that implement the <see cref="TControllable"/>, such
/// as the <see cref="Character"/> class.
/// <para/>
/// It's also used as a communication intermediary between the "model" and the "view" or UI
/// classes
/// </summary>
public class GameCharacterController : GenericController<TControllable, CharacterEvent>
{
    /// <summary>
    /// Event callback for when a word is finished being written, 
    /// sending a message to the corresponding messageables
    /// </summary>
    /// <param name="eventData">
    /// Event data corresponding to a data array with the word
    /// string value, the points value and the target type
    /// </param>
    public virtual void Event_OnWordFinished(object eventData)
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
                return;
            }
            Debug.LogError("Received invalid value data.");
            return;
        }
        Debug.LogError("Received value is not an array.");
    }
}

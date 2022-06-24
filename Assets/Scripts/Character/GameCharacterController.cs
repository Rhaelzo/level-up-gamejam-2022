using UnityEngine;

public class GameCharacterController : GenericController<TControllable, CharacterEvent>
{
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

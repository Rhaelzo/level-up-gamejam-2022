using UnityEngine;

[CreateAssetMenu(fileName = "New game event SO"
    , menuName = "Tools/Events/Game event", order = 0)]
public class GameEvent : ListenableSO
{
    public override void Event_Raise(object eventData = null)
    {
        base.Event_Raise(eventData);
        Debug.Log("Game event is being raised (" + name + ") with data of "
            + eventData?.ToString());
    }
}

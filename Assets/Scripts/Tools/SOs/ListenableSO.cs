using System.Collections.Generic;
using UnityEngine;

public class ListenableSO : ScriptableObject, IListenable
{
    protected readonly List<ListenerCallbackData> callbacks =
        new List<ListenerCallbackData>();

    public virtual void Event_Raise(object eventData = null)
    {
        foreach (ListenerCallbackData listenerCallbackData in callbacks)
        {
            listenerCallbackData.Callback.Invoke(eventData);
        }
    }

    public virtual void RegisterListener(ListenerCallbackData data)
    {
        if (!callbacks.Contains(data))
        {
            callbacks.Add(data);
        }
    }

    public virtual void UnregisterListener(ListenerCallbackData data)
    {
        if (callbacks.Contains(data))
        {
            callbacks.Remove(data);
        }
    }
}

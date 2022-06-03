using System;
using UnityEngine;

[Serializable]
public class ListenerCallbackData
{
    [field: SerializeField, ReadOnly]
    public string ListenerID { get; private set; }

    public Action<object> Callback { get; private set; }

    public ListenerCallbackData(string listenerID, Action<object> callback)
    {
        ListenerID = listenerID;
        Callback = callback;
    }

    public override bool Equals(object obj)
    {
        if (GetHashCode() == obj.GetHashCode())
        {
            return true;
        }
        if (obj is ListenerCallbackData data)
        {
            return data.ListenerID == ListenerID;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return "Listener callback data with ID " + ListenerID;
    }
}

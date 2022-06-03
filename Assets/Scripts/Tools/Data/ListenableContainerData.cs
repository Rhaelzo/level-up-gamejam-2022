using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ListenableContainerData 
{
    [field: SerializeField, Space]
    public ListenableSO Listenable { get; private set; }
    
    [field: SerializeField, Space]
    public UnityEvent<object> Response { get; private set; }
    
    [field: SerializeField, Space]
    public ListenerCallbackData CallbackData { get; private set; }
    
    public bool IsInitialized { get; private set; }

    public void Initialize()
    {
        if (!IsInitialized)
        {
            Debug.LogWarning("Data is already serialized. Skipping...");
            return;
        }
        string newID = Guid.NewGuid().ToString();
        CallbackData = new ListenerCallbackData(newID, data => Response?.Invoke(data));
        IsInitialized = true;
    }
}

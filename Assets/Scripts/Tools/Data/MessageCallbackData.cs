using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class MessageCallbackData<T> where T : Enum
{
    [field: SerializeField]
    public T MessageType { get; private set; }

    [field: SerializeField, Space]
    public UnityEvent<MessageContentPayload> Callback { get; private set; }
}

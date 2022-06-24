using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericController<T, V> : MonoBehaviour where V : Enum
{
    protected IConnectable[] Connectables { get; set; }
    protected IMessenger<V>[] Messengers { get; set; }

    protected Dictionary<V, Action<MessageContentPayload>> connectionsDictionary;

    protected Action _onUpdate;

    protected virtual void Awake()
    {
        T[] controllables = GetComponentsInChildren<T>();
        Connectables = GetConnectables(controllables);
        Messengers = GetMessengers(controllables);
        connectionsDictionary = new Dictionary<V, Action<MessageContentPayload>>();
    }

    protected virtual void OnEnable()
    {
        SubscribeToMessengers();
        SubscribeToConnections();
    }

    protected virtual void Update()
    {
        _onUpdate?.Invoke();
    }

    protected virtual void OnDisable()
    {
        DisconnectAllConnectables();
        UnsubscribeFromMessengers();
        UnsubscribeFromConnections();
    }

    protected IConnectable[] GetConnectables(T[] controllables)
    {
        List<IConnectable> connectables = new List<IConnectable>();
        for (int i = 0; i < controllables.Length; i++)
        {
            if (controllables[i] is IConnectable connectable)
            {
                connectables.Add(connectable);
            }
        }
        return connectables.ToArray();
    }

    protected IMessenger<V>[] GetMessengers(T[] controllables)
    {
        List<IMessenger<V>> messengers = new List<IMessenger<V>>();
        for (int i = 0; i < controllables.Length; i++)
        {
            if (controllables[i] is IMessenger<V> messenger)
            {
                messengers.Add(messenger);
            }
        }
        return messengers.ToArray();
    }

    protected void SubscribeToMessengers()
    {
        for (int i = 0; i < Messengers.Length; i++)
        {
            if (Messengers[i] is IMessenger<V> messenger)
            {
                messenger.SendCustomMessage += SendCustomMessage;
            }
        }
    }

    protected void UnsubscribeFromMessengers()
    {
        for (int i = 0; i < Messengers.Length; i++)
        {
            if (Messengers[i] is IMessenger<V> messenger)
            {
                messenger.SendCustomMessage -= SendCustomMessage;
            }
        }
    }

    protected void SubscribeToConnections()
    {
        for (int i = 0; i < Connectables.Length; i++)
        {
            if (Connectables[i] is IConnectable connectable)
            {
                connectable.Connect += ConnectToConnectable;
                connectable.Disconnect += DisconnectFromConnectable;
            }
        }
    }

    protected void UnsubscribeFromConnections()
    {
        for (int i = 0; i < Connectables.Length; i++)
        {
            if (Connectables[i] is IConnectable connectable)
            {
                connectable.Connect -= ConnectToConnectable;
                connectable.Disconnect -= DisconnectFromConnectable;
            }
        }
    }

    protected void ConnectToConnectable(IConnectable connectable)
    {
        if (connectable is IMessageable<V> messageable)
        {
            for (int i = 0; i < messageable.CallbackDatas.Length; i++)
            {
                if (!connectionsDictionary.ContainsKey(messageable.CallbackDatas[i].MessageType))
                {
                    connectionsDictionary.Add(messageable.CallbackDatas[i].MessageType
                        , new Action<MessageContentPayload>(messageable.CallbackDatas[i].Callback.Invoke));
                }
                else
                {
                    connectionsDictionary[messageable.CallbackDatas[i].MessageType] += messageable.CallbackDatas[i].Callback.Invoke;
                }
            }
        }

        if (connectable is IUpdateable updateable)
        {
            _onUpdate += updateable.CustomUpdate;
        }
    }

    protected void DisconnectFromConnectable(IConnectable connectable)
    {
        if (connectable is IMessageable<V> messageable)
        {
            for (int i = 0; i < messageable.CallbackDatas.Length; i++)
            {
                if (connectionsDictionary.ContainsKey(messageable.CallbackDatas[i].MessageType))
                {
                    connectionsDictionary[messageable.CallbackDatas[i].MessageType] -= messageable.CallbackDatas[i].Callback.Invoke;
                }
            }
        }

        if (connectable is IUpdateable updateable)
        {
            _onUpdate -= updateable.CustomUpdate;
        }
    }

    protected void DisconnectAllConnectables()
    {
        for (int i = 0; i < Connectables.Length; i++)
        {
            if (Connectables[i] is IConnectable connectable)
            {
                DisconnectFromConnectable(connectable);
            }
        }
    }

    protected void SendCustomMessage(V messageType, MessageContentPayload messagePayload)
    {
        if (connectionsDictionary.ContainsKey(messageType))
        {
            connectionsDictionary[messageType]?.Invoke(messagePayload);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour, IListener
{
    [SerializeField]
    private List<ListenableContainerData> _containerData;

    private void OnEnable()
    {
        SubscribeToListenables();
    }

    private void OnDisable()
    {
        UnsubscribeFromListenables();
    }

    private void OnDestroy()
    {
        UnsubscribeFromListenables();
    }

    public void SubscribeToListenables()
    {
        foreach (ListenableContainerData data in _containerData)
        {
            if (!data.IsInitialized)
            {
                data.Initialize();
            }
            data.Listenable.RegisterListener(data.CallbackData);
        }
    }

    public void UnsubscribeFromListenables()
    {
        foreach (ListenableContainerData data in _containerData)
        {
            if (!data.IsInitialized)
            {
                continue;
            }
            data.Listenable.UnregisterListener(data.CallbackData);
        }
    }
}

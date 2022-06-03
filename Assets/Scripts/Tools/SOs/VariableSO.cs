using UnityEngine;

public abstract class VariableSO<T> : ListenableSO
    , ISerializationCallbackReceiver
{
    [SerializeField]
    protected T defaultValue;

    public T RuntimeValue
    {
        get => _runtimeValue;
        set
        {
            _runtimeValue = value;
            Event_Raise(_runtimeValue);
        }
    }

    private T _runtimeValue;

    public virtual void OnAfterDeserialize()
    {
        _runtimeValue = defaultValue;
    }

    public virtual void OnBeforeSerialize() {}

    public override void Event_Raise(object data = null)
    {
        base.Event_Raise(data);
        Debug.Log(nameof(VariableSO<T>) + " event is being sent by " 
            + name + " with data of " + data?.ToString());
    }
}

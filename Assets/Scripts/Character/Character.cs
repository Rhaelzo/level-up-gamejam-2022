using System;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable, TControllable, IMessenger<CharacterEvent>, IMessageable<CharacterEvent>
{
    [field: SerializeField, Range(50, 200)]
    public int MaxHealth { get; private set; } = 100;

    [field: SerializeField, ReadOnly]
    public int Health { get; private set; }

    [SerializeField]
    private CharacterType _characterType;

    [SerializeField]
    private GameEvent _onCharacterDeath;

    [field: SerializeField]
    public MessageCallbackData<CharacterEvent>[] CallbackDatas { get; private set; }

    public Action<CharacterEvent, MessageContentPayload> SendCustomMessage { get; set; }
    public Action<IConnectable> Connect { get; set; }
    public Action<IConnectable> Disconnect { get; set; }

    private void Awake()
    {
        Health = MaxHealth;
    }

    private void OnEnable()
    {
        Connect?.Invoke(this);
    }

    private void OnDisable()
    {
        Disconnect?.Invoke(this);
    }

    public void IncreaseHealth(int amount)
    {
        Health = Math.Min(Health + amount, MaxHealth);
        SendCustomMessage?.Invoke(CharacterEvent.UpdateHealthUI, new UpdateHealthUIPayload(Health, MaxHealth, amount));
    }

    public void ReduceHealth(int amount)
    {
        Health = Math.Max(Health + amount, 0);
        SendCustomMessage?.Invoke(CharacterEvent.UpdateHealthUI, new UpdateHealthUIPayload(Health, MaxHealth, amount));
        if (Health == 0)
        {
            _onCharacterDeath?.Event_Raise(new object[] { _characterType });
        }
    }

    private void UpdateHealth(int value)
    {
        if (value < 0)
        {
            ReduceHealth(value);
        }
        else if (value > 0)
        {
            IncreaseHealth(value);
        }
    }

    public void Message_UpdateHealth(MessageContentPayload contentPayload)
    {
        if (contentPayload is UpdateHealthPayload updateHealthPayload)
        {
            if (updateHealthPayload.TargetType.Equals(_characterType))
            {
                // This will be reviewed once gameplay is updated.
                // For now, only reduce health by given value.
                UpdateHealth(-1 * updateHealthPayload.Value);
                return;
            }
            return;
        }
    }
}

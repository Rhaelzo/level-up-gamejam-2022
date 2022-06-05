using System;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour, IDamageable
{
    [field: SerializeField, Range(50, 200)]
    public int MaxHealth { get; private set; } = 100;

    [field: SerializeField, ReadOnly]
    public int Health { get; private set; }

    [SerializeField]
    private CharacterType _characterType;

    [SerializeField]
    private Image _healthBar;

    [SerializeField]
    private GameEvent _onCharacterDeath;

    private void Awake() 
    {
        Health = MaxHealth;    
    }

    public void Event_OnWordFinished(object eventData)
    {
        if (eventData is object[] dataArray)
        {
            if (dataArray.Length != 3)
            {
                Debug.LogError("Wrong amount of data received.");
                return;
            }
            if (dataArray[1] is int value && dataArray[2] is CharacterType characterType)
            {
                if (characterType == _characterType)
                {
                    ReduceHealth(value);
                }
                return;
            }
            Debug.LogError("Received value is not a string.");
        }
        Debug.LogError("Received value is not an array.");
    }

    public void IncreaseHealth(int amount)
    {
        int processedAmount = Math.Abs(amount);
        Health = Math.Min(Health + processedAmount, MaxHealth);
        UpdateUI();
    }

    public void ReduceHealth(int amount)
    {
        int processedAmount = Math.Abs(amount);
        Health = Math.Max(Health - processedAmount, 0);
        UpdateUI();
        if (Health == 0)
        {
            _onCharacterDeath.Event_Raise(new object[]{ (CharacterType)_characterType });
        }
    }

    private void UpdateUI()
    {
        _healthBar.fillAmount = Health / (float)MaxHealth;
    }
}

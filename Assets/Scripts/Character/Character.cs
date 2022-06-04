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

    public void IncreaseHealth(int amount)
    {
        int processedAmount = Math.Abs(amount);
        Health = Math.Max(Health + processedAmount, MaxHealth);
        UpdateUI();
    }

    public void ReduceHealth(int amount)
    {
        int processedAmount = Math.Abs(amount);
        Health = Math.Min(Health - processedAmount, 0);
        if (Health == 0)
        {
            _onCharacterDeath.Event_Raise(_characterType);
        }
    }

    private void UpdateUI()
    {
        _healthBar.fillAmount = Health / MaxHealth;
    }
}

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Character : MonoBehaviour, IDamageable
{
    [field: SerializeField, Range(50, 200)]
    public int MaxHealth { get; private set; } = 100;

    [field: SerializeField]
    public int Health { get; private set; }

    [SerializeField]
    private CharacterType _characterType;

    [SerializeField]
    private Image _healthBar;

    [SerializeField]
    private GameEvent _onCharacterDeath;

    [SerializeField, Range(1.5f, 1.7f)]
    private float _maxSpawnRadius = 1.5f;

    [SerializeField, Range(0.5f, 1f)]
    private float _minSpawnRadius = 0.5f;

    [SerializeField, Range(10, 13)]
    private int _maxFontSize = 10;

    [SerializeField, Range(5, 8)]
    private int _minFontSize = 5;

    [SerializeField]
    private TextMeshPro _damagePrefab;

    [SerializeField]
    private Transform _damageSpawnCenter;

    [SerializeField]
    private TMP_FontAsset[] _damageFonts;

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
                    if (characterType == CharacterType.Enemy){
                        ScoreVariables.damageDone += value;
                    }
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
        ShowHealthPopup(+processedAmount);
        UpdateUI();
    }

    public void ReduceHealth(int amount)
    {
        int processedAmount = Math.Abs(amount);
        Health = Math.Max(Health - processedAmount, 0);
        ShowHealthPopup(-processedAmount);
        UpdateUI();
        if (Health == 0)
        {
            _onCharacterDeath.Event_Raise(new object[]{ (CharacterType)_characterType });
        }
    }

    private void ShowHealthPopup(int damage)
    {
        float distance = 0f;
        Vector2 randomPosition = (Vector2)_damageSpawnCenter.position + (Random.insideUnitCircle * _maxSpawnRadius);
        distance = Vector2.Distance(_damageSpawnCenter.position, randomPosition);
        while (!(distance > _minSpawnRadius && distance < _maxSpawnRadius))
        {
            randomPosition = (Vector2)_damageSpawnCenter.position + (Random.insideUnitCircle * _maxSpawnRadius);
            distance = Vector2.Distance(_damageSpawnCenter.position, randomPosition);
        }
        TextMeshPro instancedDamage = Instantiate(_damagePrefab, randomPosition, Quaternion.identity);
        instancedDamage.text = damage.ToString();
        if (_damageFonts.Length != 0)
        {
            instancedDamage.font = _damageFonts[Random.Range(0, _damageFonts.Length)];
        }
        instancedDamage.fontSize = Random.Range(_minFontSize, _maxFontSize);
    }

    private void UpdateUI()
    {
        _healthBar.fillAmount = Health / (float)MaxHealth;
    }
}

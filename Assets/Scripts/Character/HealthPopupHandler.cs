using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class HealthPopupHandler : MonoBehaviour, TUIControllable, IMessageable<CharacterEvent>
{
    [SerializeField]
    private TextMeshPro _popupPrefab;

    [SerializeField]
    private FontsVariableSO _availableFonts;

    [SerializeField, Range(5, 8)]
    private float _minFontSize = 5;

    [SerializeField, Range(10, 13)]
    private float _maxFontSize = 10;

    [SerializeField, Range(0.5f, 1f)]
    private float _minSpawnRadius = 0.5f;

    [SerializeField, Range(1.5f, 1.7f)]
    private float _maxSpawnRadius = 1.5f;

    [field: SerializeField]
    public MessageCallbackData<CharacterEvent>[] CallbackDatas { get; private set; }

    public Action<IConnectable> Connect { get; set; }
    public Action<IConnectable> Disconnect { get; set; }

    private Transform _popupHandlerTransform;

    private void Awake()
    {
        _popupHandlerTransform = transform;
    }

    private void OnEnable()
    {
        Connect?.Invoke(this);
    }

    private void OnDisable()
    {
        Disconnect?.Invoke(this);
    }

    public void Message_ShowPopup(MessageContentPayload contentPayload)
    {
        if (contentPayload is UpdateHealthUIPayload updateHealthUIPayload)
        {
            SpawnPopup(updateHealthUIPayload.Difference);
            return;
        }
    }

    private void SpawnPopup(int value)
    {
        TextMeshPro spawnedPopup = Instantiate(_popupPrefab);
        spawnedPopup.text = value.ToString();
        spawnedPopup.font = ChooseRandomFont();
        spawnedPopup.fontSize = GetRandomFontSize();
        SetRandomPosition(spawnedPopup.transform);
    }

    private TMP_FontAsset ChooseRandomFont()
    {
        int availableFontsCount = _availableFonts.RuntimeValue.Length;
        if (availableFontsCount <= 0)
        {
            Debug.LogWarning("No fonts available");
            return null;
        }
        int randomIndex = Random.Range(0, availableFontsCount);
        return _availableFonts.RuntimeValue[randomIndex];
    }

    private float GetRandomFontSize()
    {
        return Random.Range(_minFontSize, _maxFontSize);
    }

    private void SetRandomPosition(Transform spawnedPopupTransform)
    {
        Vector2 randomPosition = (Vector2)_popupHandlerTransform.position + (Random.insideUnitCircle * _maxSpawnRadius);
        float distance = Vector2.Distance(_popupHandlerTransform.position, randomPosition);
        while (!(distance > _minSpawnRadius && distance < _maxSpawnRadius))
        {
            randomPosition = (Vector2)_popupHandlerTransform.position + (Random.insideUnitCircle * _maxSpawnRadius);
            distance = Vector2.Distance(_popupHandlerTransform.position, randomPosition);
        }
        spawnedPopupTransform.position = randomPosition;
    }
}

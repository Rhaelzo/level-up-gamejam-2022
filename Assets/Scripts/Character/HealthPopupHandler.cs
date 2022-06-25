using UnityEngine;
using TMPro;
using System;
using Random = UnityEngine.Random;

/// <summary>
/// Controllable class <see cref="TUIControllable"/>, responsible for handling the
/// damage and healing popups that appear on screen
/// </summary>
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

    /// <summary>
    /// Message related to when there is an update in the character's
    /// health state and a popup needs to be shown
    /// </summary>
    /// <param name="contentPayload">
    /// Content payload of type <see cref="UpdateHealthUIPayload"/>
    /// </param>
    public void Message_ShowPopup(MessageContentPayload contentPayload)
    {
        if (contentPayload is UpdateHealthUIPayload updateHealthUIPayload)
        {
            SpawnPopup(updateHealthUIPayload.Difference);
            return;
        }
    }

    /// <summary>
    /// Spawns popup with the given value as its text content, a random 
    /// font and a random font size
    /// </summary>
    /// <param name="value">
    /// Damage/healing value to be displayed
    /// </param>
    private void SpawnPopup(int value)
    {
        TextMeshPro spawnedPopup = Instantiate(_popupPrefab);
        spawnedPopup.text = value.ToString();
        spawnedPopup.font = ChooseRandomFont();
        spawnedPopup.fontSize = GetRandomFontSize();
        SetRandomPosition(spawnedPopup.transform);
    }

    /// <summary>
    /// Gets a random font from the available fonts SO (<see cref="_availableFonts"/>)
    /// </summary>
    /// <returns>
    /// Returns a random font from the available fonts given
    /// </returns>
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

    /// <summary>
    /// Gets a random font size value depending on the values given
    /// from <see cref="_minFontSize"/> to <see cref="_maxFontSize"/>
    /// </summary>
    /// <returns>
    /// Returns a random font size value
    /// </returns>
    private float GetRandomFontSize()
    {
        return Random.Range(_minFontSize, _maxFontSize);
    }

    /// <summary>
    /// Moves popup to a position within a circle defined by the given 
    /// parameters in <see cref="_minSpawnRadius"/> and <see cref="_maxSpawnRadius"/>
    /// </summary>
    /// <param name="spawnedPopupTransform">
    /// Transform of the spawned popup
    /// </param>
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

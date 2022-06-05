using UnityEngine;

public class RoundManager : MonoBehaviour, IPausable
{
    [SerializeField]
    private float _timeBetweenRound;
    
    [SerializeField, ReadOnly]
    private float _currentTime;

    [SerializeField]
    private AudioSource _roundAudioSource;

    [SerializeField]
    private AudioClip _roundStartSound;

    [SerializeField]
    private AudioClip _roundEndSound;

    [SerializeField, Range(3, 5)]
    private int _numberOfTurns = 3;
    
    [field: SerializeField]
    public BoolVariableSO PauseVariable { get; private set; }

    private void Update() 
    {
        if (PauseVariable.RuntimeValue)
        {
            return;
        }
        _currentTime += Time.deltaTime;
        if (_currentTime >= _timeBetweenRound)
        {

        }
    }

    public void Event_TurnCountIncrease(object eventData)
    {
        if (eventData is int currentCount)
        {
            StartRoundAnimation(currentCount);
            return;
        }
        Debug.LogError("Received value is NaN.");
    }

    private void StartRoundAnimation(int currentCount)
    {
        if (currentCount < _numberOfTurns)
        {
            _roundAudioSource.clip = _roundStartSound;
            StartNormalRound(currentCount + 1);
        }
        else
        {
            _roundAudioSource.clip = _roundStartSound;
            StartOverloadRound();
        }
    }

    private void StartNormalRound(int currentRound)
    {

    }

    private void StartOverloadRound()
    {

    }
}

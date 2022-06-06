using UnityEngine;
using TMPro;

public class RoundManager : MonoBehaviour, IPausable
{
    [SerializeField]
    private AudioSource _roundAudioSource;

    [SerializeField]
    private AudioClip _roundStartSound;

    [SerializeField]
    private AudioClip _roundEndSound;

    [SerializeField]
    private GameEvent _startNextRound;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private TextMeshProUGUI _roundNumberText;

    [SerializeField]
    private GameObject _overloadObject;

    [SerializeField]
    private GameObject _roundObject;
    [SerializeField]
    private GameObject _numberObject;

    [SerializeField, Range(3, 5)]
    private int _numberOfTurns = 3;

    [SerializeField]
    private float _currentSoundTime = 0f;

    [SerializeField]
    private bool _wasPlaying = false;
    
    [field: SerializeField]
    public BoolVariableSO PauseVariable { get; private set; }

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
            _roundObject.SetActive(true);
            _numberObject.SetActive(true);
            _overloadObject.SetActive(false);
            _roundAudioSource.Stop();
            _roundAudioSource.clip = _roundStartSound;
            _roundAudioSource.Play();
            StartNormalRound(currentCount + 1);
        }
        else
        {
            _roundObject.SetActive(false);
            _numberObject.SetActive(false);
            _overloadObject.SetActive(true);
            _roundAudioSource.Stop();
            _roundAudioSource.clip = _roundStartSound;
            _roundAudioSource.Play();
            StartOverloadRound();
        }
    }

    private void StartNormalRound(int currentRound)
    {
        _roundNumberText.text = currentRound.ToString();
        _animator.SetTrigger("Show");
    }

    public void Event_OnEndAnimation()
    {
        _roundObject.SetActive(false);
        _numberObject.SetActive(false);
        _overloadObject.SetActive(false);
        _roundAudioSource.Stop();
        _startNextRound.Event_Raise();
        _animator.SetTrigger("Idle");
    }

    private void StartOverloadRound()
    {
        _animator.SetTrigger("ShowOverload");
    }

    public void Event_OnPause(object eventData)
    {
        if (PauseVariable.RuntimeValue)
        {
            _animator.speed = 0f;
            if (_roundAudioSource.isPlaying)
            {
                _currentSoundTime = _roundAudioSource.time;
                _roundAudioSource.Stop();
                _wasPlaying = true;
            }
        }
        else
        {
            _animator.speed = 1f;
            if (_wasPlaying)
            {
                _roundAudioSource.Play();
                _roundAudioSource.time = _currentSoundTime;
                _wasPlaying = false;
            }
        }
    }
}

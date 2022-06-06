using UnityEngine;

public class MusicManager : MonoBehaviour, IPausable
{
    [field: SerializeField]
    public BoolVariableSO PauseVariable { get; private set; }

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip[] _audioClips;

    [SerializeField]
    private float _currentSoundTime;

    private void Start() 
    {
        _audioSource.clip = _audioClips[Random.Range(0, _audioClips.Length)];
        _audioSource.Play();
    }

    private void Update()
    {
        if (_audioSource.clip.length <= _audioSource.time)
        {
            _audioSource.Stop();
            _audioSource.clip = _audioClips[Random.Range(0, _audioClips.Length)];
            _audioSource.Play();
        }
    }

    public void Event_OnPause(object eventData)
    {
        if (PauseVariable.RuntimeValue)
        {
            if (_audioSource.isPlaying)
            {
                _currentSoundTime = _audioSource.time;
                _audioSource.Stop();
            }
        }
        else
        {
            _audioSource.Play();
            _audioSource.time = _currentSoundTime;
        }
    }
}

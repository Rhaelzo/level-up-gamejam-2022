using UnityEngine;

/// <summary>
/// Game's music manager, loads random music
/// once the last music has reached completion
/// with given offset
/// </summary>
public class MusicManager : MonoBehaviour, IPausable
{
    [field: Header("Listenables"), SerializeField]
    public BoolVariableSO PauseVariable { get; private set; }

    [Header("Audio")]
    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip[] _audioClips;
    
    [SerializeField, Range(0f, 3f)]
    private float _timeAdditionBetweenSongs = 0f;

    [Header("Read only")]
    [SerializeField, ReadOnly]
    private float _currentSongLength;

    [SerializeField, ReadOnly]
    private float _timeToNextSong;

    [SerializeField, ReadOnly]
    private float _currentSongTime;

    [SerializeField, ReadOnly]
    private bool _isInitialized;

    private void Update()
    {
        if (!_isInitialized || PauseVariable.RuntimeValue)
        {
            return;
        }
        if (_timeToNextSong <= _audioSource.time)
        {
            LoadRandomTrack();
            return;
        }
        _currentSongTime = _audioSource.time;
    }

    /// <summary>
    /// Event callback to trigger pause logic, saving 
    /// the current song time and stopping the music
    /// if the game is paused, or play and set the
    /// previously song time if unpaused 
    /// </summary>
    /// <param name="eventData">
    /// Event data with no relevant payload data
    /// </param>
    public void Event_OnPause(object eventData)
    {
        if (!_isInitialized)
        {
            return;
        }
        if (PauseVariable.RuntimeValue)
        {
            if (_timeToNextSong > _audioSource.time)
            {
                _currentSongTime = _audioSource.time;
                _audioSource.Stop();
            }
        }
        else
        {
            _audioSource.Play();
            _audioSource.time = _currentSongTime;
        }
    }

    /// <summary>
    /// Event callback to initialize the music manager
    /// </summary>
    /// <param name="eventData">
    /// Event data with no relevant payload data
    /// </param>
    public void Event_Initialize(object eventData)
    {
        Initialize();
    }

    /// <summary>
    /// Initializes the music manager, loading a random track
    /// </summary>
    private void Initialize()
    {
        if (_isInitialized)
        {
            Debug.LogWarning("Music manager is already initialized");
            return;
        }
        _isInitialized = true;
        LoadRandomTrack();
    }

    /// <summary>
    /// Loads a random clip, stopping a previous track,
    /// setting up debug data and playing the audio clip
    /// </summary>
    public void LoadRandomTrack()
    {
        _audioSource.Stop();
        _audioSource.clip = _audioClips[Random.Range(0, _audioClips.Length)];
        _currentSongLength = _audioSource.clip.length;
        _timeToNextSong = _currentSongLength + _timeAdditionBetweenSongs;
        _currentSongTime = 0f;
        _audioSource.Play();
    }
}

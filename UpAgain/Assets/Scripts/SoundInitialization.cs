using UnityEngine;

enum AudioType
{
    sound,
    music
}

[RequireComponent(typeof(AudioSource))]
public class SoundInitialization : MonoBehaviour
{
    [SerializeField] private AudioType _audioType;

    private AudioSource _audioSource;
    private float _baseVolume;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _baseVolume = _audioSource.volume;
        Initialize();
    }

    private void OnEnable()
    {
        SettingsValues.Instance.OnChange += Initialize;
    }

    private void OnDisable()
    {
        SettingsValues.Instance.OnChange -= Initialize;
    }

    private void Initialize()
    {
        if (_audioType == AudioType.sound)
            _audioSource.volume = Mathf.Lerp(0, _baseVolume, SettingsValues.Instance.soundVolume);

        else if (_audioType == AudioType.music)
            _audioSource.volume = Mathf.Lerp(0, _baseVolume, SettingsValues.Instance.musicVolume);
    }
}

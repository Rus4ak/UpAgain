using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _step;
    [SerializeField] private AudioSource _hit;
    [SerializeField] private AudioSource _wind;

    private float _baseWindVolume;

    private void Start()
    {
        _baseWindVolume = _wind.volume;
    }

    public void PlayStep()
    {
        _step.pitch = Random.Range(.8f, 1.2f);

        _step.Play();
    }

    public void PlayHit()
    {
        _hit.pitch = Random.Range(.8f, 1.2f);

        _hit.Play();
    }

    public void PlayWind(float speed)
    {
        if (speed < .1f)
        {
            if (_wind.isPlaying)
                _wind.Stop();

            return;
        }
        else if (speed > .1f && !_wind.isPlaying)
        {
            _wind.Play();
        }

        _wind.volume = Mathf.Lerp(0, _baseWindVolume, speed);
        _wind.pitch = Mathf.Lerp(1.5f, 3f, speed);
    }
}

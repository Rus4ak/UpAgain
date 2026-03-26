using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Stone : MonoBehaviour
{
    [SerializeField] private float _minSpeed = 10f;
    [SerializeField] private float _maxSpeed = 25f;

    private float _speed;
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    private Coroutine _fadeCoroutine;
    private float _baseVolume;

    private void Start()
    {
        _speed = Random.Range(_minSpeed, _maxSpeed);
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();

        _audioSource.pitch = Random.Range(.8f, 1.2f);
        _baseVolume = _audioSource.volume;
    }

    private void FixedUpdate()
    {
        Vector3 velocity = _rigidbody.linearVelocity;
        velocity.z = _speed;

        _rigidbody.linearVelocity = velocity;
    }

    private void Update()
    {
        if (transform.position.y < 0)
            Destroy(gameObject);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = null;
        }

        if (!_audioSource.isPlaying)
            _audioSource.Play();

        _audioSource.volume = _baseVolume;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (_fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine);

        _fadeCoroutine = StartCoroutine(FadeOutSound(_audioSource, .3f));
    }

    private IEnumerator FadeOutSound(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;

        float time = 0;

        while (time < fadeTime)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0, time / fadeTime);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Stone : MonoBehaviour
{
    [SerializeField] private float _minSpeed = 10f;
    [SerializeField] private float _maxSpeed = 25f;
    [SerializeField] private GameObject _hitParticles;

    private float _speed;
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    private Coroutine _fadeCoroutine;
    private float _baseVolume;
    private bool _isFreeze;
    private Vector3 _savedVelocity;
    private Vector3 _savedAngularVelocity;
    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _speed = Random.Range(_minSpeed, _maxSpeed);
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _meshRenderer = GetComponent<MeshRenderer>();

        _audioSource.pitch = Random.Range(.8f, 1.2f);
        _baseVolume = _audioSource.volume;
    }

    private void FixedUpdate()
    {
        if (_isFreeze)
            return;

        Vector3 velocity = _rigidbody.linearVelocity;
        velocity.z = _speed;

        _rigidbody.linearVelocity = velocity;
    }

    private void Update()
    {
        if (transform.position.y < 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!SettingsValues.Instance.VFX)
            return;

        if (collision.gameObject.CompareTag("Player"))
            return;

        foreach (ContactPoint contact in collision.contacts)
        {
            Vector3 hitPoint = contact.point;
            Vector3 hitNormal = contact.normal;

            Instantiate(_hitParticles, hitPoint, Quaternion.LookRotation(hitNormal));
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (_isFreeze)
            return;

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

    private void OnEnable()
    {
        Freeze.Instance.FreezeActivate += ActivateFreeze;
        Freeze.Instance.FreezeDisactivate += DisactivateFreeze;
    }

    private void OnDisable()
    {
        Freeze.Instance.FreezeActivate -= ActivateFreeze;
        Freeze.Instance.FreezeDisactivate -= DisactivateFreeze;
    }

    private void ActivateFreeze()
    {
        _isFreeze = true;

        _meshRenderer.material.color = new Color(0, .8f, 1f);

        _savedVelocity = _rigidbody.linearVelocity;
        _savedAngularVelocity = _rigidbody.angularVelocity;

        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        _rigidbody.isKinematic = true;
    }

    private void DisactivateFreeze()
    {
        _rigidbody.isKinematic = false;

        _rigidbody.linearVelocity = _savedVelocity;
        _rigidbody.angularVelocity = _savedAngularVelocity;

        _meshRenderer.material.color = Color.white;

        _isFreeze = false;
    }
}

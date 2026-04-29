using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _groundCheckLayer;
    [SerializeField] private ParticleSystem _smokeParticle;

    private Animator _animator;
    private Rigidbody _rigidbody;
    private bool _isOnGround;
    private Vector3 _lastPosition;
    private float _currentSpeed;
    private PlayerSounds _playerSounds;
    private float _additionalSpeed;

    private bool _isMove = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _playerSounds = GetComponent<PlayerSounds>();

        GameManager.Instance.playerMovement = this;
    }

    private void Start()
    {
        _lastPosition = _rigidbody.position;
    }

    private void Update()
    {
        if (transform.position.y < 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        float t = Mathf.InverseLerp(5f, 20f, _rigidbody.linearVelocity.magnitude);

        if (_rigidbody.linearVelocity.z > 5 && transform.forward.z < 0)
        {
            if (!_animator.GetBool("IsSliding"))
            {
                _animator.SetBool("IsSliding", true);
            }
        }
        else if (_animator.GetBool("IsSliding"))
        {
            _animator.SetBool("IsSliding", false);
        }

        _playerSounds.PlayWind(t);
    }

    private void FixedUpdate()
    {
        _isOnGround = Physics.CheckSphere(transform.position, _groundCheckRadius, _groundCheckLayer);
        
        _animator.SetBool("IsFalling", !_isOnGround);
        _animator.SetFloat("Speed", _currentSpeed);

        if (_isMove)
            Move();

        else if (_currentSpeed > 0)
            _currentSpeed = 0;

        if (_currentSpeed >= 1.5f && !_smokeParticle.isPlaying)
            _smokeParticle.Play();
        else if (_currentSpeed < 1.5f && _smokeParticle.isPlaying)
            _smokeParticle.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Vector3 hitDirection = transform.position - collision.transform.position;
            hitDirection.y = 0;
            hitDirection.Normalize();

            Vector3 force = hitDirection * collision.gameObject.GetComponent<Rigidbody>().linearVelocity.magnitude;
            _rigidbody.AddForce(force, ForceMode.Impulse);

            _playerSounds.PlayHit();
        }
    }

    private void Move()
    {
        if (Input.touchCount == 0 || !_isOnGround)
        {
            _currentSpeed = 0;
            _lastPosition = _rigidbody.position;
            return;
        }

        _currentSpeed = (_rigidbody.position - _lastPosition).magnitude / Time.fixedDeltaTime;
        _lastPosition = _rigidbody.position;

        if (_currentSpeed < .1f)
            _currentSpeed = 0;
        else if (_currentSpeed < 1)
            _currentSpeed = 1;
        else if (_currentSpeed > 3 && _additionalSpeed == 0)
            _currentSpeed = 3;

        Vector3 movement = new Vector3(-_joystick.Horizontal / 2, 0, -_joystick.Vertical);
        
        Vector3 pos = movement * _speed * Time.fixedDeltaTime;

        if (_additionalSpeed > 0)
            pos *= _additionalSpeed;
        
        if (pos.magnitude < .01f)
            return;

        _rigidbody.MovePosition(_rigidbody.position + pos);

        if (movement != Vector3.zero) 
            transform.rotation = Quaternion.LookRotation(movement);
    }

    public void SetMove(bool isMove)
    {
        _isMove = isMove;
        _joystick.gameObject.SetActive(isMove);
    }

    public void SetAdditionalSpeed(float speed, float smoothDuration)
    {
        StartCoroutine(ChangeSpeed(speed, smoothDuration));
    }

    IEnumerator ChangeSpeed(float speed, float duration)
    {
        float startSpeed = _additionalSpeed;
        float time = 0f;

        while (time <= duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            _additionalSpeed = Mathf.Lerp(startSpeed, speed, t);

            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _isOnGround ? Color.green : Color.red;

        Gizmos.DrawWireSphere(transform.position, _groundCheckRadius);
    }
}

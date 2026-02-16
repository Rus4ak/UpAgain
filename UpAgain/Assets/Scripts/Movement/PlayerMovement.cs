using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _groundCheckLayer;

    private Animator _animator;
    private Rigidbody _rigidbody;
    private bool _isOnGround;
    private Vector3 _lastPosition;
    private float _currentSpeed;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _lastPosition = _rigidbody.position;
    }

    private void Update()
    {
        if (transform.position.y < 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void FixedUpdate()
    {
        _isOnGround = Physics.CheckSphere(transform.position, _groundCheckRadius, _groundCheckLayer);

        _animator.SetBool("IsFalling", !_isOnGround);
        _animator.SetFloat("Speed", _currentSpeed);

        Move();

        _currentSpeed = (_rigidbody.position - _lastPosition).magnitude / Time.fixedDeltaTime;
        _lastPosition = _rigidbody.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Vector3 hitDirection = transform.position - collision.transform.position;
            hitDirection.y = 0;
            hitDirection.Normalize();

            _rigidbody.AddForce(hitDirection * 15, ForceMode.Impulse);
        }
    }

    private void Move()
    {
        if (Input.touchCount == 0 || !_isOnGround)
            return;

        Vector3 movement = new Vector3(-_joystick.Horizontal / 2, 0, -_joystick.Vertical);

        _rigidbody.MovePosition(_rigidbody.position + movement * _speed * Time.fixedDeltaTime);

        if (movement != Vector3.zero) 
            transform.rotation = Quaternion.LookRotation(movement);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _isOnGround ? Color.green : Color.red;

        Gizmos.DrawWireSphere(transform.position, _groundCheckRadius);
    }
}

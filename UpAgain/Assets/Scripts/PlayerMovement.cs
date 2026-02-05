using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _groundCheckLayer;

    private Animator _animator;
    private Rigidbody _rigidbody;
    private bool _isOnGround;
    private Vector3 _startedPosition;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _startedPosition = transform.position;
    }

    private void Update()
    {
        if (transform.position.y < 0)
            transform.position = _startedPosition;
    }

    private void FixedUpdate()
    {
        _isOnGround = Physics.CheckSphere(transform.position, _groundCheckRadius, _groundCheckLayer);

        _animator.SetBool("IsFalling", !_isOnGround);
        _animator.SetFloat("Speed", _rigidbody.linearVelocity.magnitude);

        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Vector3 hitDirection = transform.position - collision.transform.position;
            hitDirection.y = 0;
            hitDirection.Normalize();

            _rigidbody.AddForce(hitDirection * 100, ForceMode.Impulse);
        }
    }

    private void Move()
    {
        if (Input.touchCount == 0 || !_isOnGround)
            return;

        Vector3 movement = new Vector3(-_joystick.Horizontal / 2, 0, -_joystick.Vertical);
        
        if (Mathf.Abs(_rigidbody.linearVelocity.magnitude) > 4)
            return;

        _rigidbody.linearVelocity = movement * _speed;

        if (movement != Vector3.zero) 
            transform.rotation = Quaternion.LookRotation(movement);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _isOnGround ? Color.green : Color.red;

        Gizmos.DrawWireSphere(transform.position, _groundCheckRadius);
    }
}

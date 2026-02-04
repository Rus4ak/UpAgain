using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _groundCheckLayer;

    private Animator _animator;
    private Rigidbody _rigidbody;
    private bool _isMove;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _isMove = Physics.CheckSphere(transform.position, _groundCheckRadius, _groundCheckLayer);

        _animator.SetBool("IsFalling", !_isMove);
        _animator.SetFloat("Speed", _rigidbody.linearVelocity.magnitude);

        Move();
    }

    private void Move()
    {
        if (Input.touchCount == 0 || !_isMove)
            return;

        Vector3 movement = new Vector3(-_joystick.Horizontal / 2, 0, -_joystick.Vertical);

        _rigidbody.linearVelocity = movement * _speed;

        if (movement != Vector3.zero) 
            transform.rotation = Quaternion.LookRotation(movement);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _isMove ? Color.green : Color.red;

        Gizmos.DrawWireSphere(transform.position, _groundCheckRadius);
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private float _minSpeed = 5f;
    private float _maxSpeed = 20f;

    private float _speed;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _speed = Random.Range(_minSpeed, _maxSpeed);
        _rigidbody = GetComponent<Rigidbody>();
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
}

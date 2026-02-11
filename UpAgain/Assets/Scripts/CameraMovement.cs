using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _offset;

    private Transform _target;
    private Vector3 _targetOffset;

    private void Start()
    {
        SetTarget(_player, _offset);
    }

    private void FixedUpdate()
    {
        Vector3 movement = _target.position + _targetOffset;

        transform.position = Vector3.Lerp(transform.position, movement, Time.deltaTime * _speed);
    }

    public void SetTarget(Transform target, Vector3 offset)
    {
        _target = target;
        _targetOffset = offset;
    }
}

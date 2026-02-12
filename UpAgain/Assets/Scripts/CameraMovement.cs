using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _offset;

    private Transform _target;
    private Vector3 _targetOffset;
    private Vector3 _lookAt;

    private void Start()
    {
        SetTarget(_player, _offset);
    }

    private void FixedUpdate()
    {
        Vector3 movement = _target.position + _targetOffset;

        transform.position = Vector3.Lerp(transform.position, movement, Time.deltaTime * _speed);
    
        if (_lookAt != default) 
            transform.LookAt(_lookAt);
    }

    public void SetTarget(Transform target, Vector3 offset, Vector3? lookAtWorldPosition = null)
    {
        _target = target;
        _targetOffset = offset;

        if (lookAtWorldPosition.HasValue)
            _lookAt = lookAtWorldPosition.Value;
    }
}

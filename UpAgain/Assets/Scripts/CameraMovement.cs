using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _offset;

    private void FixedUpdate()
    {
        Vector3 movement = _player.position + _offset;

        transform.position = Vector3.Lerp(transform.position, movement, Time.deltaTime * _speed);
    }
}

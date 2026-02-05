using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _obstacles;

    private float _spawnSpeed = 5f;
    private Bounds _colliderBounds;
    private Transform _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _colliderBounds = GetComponent<Collider>().bounds;

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            if (transform.position.y - 5 > _player.position.y)
            {
                int obstacleIndex = Random.Range(0, _obstacles.Count);

                float x = Random.Range(_colliderBounds.min.x, _colliderBounds.max.x);
                Vector3 randomPos = new Vector3(x, transform.position.y, transform.position.z);

                Instantiate(_obstacles[obstacleIndex], randomPos, Quaternion.identity);
            }

            yield return new WaitForSeconds(_spawnSpeed);
        }
    }
}

using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private ObjectPool _obstaclePool;
    [SerializeField] private ObjectPoolParticle _obstacleSmokePool;

    private float _spawnSpeed = 3f;
    private Bounds _colliderBounds;
    private Transform _player;

    private bool _isSpawn = true;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _colliderBounds = GetComponent<Collider>().bounds;

        for (int i = 0; i < GameManager.Instance.CurrentLevel; i++)
        {
            if (_spawnSpeed > 2)
                _spawnSpeed -= .1f;
            else if (_spawnSpeed > 1.5f)
                _spawnSpeed -= .04f;
            else if (_spawnSpeed > 1)
                _spawnSpeed -= .01f;
            else if (_spawnSpeed > .5f)
                _spawnSpeed -= .005f;
            else if (_spawnSpeed > .3f)
                _spawnSpeed -= .0005f;
            else
                break;
        }

        StartCoroutine(Spawn());

        Freeze.Instance.FreezeActivate += StopSpawn;
        Freeze.Instance.FreezeDisactivate += StartSpawn;
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            if (_isSpawn)
            {
                if (transform.position.y - 5 > _player.position.y)
                {
                    float x = Random.Range(_colliderBounds.min.x, _colliderBounds.max.x);
                    Vector3 randomPos = new Vector3(x, transform.position.y, transform.position.z);

                    GameObject obj = _obstaclePool.GetObject();
                    obj.transform.position = randomPos;

                    Stone stone = obj.GetComponent<Stone>();
                    stone.obstaclePool = _obstaclePool;
                    stone.obstacleSmokePool = _obstacleSmokePool;
                }
            }

            yield return new WaitForSeconds(_spawnSpeed);
        }
    }

    private void OnDestroy()
    {
        Freeze.Instance.FreezeActivate -= StopSpawn;
        Freeze.Instance.FreezeDisactivate -= StartSpawn;
    }

    private void StopSpawn()
    {
        _isSpawn = false;
    }

    private void StartSpawn()
    {
        _isSpawn = true;
    }
}

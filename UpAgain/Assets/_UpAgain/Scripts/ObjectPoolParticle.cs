using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolParticle : MonoBehaviour
{
    [SerializeField] private GameObject[] _prefabs;
    [SerializeField] private float _prewarmCount = 10;
    [SerializeField] private int _maxActiveObjects = 30;

    private Queue<GameObject> pool = new Queue<GameObject>();
    private Queue<GameObject> activeObjects = new Queue<GameObject>();

    public void Initialize()
    {
        for (int i = 0; i < _prewarmCount; i++)
        {
            GameObject obj = CreateNew();
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    private GameObject CreateNew()
    {
        return Instantiate(_prefabs[Random.Range(0, _prefabs.Length)]);
    }

    public GameObject GetObject()
    {
        GameObject obj;

        if (activeObjects.Count >= _maxActiveObjects)
        {
            obj = activeObjects.Dequeue();
            obj.SetActive(false);
        }
        else if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
            obj = CreateNew();
        }

        obj.SetActive(true);
        activeObjects.Enqueue(obj);

        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);

        Queue<GameObject> newQueue = new Queue<GameObject>();

        foreach (var item in activeObjects)
        {
            if (item != obj)
                newQueue.Enqueue(item);
        }

        activeObjects = newQueue;

        pool.Enqueue(obj);
    }
}

using UnityEngine;

public class PooledParticle : MonoBehaviour
{
    private ObjectPool pool;

    public void Init(ObjectPool pool)
    {
        this.pool = pool;
    }

    private void OnParticleSystemStopped()
    {
        pool.ReturnObject(gameObject);
    }
}

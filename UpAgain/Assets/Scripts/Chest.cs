using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    public void StartParticle()
    {
        _particleSystem.Play();
    }
}

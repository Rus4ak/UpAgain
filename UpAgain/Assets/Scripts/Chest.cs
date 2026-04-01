using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private ParticleSystem _coinsParticle;
    [SerializeField] private ParticleSystem _jumpParticle;

    public void StartParticleCoins()
    {
        _coinsParticle.Play();
    }

    public void StartParticleJump()
    {
        _jumpParticle.Play();
    }
}

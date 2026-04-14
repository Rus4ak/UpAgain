using System.Collections;
using UnityEngine;

public class TurboSpeed : Ability
{
    [SerializeField] private float _additionalSpeed;
    [SerializeField] private float _duration;

    private PlayerMovement _playerMovement;

    protected override void Initialize()
    {
        base.Initialize();

        _playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    protected override void UseAbility()
    {
        StartCoroutine(IncreaseSpeed());
    }

    IEnumerator IncreaseSpeed()
    {
        _playerMovement.SetAdditionalSpeed(_additionalSpeed);

        EnableEffect();

        yield return new WaitForSeconds(_duration);

        _playerMovement.SetAdditionalSpeed(0);

        DisableEffect();
    }
}

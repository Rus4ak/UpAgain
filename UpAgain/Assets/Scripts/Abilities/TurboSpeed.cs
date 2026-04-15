using System.Collections;
using UnityEngine;

public class TurboSpeed : Ability
{
    [SerializeField] private float _additionalSpeed;
    [SerializeField] private float _duration;
    [SerializeField] private GameObject _VFX;
    [SerializeField] private Transform _effectCamera;

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
        _playerMovement.SetAdditionalSpeed(_additionalSpeed, .1f);

        GameObject effect = Instantiate(_VFX, _effectCamera);

        yield return new WaitForSeconds(_duration);

        _playerMovement.SetAdditionalSpeed(0, .5f);

        Destroy(effect);
    }
}

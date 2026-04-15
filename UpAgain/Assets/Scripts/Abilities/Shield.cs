using System.Collections;
using UnityEngine;

public class Shield : Ability
{
    [SerializeField] private float _duration;
    [SerializeField] private SkinnedMeshRenderer _playerMesh;
    [SerializeField] private Material _playerTransparentMaterial;

    private Rigidbody _playerRigidbody;
    private LayerMask _obstacleLayer;

    protected override void Initialize()
    {
        base.Initialize();

        _playerRigidbody = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        _obstacleLayer = LayerMask.GetMask("Obstacle");
    }

    protected override void UseAbility()
    {
        StartCoroutine(ActivateShield());
    }

    IEnumerator ActivateShield()
    {
        Material defaultMaterial = _playerMesh.material;

        _playerMesh.material = _playerTransparentMaterial;

        _playerRigidbody.excludeLayers |= _obstacleLayer;

        yield return new WaitForSeconds(_duration);

        _playerRigidbody.excludeLayers &= ~_obstacleLayer;

        _playerMesh.material = defaultMaterial;
    }
}

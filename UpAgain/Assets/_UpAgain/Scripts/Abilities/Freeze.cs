using System;
using System.Collections;
using UnityEngine;

public class Freeze : Ability
{
    [SerializeField] private float _duration;

    public event Action FreezeActivate;
    public event Action FreezeDisactivate;

    public static Freeze Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        Instance = this;
    }

    protected override void UseAbility()
    {
        StartCoroutine(ActivateFreeze());
    }

    IEnumerator ActivateFreeze()
    {
        FreezeActivate?.Invoke();

        yield return new WaitForSeconds(_duration);

        FreezeDisactivate?.Invoke();
    }
}

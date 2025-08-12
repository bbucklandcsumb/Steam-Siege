using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Heavy : Enemy
{
    [Header("Enemy details")]
    [SerializeField] private int shieldAmount = 50;
    [SerializeField] private Enemy_Shield shieldObject;

    protected override void Awake()
    {
        base.Awake();
        EnableShieldIfNeeded();

    }

    private void EnableShieldIfNeeded()
    {
        if (shieldObject != null)
            shieldObject.gameObject.SetActive(true);
    }

    public override void TakeDamage(int damage)
    {
        if (shieldAmount > 0)
        {
            shieldAmount -= damage;
            shieldObject.ActivateShieldImpact();

            if (shieldAmount <= 0)
                shieldObject.gameObject.SetActive(false);
        }
        else
            base.TakeDamage(damage);
    }
}

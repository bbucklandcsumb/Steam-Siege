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

        if (shieldObject != null)
        {
            shieldObject.gameObject.SetActive(true);
            shieldObject.SetUpShield(shieldAmount);
        }
    }
}

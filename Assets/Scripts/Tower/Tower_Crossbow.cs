using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Crossbow : Tower
{

    private Crossbow_Visuals visuals;

    [Header("Crossbow details")]
    [SerializeField] private int damage;

    protected override void Awake()
    {
        base.Awake();

        visuals = GetComponent<Crossbow_Visuals>();
    }

    protected override void Attack()
    {
        base.Attack();

        Vector3 directionToEnemy = DirectionToEnemyFrom(gunPoint);

        if (Physics.Raycast(gunPoint.position, directionToEnemy, out RaycastHit hitInfo, Mathf.Infinity))
        {
            towerHead.forward = directionToEnemy;

            Enemy enemyTarget = null;

            Enemy_Shield enemyShield = hitInfo.collider.GetComponent<Enemy_Shield>();
            IDamagable damagable = hitInfo.transform.GetComponent<IDamagable>();

            if (damagable != null && enemyShield == null)
            {
                damagable.TakeDamage(damage);
                enemyTarget = currentEnemy;
            }

            if (enemyShield != null)
            {
                damagable = enemyShield.GetComponent<IDamagable>();
                damagable.TakeDamage(damage);
            }


            visuals.CreateOnHitFx(hitInfo.point);
            visuals.PlayAttackVFX(gunPoint.position, hitInfo.point, enemyTarget);
            visuals.PlayReloaxVFX(attackCooldown);
            AudioManager.instance?.PlaySFX(attackSfx, true);
        }
    }
}

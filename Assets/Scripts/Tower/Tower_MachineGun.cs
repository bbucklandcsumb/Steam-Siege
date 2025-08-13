using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_MachineGun : Tower
{
    private MachineGun_Visuals machineGun_Visuals;

    [Header("Machine Gun Details")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float damage;
    [SerializeField] private float projectileSpeed;
    [Space]
    [SerializeField] private Vector3 rotationOffset;
    [SerializeField] private Transform[] gunPoinSet;
    private int gunPointIndex;

    protected override void Awake()
    {
        base.Awake();
        machineGun_Visuals = GetComponent<MachineGun_Visuals>();
    }

    protected override void Attack()
    {

        gunPoint = gunPoinSet[gunPointIndex];
        Vector3 directionToEnemy = DirectionToEnemyFrom(gunPoint);

        if (Physics.Raycast(gunPoint.position, directionToEnemy, out RaycastHit hitInfo, Mathf.Infinity, whatIsEnemy)) // will be changed to whatIsTargetable when flying is implemented
        {
            IDamagable damagable = hitInfo.transform.GetComponent<IDamagable>();

            if (damagable == null)
                return;

            GameObject newProjectile = Instantiate(projectilePrefab, gunPoint.position, gunPoint.rotation);
            newProjectile.GetComponent<Projectile_MachineGun>().SetupProjectile(hitInfo.point, damagable, damage, projectileSpeed);

            machineGun_Visuals.RecoilFX(gunPoint);

            base.Attack();
            AudioManager.instance?.PlaySFX(attackSfx, true);
            gunPointIndex = (gunPointIndex + 1) % gunPoinSet.Length;
        }
    }

    protected override void RotateTowardsEnemy()
    {
        if (currentEnemy == null)
            return;

        Vector3 directionToEnemy = currentEnemy.CenterPoint() - rotationOffset - towerHead.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);

        Vector3 rotation = Quaternion.Lerp(towerHead.rotation, lookRotation, rotationSpeed * Time.deltaTime).eulerAngles;

        towerHead.rotation = Quaternion.Euler(rotation);
    }
}

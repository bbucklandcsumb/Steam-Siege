using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Cannon : MonoBehaviour
{
    private Rigidbody rb;
    private float damage;

    [SerializeField] private float damageRadius;
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private GameObject explosionFx;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetupProjectile(Vector3 newVelocity, float newDamage)
    {
        rb.velocity = newVelocity;
        damage = newDamage;
    }

    private void DamageEnemiesAround()
    {
        Collider[] enemiesAround = Physics.OverlapSphere(transform.position, damageRadius, whatIsEnemy);

        foreach (Collider enemy in enemiesAround)
        {
            IDamagable damagable = enemy.GetComponent<IDamagable>();

            if (damagable != null)
            {
                int newDamage = Mathf.RoundToInt(damage); //Will be changed later
                damagable.TakeDamage(newDamage);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        DamageEnemiesAround();
        explosionFx.SetActive(true);
        explosionFx.transform.parent = null;
        Destroy(gameObject);
    }

    private void ODrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}

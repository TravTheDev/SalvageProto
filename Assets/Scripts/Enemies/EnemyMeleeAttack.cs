using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour, IEnemyAttack
{
    [Header("Attack Settings")]
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float damage = 15f;
    [SerializeField] private float attackCooldown = 1f;

    private float nextAttackTime;

    public float AttackRange => attackRange;

    public void Attack(Transform target)
    {
        if (target == null)
        {
            return;
        }

        if (Time.time < nextAttackTime)
        {
            return;
        }

        float distance = Vector3.Distance(
            transform.position,
            target.position
        );

        if (distance > attackRange)
        {
            return;
        }

        nextAttackTime = Time.time + attackCooldown;

        IDamageable damageable =
            DamageableFinder.Find(target);

        if (damageable != null && damageable.IsAlive)
        {
            damageable.TakeDamage(
                damage,
                gameObject
            );

            Debug.Log(
                $"{gameObject.name} attacked " +
                $"{target.name} for {damage} damage."
            );
        }
    }
}
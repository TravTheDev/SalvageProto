using UnityEngine;

public interface IEnemyAttack
{
    float AttackRange { get; }

    void Attack(Transform target);
}
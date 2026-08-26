using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Health))]
public class EnemyController : MonoBehaviour
{
    private enum EnemyState
    {
        Idle,
        Chase,
        Attack,
        Dead
    }

    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Detection")]
    [SerializeField] private float detectionRange = 15f;
    [SerializeField] private float loseTargetRange = 20f;

    [Header("Attack")]
    [SerializeField] private MonoBehaviour attackBehaviour;

    private NavMeshAgent agent;
    private Health health;
    private IEnemyAttack attack;

    private EnemyState currentState = EnemyState.Idle;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
        attack = attackBehaviour as IEnemyAttack;
    }

    private void Start()
    {
        if (target == null)
        {
            GameObject player =
                GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                target = player.transform;
            }
        }
    }

    private void OnEnable()
    {
        health.Died += HandleDeath;
    }

    private void OnDisable()
    {
        health.Died -= HandleDeath;
    }

    private void Update()
    {
        if (currentState == EnemyState.Dead || target == null)
        {
            return;
        }

        UpdateState();
        ExecuteState();
    }

    private void UpdateState()
    {
        float distance = Vector3.Distance(
            transform.position,
            target.position
        );

        switch (currentState)
        {
            case EnemyState.Idle:
                if (distance <= detectionRange)
                {
                    currentState = EnemyState.Chase;
                }
                break;

            case EnemyState.Chase:
                if (distance > loseTargetRange)
                {
                    currentState = EnemyState.Idle;
                }
                else if (attack != null &&
                         distance <= attack.AttackRange)
                {
                    currentState = EnemyState.Attack;
                }
                break;

            case EnemyState.Attack:
                if (distance > attack.AttackRange)
                {
                    currentState = EnemyState.Chase;
                }
                break;
        }
    }

    private void ExecuteState()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                agent.isStopped = true;
                break;

            case EnemyState.Chase:
                ChaseTarget();
                break;

            case EnemyState.Attack:
                AttackTarget();
                break;
        }
    }

    private void ChaseTarget()
    {
        agent.isStopped = false;
        agent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        agent.isStopped = true;

        FaceTarget();

        attack?.Attack(target);
    }

    private void FaceTarget()
    {
        Vector3 direction =
            target.position - transform.position;

        direction.y = 0f;

        if (direction.sqrMagnitude <= 0.001f)
        {
            return;
        }

        Quaternion targetRotation =
            Quaternion.LookRotation(direction);

        transform.rotation =
            Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                8f * Time.deltaTime
            );
    }

    private void HandleDeath()
    {
        currentState = EnemyState.Dead;

        if (agent.enabled)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }
    }
}
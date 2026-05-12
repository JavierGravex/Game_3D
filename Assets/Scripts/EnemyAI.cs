using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public int attackDamage = 10;
    public float attackRange = 2.2f;
    public float attackCooldown = 1.25f;

    private EnemyAwareness enemyAwareness;
    private Transform playersTransform;
    private PlayerHealth playerHealth;
    private NavMeshAgent enemyNavMeshAgent;
    private float nextAttackTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyAwareness = GetComponent<EnemyAwareness>();
        var player = Object.FindFirstObjectByType<PlayerMove>();
        playersTransform = player != null ? player.transform : null;
        playerHealth = player != null ? player.GetComponent<PlayerHealth>() : null;
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();

        if (enemyNavMeshAgent != null)
        {
            enemyNavMeshAgent.stoppingDistance = attackRange * 0.75f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playersTransform == null || playerHealth == null || enemyAwareness == null || enemyNavMeshAgent == null)
        {
            return;
        }

        if (!enemyAwareness.isAggro)
        {
            StopMoving();
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, playersTransform.position);

        if (distanceToPlayer <= attackRange)
        {
            StopMoving();
            TryAttackPlayer();
        }
        else if (enemyNavMeshAgent.enabled && enemyNavMeshAgent.isOnNavMesh)
        {
            enemyNavMeshAgent.isStopped = false;
            enemyNavMeshAgent.SetDestination(playersTransform.position);
        }
    }

    private void TryAttackPlayer()
    {
        if (Time.time < nextAttackTime)
        {
            return;
        }

        playerHealth.DamagePlayer(attackDamage);
        nextAttackTime = Time.time + attackCooldown;
    }

    private void StopMoving()
    {
        if (enemyNavMeshAgent.enabled && enemyNavMeshAgent.isOnNavMesh)
        {
            enemyNavMeshAgent.isStopped = true;
            enemyNavMeshAgent.ResetPath();
        }
    }
}

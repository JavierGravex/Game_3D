using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private EnemyAwareness enemyAwareness;
    private Transform playersTransform;
    private UnityEngine.AI.NavMeshAgent enemyNavMeshAgent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyAwareness = GetComponent<EnemyAwareness>();
        var player = Object.FindFirstObjectByType<PlayerMove>();
        playersTransform = player != null ? player.transform : null;
        enemyNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playersTransform == null)
        {
            return;
        }

        if (enemyAwareness.isAggro) {
            enemyNavMeshAgent.SetDestination(playersTransform.position);
        }
        else {
            enemyNavMeshAgent.SetDestination(transform.position);
        }
    }
}

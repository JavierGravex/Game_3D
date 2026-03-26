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
        playersTransform = FindObjectOfType<PlayerMove>().transform;
        enemyNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAwareness.isAggro) {
            enemyNavMeshAgent.SetDestination(playersTransform.position);
        }
        else {
            enemyNavMeshAgent.SetDestination(transform.position);
        }
    }
}

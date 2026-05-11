using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Animator spriteAnim; 
    private AngleToPlayer angleToPlayer;
    private float enemyHealth = 2f; 
    private EnemyManager enemyManager; 

    public GameObject gunHitEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteAnim = GetComponentInChildren<Animator>();
        angleToPlayer = GetComponent<AngleToPlayer>();

        enemyManager = FindObjectOfType<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteAnim.SetFloat("SpriteRot", angleToPlayer.lastIndex);
        if (enemyHealth <= 0)
        {
            enemyManager.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        Instantiate(gunHitEffect, transform.position, Quaternion.identity);
        enemyHealth -= damage;
    }
}

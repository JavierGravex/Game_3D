using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public float range = 20f;
    public float verticalRange = 20f; 
    public float gunShotRadius = 20f;

    public float damage = 2f;

    public float fireRate = 1f;
    private float nextTimeToFire;

    public int maxAmmo;
    private int ammo;

    public LayerMask raycastLayerMask;
    public LayerMask enemyLayerMask;

    private BoxCollider gunTrigger; 
    public EnemyManager enemyManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gunTrigger = GetComponent<BoxCollider>();
        gunTrigger.size = new Vector3(1, verticalRange, range);
        gunTrigger.center = new Vector3(0, 0, range * 0.5f);
        ammo = 15; // Starting ammo
    }

    // Update is called once per frame
    void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame && Time.time > nextTimeToFire && ammo > 0)
        {
            Fire();
        }
        
    }

    void Fire()
    {

        // Simulate gun shot radius

        Collider[] enemyColliders;
        enemyColliders = Physics.OverlapSphere(transform.position, gunShotRadius, enemyLayerMask);

        // Alert any enemy in earshot

        foreach (var enemyCollider in enemyColliders) {
            enemyCollider.GetComponent<EnemyAwareness>().isAggro = true;
        }

        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();

        // damage enemies 
        foreach (var enemy in enemyManager.enemiesInTrigger)
        {

            var dir = enemy.transform.position - transform.position; 

            RaycastHit hit; 
            if (Physics.Raycast(transform.position, dir, out hit, range * 1.5f, raycastLayerMask, QueryTriggerInteraction.Ignore))
            {
                if (hit.transform == enemy.transform)
                {
                    // range check
                    float dist = Vector3.Distance(enemy.transform.position, transform.position);

                    if(dist > range * 0.5f)
                    {
                        // Damage enemy
                        enemy.TakeDamage(damage - 1.0f);
                    }
                    else
                    {
                        // Damage enemy
                        enemy.TakeDamage(damage);
                    } 
                    // Debug.DrawRay(transform.position, dir, Color.green);
                    // Debug.Break();
                }
            }
        }

        // Reset timer
        nextTimeToFire = Time.time + fireRate; 

        // Deduct 1 ammo
        ammo--;
    }

    public void GiveAmmo(int amount, GameObject pickup)
    {
        if (ammo >= maxAmmo) return;

        ammo = Mathf.Min(ammo + amount, maxAmmo);
        Destroy(pickup);
    }

    private void OnTriggerEnter(Collider other)
    {
        // add potential enemy to shoot
        Enemy enemy = other.transform.GetComponent<Enemy>();

        if (enemy)
        {
            // Add enemy
            enemyManager.AddEnemy(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // remove potential enemy to shoot
        Enemy enemy = other.transform.GetComponent<Enemy>();

        if (enemy)
        {
            // remove here
            enemyManager.RemoveEnemy(enemy);
        }
    }
}

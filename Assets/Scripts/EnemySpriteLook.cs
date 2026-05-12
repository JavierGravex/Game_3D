using UnityEngine;

public class EnemySpriteLook : MonoBehaviour
{

    private Transform target;  

    public bool canLookVertically; // Option to allow vertical looking
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = FindObjectOfType<PlayerMove>().transform; 
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canLookVertically)
        {
            transform.LookAt(target);
        }
        else
        {
            Vector3 modifiedTarget = target.position;
            modifiedTarget.y = transform.position.y; // Ignore vertical component
            transform.LookAt(modifiedTarget);
        }
    }
}

using UnityEngine;

public class AngleToPlayer : MonoBehaviour
{

    private Transform player;
    private Vector3 targetPos;
    private Vector3 targetDir; 

    private SpriteRenderer spriteRenderer;
   // 0 = front, 1 = front-right, 2 = right, 3 = back-right, 4 = back, 5 = back-left, 6 = left, 7 = front-left

    private float angle; 

    public int lastIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindObjectOfType<PlayerMove>().transform;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        targetDir = targetPos - transform.position;
        angle = Vector3.SignedAngle(targetDir, transform.forward,  Vector3.up);

        lastIndex = GetIndex(angle);

        
    }

    private int GetIndex(float angle)
    {
        // angle va de -180 a 180

        // 0 = front
        if (angle > -22.5f && angle <= 22.5f)
        {
            return 0;
        }

        // 1 = front-right
        if (angle > -67.5f && angle <= -22.5f)
        {
            return 1;
        }

        // 2 = right
        if (angle > -112.5f && angle <= -67.5f)
        {
            return 2;
        }

        // 3 = back-right
        if (angle > -157.5f && angle <= -112.5f)
        {
            return 3;
        }

        // 4 = back
        if (angle > 157.5f || angle <= -157.5f)
        {
            return 4;
        }

        // 5 = back-left
        if (angle > 112.5f && angle <= 157.5f)
        {
            return 5;
        }

        // 6 = left
        if (angle > 67.5f && angle <= 112.5f)
        {
            return 6;
        }

        // 7 = front-left
        if (angle > 22.5f && angle <= 67.5f)
        {
            return 7;
        }
        
        return lastIndex;
    }
}

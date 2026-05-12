using UnityEngine;

public class SpriteRotator : MonoBehaviour
{

    private Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var player = Object.FindFirstObjectByType<PlayerMove>();
        target = player != null ? player.transform : null;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        transform.LookAt(target);
    }
}

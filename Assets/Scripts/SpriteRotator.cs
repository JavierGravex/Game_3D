using UnityEngine;

public class SpriteRotator : MonoBehaviour
{

    private Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = FindObjectOfType<PlayerMove>().transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
    }
}

using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{

    public float awerenessRadio = 15f;
    public bool isAggro;
    public Material aggroMat;
    private Transform playersTransform;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playersTransform = FindObjectOfType<PlayerMove>().transform;
    }

    // Update is called once per frame
    void Update()
    {

        var dist = Vector3.Distance(transform.position, playersTransform.position);

        if (dist < awerenessRadio) {
            isAggro = true;
        }

        if (isAggro) {
            GetComponent<MeshRenderer>().material = aggroMat;
        }
    }
}

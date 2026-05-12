using UnityEngine;

public class Door : MonoBehaviour
{

    public Animator doorAnim; 

    public bool requiresKey;
    public bool reqGreen; 
    public bool reqBlue;
    public bool reqRed;
    public GameObject areaToSpawn; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if(requiresKey)
            {
                // Do additional checks to see if the player has the key in their inventory
                if(reqGreen && other.GetComponent<PlayerInventory>().hasGreen)
                {
                    doorAnim.SetTrigger("OpenDoor");

                    areaToSpawn.SetActive(true);
                }

                if(reqBlue && other.GetComponent<PlayerInventory>().hasBlue)
                {
                    doorAnim.SetTrigger("OpenDoor");

                    areaToSpawn.SetActive(true);
                }

                if(reqRed && other.GetComponent<PlayerInventory>().hasRed)
                {
                    doorAnim.SetTrigger("OpenDoor");

                    areaToSpawn.SetActive(true);
                }

            }
            else
            {
                doorAnim.SetTrigger("OpenDoor");

                areaToSpawn.SetActive(true);
            }    
        }
    }
}

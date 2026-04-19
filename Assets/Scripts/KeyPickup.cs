using UnityEngine;

public class KeyPickup : MonoBehaviour
{

    public bool isRedKey, isGreenKey, isBlueKey;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(isGreenKey)
            {
                other.GetComponent<PlayerInventory>().hasGreen = true;
            }

            if(isBlueKey)
            {
                other.GetComponent<PlayerInventory>().hasBlue = true;
            }

            if(isRedKey)
            {
                other.GetComponent<PlayerInventory>().hasRed = true;
            }

            Destroy(gameObject);
        }
    }


}

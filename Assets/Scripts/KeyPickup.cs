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
                CanvasManager.Instance.UpdateKeyUI(keyColor:"green");
            }

            if(isBlueKey)
            {
                other.GetComponent<PlayerInventory>().hasBlue = true;
                CanvasManager.Instance.UpdateKeyUI(keyColor:"blue");
            }

            if(isRedKey)
            {
                other.GetComponent<PlayerInventory>().hasRed = true;
                CanvasManager.Instance.UpdateKeyUI(keyColor:"red");
            }

            Destroy(gameObject);
        }
    }


}

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
                if (CanvasManager.Instance != null)
                {
                    CanvasManager.Instance.UpdateKeyUI(keyColor:"green");
                }
            }

            if(isBlueKey)
            {
                other.GetComponent<PlayerInventory>().hasBlue = true;
                if (CanvasManager.Instance != null)
                {
                    CanvasManager.Instance.UpdateKeyUI(keyColor:"blue");
                }
            }

            if(isRedKey)
            {
                other.GetComponent<PlayerInventory>().hasRed = true;
                if (CanvasManager.Instance != null)
                {
                    CanvasManager.Instance.UpdateKeyUI(keyColor:"red");
                }
            }

            Destroy(gameObject);
        }
    }


}

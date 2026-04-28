using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    public bool hasGreen, hasBlue, hasRed;
    
    private void Start()
    {
        CanvasManager.Instance.ClearKeyUI();
    }
}

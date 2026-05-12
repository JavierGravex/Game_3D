using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{

    public bool hasGreen, hasBlue, hasRed;
    
    private void Start()
    {
        if (CanvasManager.Instance != null)
        {
            CanvasManager.Instance.ClearKeyUI();
        }
    }

    private void Update()
    {
        if(hasGreen && hasBlue && hasRed)
        {

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SceneManager.LoadScene("VictoryScreen");
        }
    }
}

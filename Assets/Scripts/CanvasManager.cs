using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public TextMeshProUGUI health;
    public TextMeshProUGUI armor;
    public TextMeshProUGUI ammo;

    public Image healthIndicator;

    public Sprite Health1; //Full health sprite
    public Sprite Health2;
    public Sprite Health3;  
    public Sprite Health4; //dead sprite

    public GameObject redkey;
    public GameObject bluekey;
    public GameObject greenkey; 


    private static CanvasManager _instance;
    public static CanvasManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    //Methods to update the UI elements

    public void UpdateHealth(int healthValue)
    {
        health.text = healthValue.ToString() + "%";
        UpdateHealthIndicator(healthValue);
    }

    public void UpdateArmor(int armorValue)
    {
        armor.text = armorValue.ToString() + "%";
    }

    public void UpdateAmmo(int ammoValue)
    {
        ammo.text = ammoValue.ToString();
    }


    public void UpdateHealthIndicator(int healthValue)
    {
        // Update health indicator sprite based on current health
        if (healthValue >= 66)
        {
            healthIndicator.sprite = Health1; // Full health
        }
        else if (healthValue < 66 && healthValue > 33)
        {
            healthIndicator.sprite = Health2; // Medium health
        }
        else if (healthValue <= 33 && healthValue > 0)
        {
            healthIndicator.sprite = Health3; // Low health
        }
        else
        {
            healthIndicator.sprite = Health4; // Dead
        }
    }

    public void UpdateKeyUI(string keyColor)
    {
        if(keyColor == "red")
        {
            redkey.SetActive(true);
        }

        if(keyColor == "blue")
        {
            bluekey.SetActive(true);
        }

        if(keyColor == "green")
        {
            greenkey.SetActive(true);
        }
        
    }

    public void ClearKeyUI()
    {
        redkey.SetActive(false);
        bluekey.SetActive(false);
        greenkey.SetActive(false);
    }
}

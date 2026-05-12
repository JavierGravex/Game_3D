using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{   
    public int maxHealth;
    private int health;

    public int MaxArmor;
    private int armor;
    private bool isDead;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        health = maxHealth;  
        armor = 0;  

        if (CanvasManager.Instance != null)
        {
            CanvasManager.Instance.UpdateHealth(health);
            CanvasManager.Instance.UpdateArmor(armor);
        }
    }

    public void DamagePlayer(int damage)
    {
        if (isDead || damage <= 0)
        {
            return;
        }

        int absorbedDamage = Mathf.Min(armor, damage);
        armor -= absorbedDamage;
        health = Mathf.Max(health - (damage - absorbedDamage), 0);

        if (CanvasManager.Instance != null)
        {
            CanvasManager.Instance.UpdateHealth(health);
            CanvasManager.Instance.UpdateArmor(armor);
            CanvasManager.Instance.ShowDamageFeedback();
        }

        if (health <= 0)
        {
            isDead = true;
            Debug.Log("Player has died.");

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SceneManager.LoadScene("DeathScreen");
        }
    }

    public void GiveHealth(int amount, GameObject pickup)
    {
        if (health < maxHealth)
        {
            health = Mathf.Min(health + amount, maxHealth);
            Destroy(pickup);
        }

        if (CanvasManager.Instance != null)
        {
            CanvasManager.Instance.UpdateHealth(health);
        }
    }

    public void GiveArmor(int amount, GameObject pickup)
    {
        if (armor < MaxArmor)
        {
            armor = Mathf.Min(armor + amount, MaxArmor);
            Destroy(pickup);
        }

        if (CanvasManager.Instance != null)
        {
            CanvasManager.Instance.UpdateArmor(armor);
        }
    }
}

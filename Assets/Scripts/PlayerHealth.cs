using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{   
    public int maxHealth;
    private int health;

    public int MaxArmor;
    private int armor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        health = maxHealth;  
        CanvasManager.Instance.UpdateHealth(health);
        armor = 0;  
        CanvasManager.Instance.UpdateArmor(armor);
    }

    // Update is called once per frame
    void Update()
    {   
        //temporary test
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            DamagePlayer(30);
            Debug.Log("Player has been damaged." );
        }
    }

    public void DamagePlayer(int damage)
    {
        
        if(armor > 0)
        {
            if(armor >= damage)
            {
                armor -= damage;
            }
            else if(armor < damage)
            {
                int remainingDamage;
                remainingDamage = damage - armor;
                armor = 0;
                health -= remainingDamage;
            }
        }
        else
        {
            health -= damage;
        }

        if(health <= 0)
        {
            Debug.Log("Player has died.");

            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);

        }

        CanvasManager.Instance.UpdateHealth(health);
        CanvasManager.Instance.UpdateArmor(armor);
    }

    public void GiveHealth(int amount, GameObject pickup)
    {
        if (health < maxHealth)
        {
        health = Mathf.Min(health + amount, maxHealth);
        Destroy(pickup);
        }
        CanvasManager.Instance.UpdateHealth(health);
    }

    public void GiveArmor(int amount, GameObject pickup)
    {
        if (armor < MaxArmor)
        {
            armor = Mathf.Min(armor + amount, MaxArmor);
            Destroy(pickup);
        }
        CanvasManager.Instance.UpdateArmor(armor);
    }
}

    
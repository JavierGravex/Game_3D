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
        armor = 0;  
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

        
    }

    public void GiveHealth(int amount, GameObject pickup)
    {
        if (health >= maxHealth) return;

        health = Mathf.Min(health + amount, maxHealth);
        Destroy(pickup);
    }

    public void GiveArmor(int amount, GameObject pickup)
    {
        if (armor >= MaxArmor) return;

        armor = Mathf.Min(armor + amount, MaxArmor);
        Destroy(pickup);
    }
}

    
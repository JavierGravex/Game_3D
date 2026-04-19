using UnityEngine;
using System.Collections; 
using System.Collections.Generic;

public class ItemPickup : MonoBehaviour
{

    public bool isHealth;
    public bool isAmmo;
    public bool isArmor;

    public int ammout; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(isHealth)
            {
                other.GetComponent<PlayerHealth>().GiveHealth(ammout, this.gameObject);
            }
            if(isAmmo)
            {
                other.GetComponentInChildren<Gun>().GiveAmmo(ammout, this.gameObject);
            }
            if(isArmor)
            {
                other.GetComponent<PlayerHealth>().GiveArmor(ammout, this.gameObject);
            }
        }
    }
}

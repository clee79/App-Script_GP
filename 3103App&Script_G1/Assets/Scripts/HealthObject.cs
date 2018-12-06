using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthObject : MonoBehaviour {

    public float healamount;
    public PlayerController player;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "Player")
        {           
            if (player.CurrentHealth < player.MaxHealth)
            {                
                float phealth = player.CurrentHealth;               
                phealth += healamount;               
                if(phealth > 100f)
                {
                    phealth = 100f;
                }
                player.CurrentHealth = phealth;
                gameObject.SetActive(false);                
                
            }
        }
    }
}

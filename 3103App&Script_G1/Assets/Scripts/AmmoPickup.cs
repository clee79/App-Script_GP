using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour {

    public string ammoType;
    public PlayerController player;
    public GameObject ammo;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            player.AmmoType = this.ammoType;
            player.Shot = ammo;

            gameObject.SetActive(false);
        }
    }
}

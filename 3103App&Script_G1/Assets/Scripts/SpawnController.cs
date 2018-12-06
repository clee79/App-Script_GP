using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public List<SkeletonController> enemy;
    public PlayerController player;
    public Vector3 spawn;
    public SkeletonController[] enemies;
    public int spawncount;


    void Start()
    {
        enemy = new List<SkeletonController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Spawn();
    }


    void Update()
    {
        Vector3 currentPos = player.transform.position;
        foreach (SkeletonController i in enemy)
        {
            if (i != null)
            {
                //i.Move(currentPos);                
                //i.Attack(currentPos);
            }
        }        
    }

    // Adapted function from previous project to have dynamic mob spawns at start.
    public void Spawn()
    {
        // Spawn enemies until spawncount met.
        while (enemy.Count <= spawncount)
        {
            // Pick an enemy type then assign a random range.
            // Expand later with random mob selection
            int select = 0;
            Vector3 spawnlocation = new Vector3(Random.Range(-spawn.x, spawn.x), spawn.y, Random.Range(-spawn.z, spawn.z));

            // if the random location is to close the player, while loop defines new spawn
            while (Vector3.Distance(spawnlocation, player.transform.position) <= 20)
            {
                spawnlocation = new Vector3(Random.Range(-spawn.x, spawn.x) + 2, spawn.y, Random.Range(-spawn.z, spawn.z) + 2);
            }
            Quaternion spawnquaterion = Quaternion.identity;

            // Based on random selection, spawn enemy type and add to list of enemy.
            // To Be expanded later to include more enemy types
            if (select == 0)
            {
                enemy.Add(Instantiate(enemies[0], spawnlocation, spawnquaterion));
            }           
            else
            {
                // Debug check for mob type selection
                Debug.Log("Error: Select mob out of range: " + select);
            }
        }
    }
}


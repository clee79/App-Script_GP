using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendAI : MonoBehaviour {

    public PlayerController player;
    NavMeshAgent agent;    
    public Transform shotspawn;
    public GameObject shot;
    private float time;
    private float tdelay = 0f;
    private SkeletonController enm;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        shot = player.Shot;
    }
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance > 2)
        {
            agent.SetDestination(player.transform.position);            
        }        

        shot = player.Shot;

    }

    public void Shoot(Vector3 target)
    {   
        transform.LookAt(target);
        if(player != null)
        {
            time += Time.deltaTime;
            if(time > tdelay)
            {
                tdelay = time + 5;
                Instantiate(shot, shotspawn.transform.position, transform.rotation);
                
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Vector3 target = other.gameObject.transform.position;
            Shoot(target);
           
        }
        
    }
}

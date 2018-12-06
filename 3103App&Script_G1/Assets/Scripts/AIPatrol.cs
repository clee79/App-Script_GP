using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIPatrol : MonoBehaviour
{
    
    private NavMeshAgent nav;    
    public float patrolSpeed = 2.0f;    
    public Transform[] waypoints;
    private int curWaypoint = 0;
    private int maxWaypoint;

    public float minWaypointDistance = 0.1f;
    PlayerController player;
    SkeletonController skele;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();

        maxWaypoint = waypoints.Length - 1;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        skele = this.gameObject.GetComponent<SkeletonController>();        
    }
    
    void Update()
    {
        Patrolling();
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > 10)
        {
            Patrolling();
        }
        else
        {
            ChasePlayer();
        }
    }

    public void Patrolling()
    {        
        nav.speed = patrolSpeed;        
        Vector3 tempLocalPosition;
        Vector3 tempWaypointPosition;
        
        tempLocalPosition = transform.position;
        tempLocalPosition.y = 0f;
        
        tempWaypointPosition = waypoints[curWaypoint].position;
        tempWaypointPosition.y = 0f;

        
        if (Vector3.Distance(tempLocalPosition, tempWaypointPosition) <= minWaypointDistance)
        {
            if (curWaypoint == maxWaypoint)
            {
                curWaypoint = 0;
            }
                
            else
            {
                curWaypoint++;
            }
        }      
        nav.SetDestination(waypoints[curWaypoint].position);
    }
    
    public void ChasePlayer()
    {
        skele.Move(player.transform.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonController : MonoBehaviour {

    public float maxHealth = 50;
    public float speed = .5f;
    public PlayerController player;
    private float time;
    private float tdelay = 0f;
    public Animator skelAnimator;
    private float distance;
    NavMeshAgent agent;
    private bool isStopped;


    public float currentHealth = 0f;
    public GameObject healthBar;
    public GameObject healthframe;
    public Camera gameCamera;

    //public Transform[] patrolPoint;
    //private int destPoint = 0;
    //private Vector3 lastPoint;
    

    public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        skelAnimator = GetComponent<Animator>();
        currentHealth = maxHealth;
        gameCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        skelAnimator.SetBool("isIdle", true);

        UpdateDistance();

        agent = this.GetComponent<NavMeshAgent>();
        agent.speed = Random.Range(.8f, 1.5f);

        //transform.position = patrolPoint[0].position;
       
        // start patrol
        //GotoPoint();

    }

    void Update()
    {
        if (currentHealth != maxHealth)
        {
            float calhealth = CalcHealthBar(maxHealth, currentHealth);
            SetHealthBar(calhealth);
        }
        // Point the health frames at the camera so they can clearly be seen.
        healthframe.transform.LookAt(healthframe.transform.position + gameCamera.transform.rotation * Vector3.back, gameCamera.transform.rotation * Vector3.up);

        if (!agent.pathPending && agent.remainingDistance < 1f)
        {
            //GotoPoint();
        }

        UpdateDistance();
        if (distance <= 10)
        {
            Move(player.transform.position);
            if (distance <= 3)
            {
                Attack(player.transform.position);
            }
           
        }
        else
        {
            IdelSkeleton();            
        }


    }

    // Skeleton Movement
    public void Move(Vector3 target)
    {        
        // Less than 2 break out.
        if (distance < 2)
        {            
            return;
        }
        // Move toward player with AI mesh.
        agent.SetDestination(target);
        WalkingAnim();        
    }   

    // Attack function for mob, attack player on 3 second time delay dealing 5 damage to the player.    
    public void Attack(Vector3 target)
    {
        if (player != null)
        {
            time += Time.deltaTime;

            if (time > tdelay)
            {
                tdelay = time + 3;
                float check = Vector3.Distance(transform.position, player.transform.position);
                if (check <= 3)
                {
                    AttackingAnim();
                    player.CurrentHealth -= 5;

                }
            }
        }
    }

    private void WalkingAnim()
    {
        skelAnimator.SetBool("isWalking", true);
        skelAnimator.SetBool("isAttacking", false);
        skelAnimator.SetBool("isIdle", false);
    }

    private void AttackingAnim()
    {
        skelAnimator.SetBool("isWalking", false);
        skelAnimator.SetBool("isAttacking", true);
        skelAnimator.SetBool("isIdle", false);
    }

    private void IdelSkeleton()
    {
        skelAnimator.SetBool("isWalking", false);
        skelAnimator.SetBool("isAttacking", false);
        skelAnimator.SetBool("isIdle", true);
    }

    private void UpdateDistance()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
    }

    public float CalcHealthBar(float mHealth, float curHealth)
    {
        float calcHealth = curHealth / mHealth;
        return calcHealth;
    }

    public void SetHealthBar(float myHealth)
    {
        // myHealth value needs to be between 0 and 1
        if (myHealth < 0)
        {
            myHealth = 0;
        }
        healthBar.transform.localScale = new Vector3(myHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }   
}

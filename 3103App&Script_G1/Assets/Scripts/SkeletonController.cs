using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour {

    public float maxHealth = 50;
    public int speed = 3;
    public PlayerController player;
    private float time;
    private float tdelay = 0f;
    public Animator skelAnimator;


    public float currentHealth = 0f;
    public GameObject healthBar;
    public GameObject healthframe;
    public Camera gameCamera;

    public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        skelAnimator = GetComponent<Animator>();
        currentHealth = maxHealth;
        gameCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

	void Update ()
    {
        if(currentHealth != maxHealth)
        {
            float calhealth = CalcHealthBar(maxHealth, currentHealth);
            SetHealthBar(calhealth);
        }

        // Point the health frames at the camera so they can clearly be seen.
        healthframe.transform.LookAt(healthframe.transform.position + gameCamera.transform.rotation * Vector3.back, gameCamera.transform.rotation * Vector3.up);
	}

    // Skeleton Movement - Look at player and move toward him
    public void Move(Vector3 target)
    {
        float sd = speed * Time.deltaTime;        
        transform.LookAt(player.transform.position);      

        // Check the distance to the player, if within 3 units hold position
        if (Vector3.Distance(transform.position, player.transform.position) >= 3)
        {            
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, sd);
            WalkingAnim();
        }
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
                    skelAnimator.SetBool("isAttacking", true);
                    player.CurrentHealth -= 5;
                }
            }
        }
    }

    private void WalkingAnim()
    {
        if (Vector3.Distance(transform.position, player.transform.position) >= 3)
        {
            skelAnimator.SetFloat("isWalking 0", 1);
        }
        else
        {
            skelAnimator.SetFloat("isWalking 0", 0);
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;

    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floormask;
    float camRayLength = 100f;
    public bool control2 = false;
    public GameObject shot;
    public Transform shotspawn;
    public GameObject healthBar;
    public float currentHealth;
    public float maxHealth;
    public string ammotype;

    public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
    public string AmmoType { get { return ammotype; } set { ammotype = value; } }
    public GameObject Shot { get { return shot; } set { shot = value; } }

    void Awake()
    {
        // Gets the references 
        floormask = LayerMask.GetMask("Ground");       
        playerRigidbody = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        ammotype = "bullet";
    }

    private void Update()
    {
        // Shoot bullet on mouseclick
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot(shot, shotspawn);
        }
        if (currentHealth != maxHealth)
        {
            float calhealth = CalcHealthBar(maxHealth, currentHealth);
            SetHealthBar(calhealth);
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                this.gameObject.SetActive(false);
            }
        }
    }

    void FixedUpdate()
    {
        float h = 0;
        float v = 0;
        if (control2 == false)
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }

        // Second Control structure, enabled via toggle.
        if(control2 == true)
        {
             h = Input.GetAxisRaw("F_MoveUp");
             v = Input.GetAxisRaw("T_MoveUp");
        }

        // TODO - Some work for player movement, needs more refinement. States not transitioning smooth, look for alternatives.
        if (h != 0 || v != 0)
        {
            anim.SetBool("isWalking", true);
        }
        if (h == 0 || v == 0)
        {
            anim.SetBool("isWalking", false);
        }

        Move(h, v);       
        Turning();        
    }


    // Keyboard control function
    void Move(float h, float v)
    {
        // Defining move from input
        movement.Set(h, 0f, v);
        // Movement normalized so player doesnt have advatange moving sideways.
        movement = movement.normalized * speed * Time.deltaTime;
        // Move the player.
        playerRigidbody.MovePosition(transform.position + movement);
    }

    // Control turning to follow mouse
    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floormask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    // Spawn bullet
    public void Shoot(GameObject s, Transform a)
    {
        Instantiate(s, a.transform.position, a.transform.rotation);
    }

    // Swap Controls via UI button ingame.
    public void Toggle_Changed(bool newValue)
    {
        if (control2 != newValue)
        {
            control2 = newValue;
        }        
    }   

    // TODO 
    // Move this function to own script since it's repeated in SkeletonManager
    public float CalcHealthBar(float mHealth, float curHealth)
    {
        float calcHealth = curHealth / mHealth;
        return calcHealth;
    }

    public void SetHealthBar(float myHealth)
    {
        if(myHealth < 0)
        {
            myHealth = 0;
        }
        // myHealth value needs to be between 0 and 1
        healthBar.transform.localScale = new Vector3(myHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}


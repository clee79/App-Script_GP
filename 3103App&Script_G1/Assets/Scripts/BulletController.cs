using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float speed = 25f;
    public float factor = 1;
    public float time = 5f;
    PlayerController player;
    SkeletonController skeleton;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed * factor;        
        BulletTravel(Time);
    }

    // Use these to modify bullet speed for different types of ammo
    public float Speed { get { return speed; } set { speed = value; } }
    public float Factor { get { return factor; } set { factor = value; } }
    public float Time { get { return time; } set { time = value; } }

    private void DestroybyTime()
    {
        Destroy(gameObject);
    }

    // Destroy object if nothing hit and time passes.
    public void BulletTravel(float i)
    {
        Destroy(gameObject, i);
    }

    // Bullet destruction on hit.
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" )
        {
            skeleton = other.gameObject.GetComponent<SkeletonController>();
            skeleton.CurrentHealth -= 10;
            if(skeleton.CurrentHealth == 0)
            {
                Destroy(other.gameObject);
            }
            Destroy(this.gameObject);
        }
        
        if (other.gameObject.tag == "Building")
        {
            Destroy(this.gameObject);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    public float speed;
    private Vector3 pullDirection;
    private float pullStrength;

    public float strength = 0.1f;

    private Vector3 bulletVelocity;

    // Variable that shrinks with time (so that individual bullets don't loop around the gravity well)
    private float exitRadius;
    private float exitTime;

    private bool canLeave = false;

    GameObject gravityWell;

    // Initializing values for variables
    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward;
        bulletVelocity = GetComponent<Rigidbody>().velocity;
        pullDirection = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Moving and interactions with gravity wells
    void Update()
    {
        if (gravityWell != null)
        {
            exitTime = Time.deltaTime;
            pullDirection = (gravityWell.transform.position - transform.position) 
                            / Vector3.Distance(gravityWell.transform.position, transform.position);
            pullStrength = strength / Mathf.Pow(Vector3.Distance(gravityWell.transform.position, transform.position), 2.0f);
            //pullStrength = strength / Vector3.Distance(gravityWell.transform.position, transform.position);
            pullDirection *= pullStrength;

            bulletVelocity += pullDirection;

            // exitRadius shrinks as time goes by, so that the bullets and aim line leave the gravity well before they loop around
            //exitRadius -= exitRadius * exitTime;

            if(Vector3.Distance(gravityWell.transform.position, transform.position) <= 1.0f)
            {
                canLeave = true;
            }

            if (Vector3.Distance(gravityWell.transform.position, transform.position) >= exitRadius || (Vector3.Distance(gravityWell.transform.position, transform.position) >= 1.0f && canLeave))
            {
                gravityWell = null;
                exitTime = 0.0f;
            }
        }
        GetComponent<Rigidbody>().velocity = (bulletVelocity) * speed;
    }

    // On entering gravity field, identify that gravity well as the gravityWell gameObject
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gravity Well")
        {
            gravityWell = other.gameObject;
            exitRadius = other.GetComponent<SphereCollider>().radius;
            exitTime = 0.0f;
        }

        if(other.tag == "Draggable")
        {
            other.GetComponent<Rigidbody>().AddForce()
        }
    }

    /*
    // On leaving gravity field, set gravityWell to null (so it can enter another one)
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Gravity Well")
        {
            gravityWell = null;
        }
    }
    */
}

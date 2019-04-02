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
            pullDirection = (gravityWell.transform.position - transform.position) 
                            / Vector3.Distance(gravityWell.transform.position, transform.position);
            pullStrength = strength / Mathf.Pow(Vector3.Distance(gravityWell.transform.position, transform.position), 2.0f);
            pullDirection *= pullStrength;

            bulletVelocity += pullDirection;

            if(Vector3.Distance(gravityWell.transform.position, transform.position) >= gravityWell.GetComponent<SphereCollider>().radius)
            {
                gravityWell = null;
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

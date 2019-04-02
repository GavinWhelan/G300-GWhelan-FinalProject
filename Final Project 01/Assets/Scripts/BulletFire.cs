using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    public float speed;
    private Vector3 pullDirection;
    private float pullStrength;

    public float strength = 1.0f;

    private Vector3 bulletVelocity;

    GameObject gravityWell;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward;
        bulletVelocity = GetComponent<Rigidbody>().velocity;
        pullDirection = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (gravityWell != null)
        {
            pullDirection = (gravityWell.transform.position - transform.position);
            pullStrength = strength / Mathf.Pow(Vector3.Distance(gravityWell.transform.position, transform.position), 2.0f);
            pullDirection *= pullStrength;

            bulletVelocity += pullDirection;
        }
        GetComponent<Rigidbody>().velocity = (bulletVelocity) * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gravity Well")
        {
            gravityWell = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Gravity Well")
        {
            gravityWell = null;
        }
    }
}

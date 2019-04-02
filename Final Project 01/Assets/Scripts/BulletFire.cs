using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    public float speed;
    private Vector3 pullDirection;

    private Vector3 bulletVelocity;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward;
        bulletVelocity = GetComponent<Rigidbody>().velocity;
        pullDirection = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        bulletVelocity += pullDirection;
        GetComponent<Rigidbody>().velocity = (bulletVelocity) * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gravity Well")
        {
            pullDirection = other.transform.position - transform.position;
            pullDirection *= 0.1f;
        }
    }
}

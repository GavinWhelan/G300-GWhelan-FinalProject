using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collideable : MonoBehaviour
{
    private bool grabbed;
    public float speed;

    private void Start()
    {
        grabbed = false;
    }

    // If linked, moves the partner in relation to the player movement 
    private void FixedUpdate()
    {
        if (grabbed == true)
        {
            Vector3 movement;

            if (Vector3.Distance(GameObject.Find("Hold Spot").transform.position, GetComponent<Transform>().position) >= 0.0f)
            {
                movement = GameObject.Find("Hold Spot").transform.position - GetComponent<Transform>().position;
            }
            else
            {
                movement = GetComponent<Transform>().position - GameObject.Find("Hold Spot").transform.position;
            }

            GetComponent<Rigidbody>().AddForce(movement * speed);
        }
    }
}

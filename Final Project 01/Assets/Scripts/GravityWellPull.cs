using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWellPull : MonoBehaviour
{
    public float strength;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        strength = 1.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Rigidbody>().velocity = transform.forward * 0.1f;
    }
}

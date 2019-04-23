using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeDamper : MonoBehaviour
{
    public float damp = 0.1f;

    private void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3 (GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y*(1.0f - damp), GetComponent<Rigidbody>().velocity.y);
    }
}

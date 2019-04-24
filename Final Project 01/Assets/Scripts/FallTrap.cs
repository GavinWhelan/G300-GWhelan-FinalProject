using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrap : MonoBehaviour
{
    public bool playerDead = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerDead = true;
            other.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            other.transform.rotation = Quaternion.identity;
            playerDead = false;
        }

        if(other.tag == "Draggable")
        {
            other.transform.position = other.GetComponent<SpawnPosition>().spawnPosition.transform.position;
            other.transform.rotation = Quaternion.identity;
            other.GetComponent<Rigidbody>().velocity = new Vector3 (0.0f, 0.0f, 0.0f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLock : MonoBehaviour
{
    Transform tempTrans;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Draggable" || other.tag == "Player")
        {
            tempTrans = other.transform.parent;
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Draggable" || other.tag == "Player")
        {
            other.transform.parent = tempTrans;
        }
    }
}

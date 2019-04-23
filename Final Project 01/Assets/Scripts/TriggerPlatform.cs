using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("True");
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("False");
    }
}

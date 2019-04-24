using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public bool pressed = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Draggable")
        {
            pressed = true;
        }
    }
}

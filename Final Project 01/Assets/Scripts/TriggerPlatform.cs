﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlatform : MonoBehaviour
{
    public bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Draggable")
        {
            triggered = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        triggered = false;
    }
}

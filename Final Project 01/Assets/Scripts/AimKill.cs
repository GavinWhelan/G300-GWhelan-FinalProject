using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimKill : MonoBehaviour
{
    public float timeElapsed;

    void Start()
    {
        timeElapsed = 0.0f;
    }
    
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= 2.0f || Input.GetButtonUp("Fire 2"))
        {
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject triggerPlatform01;
    public GameObject triggerPlatform02;
    public GameObject door01;
    public Vector3 doorPosition01;

    public bool door01Trigger01;
    public bool door01Trigger02;

    void Start()
    {
        door01Trigger01 = false;
        door01Trigger02 = false;
    }
    
    void Update()
    {
        Door01();
    }

    void Door01()
    {
        door01Trigger01 = triggerPlatform01.GetComponent<TriggerPlatform>().triggered;
        door01Trigger02 = triggerPlatform02.GetComponent<TriggerPlatform>().triggered;

        if (door01Trigger01 && door01Trigger02)
        {
            door01.transform.position += new Vector3 (0.0f, 0.0f, 10.0f);
            Debug.Log("Yippee!");
        }
    }
}

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
    public bool doorOpen = false;


    public GameObject drainWaterButton;

    public bool waterTrigger;
    public bool waterDrained = false;

    public Animator drainWaterAnim;

    void Start()
    {
        door01Trigger01 = false;
        door01Trigger02 = false;

        waterTrigger = false;

        drainWaterAnim = GameObject.Find("Water Level").GetComponent<Animator>();
    }
    
    void Update()
    {
        Door01();
        DrainWater();
    }

    void Door01()
    {
        door01Trigger01 = triggerPlatform01.GetComponent<TriggerPlatform>().triggered;
        door01Trigger02 = triggerPlatform02.GetComponent<TriggerPlatform>().triggered;

        if (door01Trigger01 && door01Trigger02 && !doorOpen)
        {
            door01.transform.position += new Vector3 (0.0f, 0.0f, 10.0f);
            doorOpen = true;
        }
    }

    void DrainWater()
    {
        waterTrigger = drainWaterButton.GetComponent<ButtonTrigger>().pressed;

        if(waterTrigger && !waterDrained)
        {
            drainWaterAnim.Play("DrainWater");
        }
    }
}

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

    public Animator door01Anim;


    public GameObject drainWaterButton;

    public bool waterTrigger;
    public bool waterDrained = false;

    public Animator waterLevelAnim;

    void Start()
    {
        door01Trigger01 = false;
        door01Trigger02 = false;

        door01Anim = GameObject.Find("Door 01").GetComponent<Animator>();


        waterTrigger = false;

        waterLevelAnim = GameObject.Find("Water Level").GetComponent<Animator>();
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
            door01Anim.Play("OpenDoor01");
            doorOpen = true;
        }
    }

    void DrainWater()
    {
        waterTrigger = drainWaterButton.GetComponent<ButtonTrigger>().pressed;

        if(waterTrigger && !waterDrained)
        {
            waterLevelAnim.Play("DrainWater");
        }
    }
}

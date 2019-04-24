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


    public GameObject triggerPlatform03;

    public bool waterTrigger02;


    public bool waterFilled = false;

    public bool playerDead;
   

    void Start()
    {
        door01Trigger01 = false;
        door01Trigger02 = false;

        door01Anim = GameObject.Find("Door 01").GetComponent<Animator>();


        waterTrigger = false;

        waterLevelAnim = GameObject.Find("Water Level").GetComponent<Animator>();


        waterTrigger02 = false;
    }
    
    void Update()
    {
        Door01();
        DrainWater();
        FillWater();
        //UndoFill();
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

        if (waterTrigger && !waterDrained)
        {
            waterLevelAnim.Play("DrainWater");
            waterFilled = false;
            waterDrained = true;
            drainWaterButton.GetComponent<ButtonTrigger>().pressed = false;
        }
    }

    void FillWater()
    {
        waterTrigger02 = triggerPlatform03.GetComponent<TriggerPlatform>().triggered;

        if (!waterTrigger02 && waterDrained)
        {
            waterLevelAnim.Play("WaterFill");
            waterFilled = true;
            waterDrained = false;
        }
    }

    void UndoFill()
    {
        playerDead = GameObject.Find("Water Level").GetComponent<FallTrap>().playerDead;

        if (waterFilled && playerDead)
        {
            DrainWater();
        }
    }
}

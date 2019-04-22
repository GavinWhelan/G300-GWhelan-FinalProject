using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerUpdated : MonoBehaviour
{
    public float speed = 10.0f;

    public GameObject gun;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Moves the player left, right, forward and backward with arrow keys
        float translation = Input.GetAxis("Vertical") * speed;
        float strafe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        strafe *= Time.deltaTime;

        transform.Translate(strafe, 0, translation);

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wet")
        {
            gun.GetComponent<LaserScript>().push = true;
        }
        if (other.tag == "Electrified")
        {
            gun.GetComponent<LaserScript>().area = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Wet")
        {
            gun.GetComponent<LaserScript>().push = false;
        }
        if(other.tag == "Electrified")
        {
            gun.GetComponent<LaserScript>().area = false;
        }
    }
}


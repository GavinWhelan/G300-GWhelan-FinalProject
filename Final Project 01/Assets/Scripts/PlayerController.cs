using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;

    public float fireRateAim = 0.01f;
    public float nextFireAim = 0.0f;

    public GameObject bullet;
    public GameObject aimLine;
    public Transform shotSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float strafe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        strafe *= Time.deltaTime;

        transform.Translate(strafe, 0, translation);

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        /*
        if (Input.GetButton("Fire2") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bullet, shotSpawn.position, shotSpawn.rotation);
            // GetComponent<AudioSource>().Play();
        }
        */

        if (Input.GetButton("Fire1") && Time.time > nextFireAim)
        {
            nextFireAim = Time.time + fireRateAim;
            Instantiate(aimLine, shotSpawn.position, shotSpawn.rotation);
        }
    }
}

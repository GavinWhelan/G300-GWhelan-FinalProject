using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGunPlaCon : MonoBehaviour
{
    public float speed = 10.0f;
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;
    
    public float fireRateAim = 0.01f;
    public float nextFireAim = 0.0f;
    
    public GameObject bullet;
    public GameObject aimLine;
    public Transform shotSpawn;
    public GameObject gun;
    public GameObject aimPointObject;

    public Transform aimSight;

    private bool isFiring = false;
    private Dictionary<int, Vector3> AimPoints = new Dictionary<int, Vector3>();
    public int maxCount = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        LineRenderer line = shotSpawn.GetComponent<LineRenderer>();
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

        // Fire a single projectile and log the position of that projectile every frame

        if (isFiring && AimPoints.Count <= maxCount)
        {
            AimPoints.Add(AimPoints.Count + 1, aimLine.GetComponent<Transform>().position);
            
        }

        if (AimPoints.Count > -1)
        {
            Debug.Log(AimPoints);
        }

        if (Input.GetButton("Fire1") && AimPoints.Count == 0)
        {
            Instantiate(aimLine, shotSpawn.position, shotSpawn.rotation);
            isFiring = true;
        }

        if (!Input.GetButton("Fire1"))
        {
            isFiring = false;
            AimPoints.Clear();
        }

        

       /*
        if (Input.GetButton("Fire1") && Time.time > nextFireAim)
        {
            nextFireAim = Time.time + fireRateAim;
            Instantiate(aimLine, shotSpawn.position, shotSpawn.rotation);
        }
        */
    }
}

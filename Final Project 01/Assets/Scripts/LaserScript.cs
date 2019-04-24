using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    LineRenderer line;
    public float speed;
    public bool push;
    public bool area;

    public float pullSpeed = 10.0f;
    public float pushSpeed = 10.0f;

    public float areaRadius = 20.0f;

    private GameObject target;

    public GameObject lightningEffect;
    public GameObject player;

    public bool lineEnabled;
    public Vector3 lineStart;
    public Vector3 lineEnd;

    private int launchFactor = 1;

    void Start()
    {
        player = GameObject.Find("Player");

        line = gameObject.GetComponent<LineRenderer>();
        line.enabled = false;
        lineEnabled = line.enabled;

        // Determine the mode of the gun--default is pull/beam (!push/!area).
        push = false;
        area = false;
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // Toggles direction and intensity of push/pull for when the coroutine runs
            if (push)
            {
                speed = pullSpeed;
                launchFactor = -1;
            }
            else
            {
                speed = pushSpeed;
                launchFactor = 1;
            }
            StopCoroutine("FireLaser");
            StartCoroutine("FireLaser");
        }
    }

    IEnumerator FireLaser()
    {
        line.enabled = true;
        lineEnabled = line.enabled;
        Vector3 movement;

        // When fire1 is pressed
        while (Input.GetButton("Fire1"))
        {
            // In even of an area effect
            if (area == true)
            {
                // Find the colliders of all nearby elements, and, if they are draggable, pull them to the hold spot
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, areaRadius);
                foreach (Collider hitCollider in hitColliders)
                {
                    if (hitCollider.GetComponentInParent<Rigidbody>() != null && hitCollider.transform.gameObject.tag == "Draggable")
                    {
                        movement = GameObject.Find("Hold Spot").transform.position - hitCollider.GetComponentInParent<Transform>().position;

                        hitCollider.GetComponentInParent<Rigidbody>().velocity = movement * speed;
                    }
                }
                // If there are objects being pulled, turn on the beam
                if (hitColliders != null)
                {
                    line.enabled = true;
                    lineEnabled = line.enabled;
                    line.material.mainTextureOffset = new Vector2(0, Time.time);
                    line.SetPosition(0, transform.position);
                    line.SetPosition(1, GameObject.Find("Hold Spot").transform.position);
                    lineStart = line.GetPosition(0);
                    lineEnd = line.GetPosition(1);


                }
            }
            else // In event of a beam effect
            {
                line.material.mainTextureOffset = new Vector2(0, Time.time);

                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;

                line.SetPosition(0, ray.origin);
                lineStart = line.GetPosition(0);

                if (Physics.Raycast(ray, out hit, 100))
                {
                    line.SetPosition(1, hit.point);
                    lineEnd = line.GetPosition(1);
                    if (hit.rigidbody)
                    {
                        if (target == null && hit.transform.gameObject.tag == "Draggable") {
                            target = hit.transform.gameObject;
                        }
                    } else if(hit.transform.gameObject.tag == "Launch")
                    {
                        player.GetComponent<Rigidbody>().AddForce(launchFactor * transform.forward, ForceMode.Impulse);
                    }
                }
                else
                {
                    if (target != null)
                    {
                        line.SetPosition(1, target.transform.position);
                        lineEnd = line.GetPosition(1);
                    }
                    else
                    {
                        line.SetPosition(1, ray.GetPoint(100));
                        lineEnd = line.GetPosition(1);
                    }
                }
                if (target != null)
                {
                    if (push)
                    {
                        movement = transform.forward;
                    }
                    else
                    {
                        Vector3 destination = GameObject.Find("Hold Spot").transform.position;
                        Vector3 carriedObject = target.transform.position;
                        movement = (destination - carriedObject);
                    }
                    target.GetComponent<Rigidbody>().velocity = movement * speed;
                }
            }
            yield return null;
        }
        target = null;
        line.enabled = false;
        lineEnabled = line.enabled;
    }


}

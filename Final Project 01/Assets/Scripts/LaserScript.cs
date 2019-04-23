using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    LineRenderer line;
    public float speed;
    public bool push;
    public bool area;

    public float pullSpeed = 5.0f;
    public float pushSpeed = 10.0f;

    private GameObject target;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.enabled = false;

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
            }
            else
            {
                speed = pushSpeed;
            }
            StopCoroutine("FireLaser");
            StartCoroutine("FireLaser");
        }
    }

    IEnumerator FireLaser()
    {
        line.enabled = true;
        Vector3 movement;

        // When fire1 is pressed
        while (Input.GetButton("Fire1"))
        {
            // In even of an area effect
            if (area == true)
            {
                // Find the colliders of all nearby elements, and, if they are draggable, pull them to the hold spot
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10.0f);
                foreach (Collider hitCollider in hitColliders)
                {
                    if (hitCollider.GetComponentInParent<Rigidbody>() != null && hitCollider.transform.gameObject.tag == "Draggable")
                    {
                        movement = GameObject.Find("Hold Spot").transform.position - hitCollider.GetComponentInParent<Transform>().position;

                        hitCollider.GetComponentInParent<Rigidbody>().velocity = movement * speed * 0.25f;
                    }
                }
                // If there are objects being pulled, turn on the beam
                if (hitColliders != null)
                {
                    line.enabled = true;
                    line.material.mainTextureOffset = new Vector2(0, Time.time);
                    line.SetPosition(0, transform.position);
                    line.SetPosition(1, GameObject.Find("Hold Spot").transform.position);
                }
            }
            else // In event of a beam effect
            {
                line.material.mainTextureOffset = new Vector2(0, Time.time);

                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;

                line.SetPosition(0, ray.origin);

                if (Physics.Raycast(ray, out hit, 100))
                {
                    line.SetPosition(1, hit.point);
                    if (hit.rigidbody)
                    {
                        if (target == null) {
                            target = hit.transform.gameObject;
                            Debug.Log("Gotcha!");
                            Debug.Log(hit.transform.gameObject.tag);
                        }

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
                else
                {
                    line.SetPosition(1, ray.GetPoint(100));
                }
            }
            yield return null;
        }
        target = null;
        line.enabled = false;
    }


}

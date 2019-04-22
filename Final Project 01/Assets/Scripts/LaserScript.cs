using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    LineRenderer line;
    public float speed;
    public bool push;
    public bool area;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.enabled = false;
        speed = 10.0f;

        // Determine the mode of the gun--default is pull/beam (!push/!area).
        push = false;
        area = false;
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (area)
            {
                StopCoroutine("FireArea");
                StartCoroutine("FireArea");
            }
            else
            {
                // Toggles direction and intensity of push/pull for when the coroutine runs
                if (push)
                {
                    speed = 5.0f;
                }
                else
                {
                    speed = 10.0f;
                }
                StopCoroutine("FireLaser");
                StartCoroutine("FireLaser");
            }
        }
    }

    IEnumerator FireArea()
    {
        Vector3 movement;

        while (Input.GetButton("Fire1"))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10.0f);
            foreach(Collider hitCollider in hitColliders)
            {
                if(hitCollider.GetComponentInParent<Rigidbody>() != null)
                {
                    movement = GameObject.Find("Hold Spot Area").transform.position - hitCollider.GetComponentInParent<Transform>().position;

                    hitCollider.GetComponentInParent<Rigidbody>().velocity = movement * speed * 0.5f;
                }
            }
            yield return null;
        }
    }

    IEnumerator FireLaser()
    {
        line.enabled = true;
        Vector3 movement;

        while (Input.GetButton("Fire1"))
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
                    if (push)
                    {
                        movement = transform.transform.forward;
                    }
                    else
                    {
                        movement = GameObject.Find("Hold Spot").transform.position - hit.transform.position;
                    }

                    hit.rigidbody.velocity = movement * speed;
                }
            }
            else
            {
                line.SetPosition(1, ray.GetPoint(100));
            }

            yield return null;
        }

        line.enabled = false;
    }


}

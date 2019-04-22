using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    LineRenderer line;
    public float speed;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.enabled = false;
        speed = 50.0f;
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StopCoroutine("FireLaser");
            StartCoroutine("FireLaser");
        }
    }

    IEnumerator FireLaser()
    {
        line.enabled = true;

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
                    Vector3 movement;

                    if (Vector3.Distance(GameObject.Find("Hold Spot").transform.position, hit.transform.position) >= 0.0f)
                    {
                        movement = GameObject.Find("Hold Spot").transform.position - hit.transform.position;
                    }
                    else
                    {
                        movement = hit.transform.position - GameObject.Find("Hold Spot").transform.position;
                    }

                    hit.rigidbody.AddForceAtPosition(transform.forward * -1 * speed, hit.point);
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

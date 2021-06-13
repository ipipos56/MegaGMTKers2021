using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToATarget : MonoBehaviour
{
    public List<Transform> targets;

    public Animator anim;

    private float rotationSpeed = 720f;

    public float speed = .5f;
    private Vector3 velocity;


    private PlayerList plTg;

    private void Awake()
    {
        plTg = GameObject.FindGameObjectWithTag("PlayersList").GetComponent<PlayerList>();
    }

    private void Update()
    {
        targets = plTg.targets;
        if (targets.Count > 0)
        {
            Transform minTarget = targets[0];
            float minDistance = Vector3.Distance(targets[0].position, gameObject.transform.position);
            for (int i = 1; i < targets.Count; i++)
            {
                float tempDistance = Vector3.Distance(targets[i].position, gameObject.transform.position);
                if (minDistance > tempDistance)
                {
                    minDistance = tempDistance;
                    minTarget = targets[i];
                }
            }

            if (minDistance > 0.1f && minDistance < 5f && !anim.GetBool("Died"))
            {
                anim.SetFloat("Speed", 1f);
                transform.position = Vector3.SmoothDamp(transform.position, minTarget.position, ref velocity, speed);

                Vector3 heading = minTarget.position - gameObject.transform.position;
                var distance = heading.magnitude;
                Vector3 movementDirection = heading / distance;
                movementDirection.Normalize();

                if (movementDirection != Vector3.zero)
                {
                    Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

                    transform.rotation =
                        Quaternion.RotateTowards(transform.rotation, toRotation,
                            rotationSpeed * Time.deltaTime);
                }
            }
            else
            {
                anim.SetFloat("Speed", 0f);
            }
        }
        else
        {
            anim.SetFloat("Speed", 0f);
        }
    }
}
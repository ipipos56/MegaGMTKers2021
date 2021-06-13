using System;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;
    public float rotationSpeed = 720;
    Vector2 velocity;

    public Animator anim;

    [Header("Running")] public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    void FixedUpdate()
    {
        // Move.
        IsRunning = canRun && Input.GetKey(runningKey);
        float movingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
            movingSpeed = speedOverrides[speedOverrides.Count - 1]();
        velocity.y = Input.GetAxis("Vertical") * movingSpeed * Time.deltaTime;
        velocity.x = Input.GetAxis("Horizontal") * movingSpeed * Time.deltaTime;
        if (velocity.y != 0 || velocity.x != 0)
            anim.SetFloat("Speed", 1f);
        else
            anim.SetFloat("Speed", 0f);

        Vector3 movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movementDirection.Normalize();
        transform.Translate(movementDirection * (speed * Time.deltaTime), Space.World);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, toRotation,
                    rotationSpeed * Time.deltaTime);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class TakeSecondPlayer : MonoBehaviour
{
    private Vector3 playerPointUp = new Vector3(0, 1.77f, 0.78f);
    private Vector3 playerPointDown = new Vector3(0, 1.77f, -0.78f);
    private Vector3 playerPointLeft = new Vector3(-0.91f, 1.77f, 0f);
    private Vector3 playerPointRight = new Vector3(0.91f, 1.77f, 0f);
    public KeyCode takingPlayerKey = KeyCode.E;
    public KeyCode playerThrowing = KeyCode.Space;
    private bool inSecondPlayer = false;
    private Transform secondPlayer = null;
    private Rigidbody secondPlayerRigidbody = null;
    private CapsuleCollider secondPlayerCapsuleCollider = null;
    private string secondPlayerName = "SecondPerson";
    private bool taked = false;
    private Transform parentOfSecondPlayer;
    public float forceValue;
    private Camera gunCamera;

    private Vector3 velocityOfSecondPlayer;
    private float smoothTime = 0.35f;

    private void Awake()
    {
        gunCamera = GameObject.FindGameObjectWithTag("GunCamera").GetComponent<Camera>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.name == secondPlayerName)
        {
            if (secondPlayer == null)
            {
                secondPlayer = col.transform;

                playerPointUp *= secondPlayer.localScale.x;
                playerPointDown *= secondPlayer.localScale.x;
                playerPointLeft *= secondPlayer.localScale.x;
                playerPointRight *= secondPlayer.localScale.x;

                parentOfSecondPlayer = secondPlayer.parent;
                secondPlayerRigidbody = secondPlayer.GetComponent<Rigidbody>();
                secondPlayerCapsuleCollider = secondPlayer.GetComponent<CapsuleCollider>();
            }

            inSecondPlayer = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.transform.name == secondPlayerName)
        {
            inSecondPlayer = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(takingPlayerKey) && inSecondPlayer && !taked)
        {
            taked = true;
            secondPlayerRigidbody.useGravity = false;
            secondPlayerRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            secondPlayerCapsuleCollider.enabled = false;
            secondPlayer.position = gameObject.transform.position + playerPointUp;
            secondPlayer.parent = gameObject.transform;
        }
        else if (Input.GetKeyDown(takingPlayerKey) && taked)
        {
            taked = false;
            secondPlayerRigidbody.useGravity = true;
            secondPlayerRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            secondPlayerCapsuleCollider.enabled = true;
            secondPlayer.parent = parentOfSecondPlayer;
        }
        else if (Input.GetKeyDown(playerThrowing) && taked)
        {
            taked = false;
            secondPlayerRigidbody.useGravity = true;
            secondPlayerRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            secondPlayerCapsuleCollider.enabled = true;
            secondPlayer.parent = parentOfSecondPlayer;
            secondPlayerRigidbody.AddForce(gunCamera.transform.forward * forceValue);
        }
        else if (taked)
        {
            Vector3 newPosition = secondPlayer.position;
            if (Input.GetAxis("Vertical") > 0.2)
            {
                newPosition = gameObject.transform.position + playerPointUp;
            }
            else if (Input.GetAxis("Vertical") < -0.2)
            {
                newPosition = gameObject.transform.position + playerPointDown;
            }
            else if (Input.GetAxis("Horizontal") > 0.2)
            {
                newPosition = gameObject.transform.position + playerPointRight;
            }
            else if (Input.GetAxis("Horizontal") < -0.2)
            {
                newPosition = gameObject.transform.position + playerPointLeft;
            }

            if (newPosition != secondPlayer.position)
                secondPlayer.position = Vector3.SmoothDamp(secondPlayer.position, newPosition,
                    ref velocityOfSecondPlayer, smoothTime);
        }
    }
}
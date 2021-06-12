using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class TakeSecondPlayer : MonoBehaviour
{
    private Vector3 playerPoint = new Vector3(0,1.77f,0.78f);
    public KeyCode takingPlayerKey = KeyCode.E;
    public KeyCode playerThrowing = KeyCode.Space;
    private bool inSecondPlayer = false;
    private Transform secondPlayer = null;
    private Rigidbody secondPlayerRigidbody = null;
    private CapsuleCollider secondPlayerCapsuleCollider = null;
    private string secondPlayerName = "SecondPerson";
    private bool taked = false;
    private Transform parentOfSecondPlayer;

    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.name == secondPlayerName)
        {
            if (secondPlayer == null)
            {
                secondPlayer = col.transform;
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
            secondPlayer.position = gameObject.transform.position + playerPoint;
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
            
        }
    }
}

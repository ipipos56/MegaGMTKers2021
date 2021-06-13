using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDown : MonoBehaviour
{
    public GameObject objectTemp;
    public Vector3 newPostion;

    private float speed = .5f;
    private Vector3 velocity;

    public bool entered = false;

    public void LateUpdate()
    {
        if (entered)
            MoveObject();
    }

    public void MoveObject()
    {
        if (objectTemp.transform.localPosition != newPostion)
        {
            StartCoroutine(moving());
        }
    }

    private IEnumerator moving()
    {
        yield return new WaitForSeconds(0.2f);
        objectTemp.transform.localPosition =
            Vector3.SmoothDamp(objectTemp.transform.localPosition, newPostion, ref velocity, speed);
    }
}
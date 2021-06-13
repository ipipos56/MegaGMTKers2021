using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingOfWeapon : MonoBehaviour
{
    private Camera mainCamera;

    public bool AmIGrounded = true;

    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (AmIGrounded)
        {
            Vector3 mousePosition = OwnUtils.GetMouseWorldPosition(mainCamera);

            Vector3 aimDirection = (mousePosition - transform.position).normalized;
            float angle = Mathf.Atan2(aimDirection.x, aimDirection.z) * Mathf.Rad2Deg;
            gameObject.transform.eulerAngles = new Vector3(0, angle, 0);
        }
    }
}
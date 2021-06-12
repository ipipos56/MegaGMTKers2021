using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    private WFX_LightFlicker lightScript;
    public GameObject weaponImpact;

    [SerializeField] private GameObject lightWeapon;

    public Camera fpsCam;

    public ParticleSystem muzzleFlash;
    

    private void Awake()
    {
        lightScript = lightWeapon.GetComponent<WFX_LightFlicker>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();
        lightScript.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            else
            {
                Instantiate(weaponImpact, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}
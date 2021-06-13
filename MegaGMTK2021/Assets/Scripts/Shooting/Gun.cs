using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    private WFX_LightFlicker lightScript;
    public GameObject weaponImpact;
    public GameObject weaponImpactBlood;

    [SerializeField] private GameObject lightWeapon;

    public Camera fpsCam;

    public ParticleSystem muzzleFlash;

    private RotatingOfWeapon rot;

    public GameObject playersList;


    private void Awake()
    {
        lightScript = lightWeapon.GetComponent<WFX_LightFlicker>();
        rot = gameObject.GetComponent<RotatingOfWeapon>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Enemy")
        {
            Destroy(gameObject);
            playersList.GetComponent<PlayerList>().GameOver();
        }
        else if (col.transform.tag == "Button")
        {
            col.transform.GetComponent<ButtonDown>().entered = true;
        }
    }
    

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && rot.AmIGrounded)
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
                Instantiate(weaponImpactBlood, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else
            {
                Instantiate(weaponImpact, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}
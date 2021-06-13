using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float Health = 50f;

    public Animator anim;

    public bool deathWorld = false;
    private GameObject playersList;

    private void Awake()
    {
        playersList = GameObject.FindGameObjectWithTag("PlayersList");
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        anim.SetBool("Died", true);
        anim.SetFloat("Speed", 0f);
        StartCoroutine(deleteObject());
    }

    private IEnumerator deleteObject()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        if (deathWorld)
        {
            playersList.GetComponent<PlayerList>().GameOver();
        }
    }
}
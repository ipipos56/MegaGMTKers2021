using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerList : MonoBehaviour
{
    public List<Transform> targets;


    public GameObject exit;
    public GameObject controlsButton;

    public GameObject controls;
    
    public GameObject text;
    public GameObject restartButton;
    public GameObject winText;

    public GameObject enemyList;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Started"))
        {
            controls.gameObject.SetActive(false);
        }
        else
        {
            controls.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Started"))
        {
            StartCoroutine(controlsTimer());
            PlayerPrefs.SetInt("Started", 1);
        }
    }

    private IEnumerator controlsTimer()
    {
        yield return new WaitForSeconds(5f);
        controls.gameObject.SetActive(false);
    }

    private void Update()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == null)
            {
                targets.Remove(targets[i]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (exit.activeSelf)
            {
                exit.gameObject.SetActive(false);
                controlsButton.gameObject.SetActive(false);
            }
            else
            {
                exit.gameObject.SetActive(true);
                controlsButton.gameObject.SetActive(true);
            }
        }

        if (enemyList.transform.childCount <= 0)
        {
            winText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            exit.gameObject.SetActive(true);
            controlsButton.gameObject.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowControls()
    {
        exit.gameObject.SetActive(false);
        controlsButton.gameObject.SetActive(false);
        controls.gameObject.SetActive(true);
        StartCoroutine(controlsTimer());
    }
    
    public void GameOver()
    {
        text.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubPauseMenu : MonoBehaviour
{
    private AudioSource aS;
    public GameObject menuToToggle;
    public bool isMenuToggled;


    // Start is called before the first frame update
    void Start()
    {
        aS = GetComponent<AudioSource>();
        menuToToggle.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isMenuToggled = !isMenuToggled;
            aS.Play();
        }

        if (Input.GetKeyDown(KeyCode.K) && isMenuToggled)
        {
            Debug.Log("K Was Pressed");
            Application.Quit();
        }

        if (isMenuToggled)
        {
            menuToToggle.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            menuToToggle.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
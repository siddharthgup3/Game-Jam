﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMechanic : MonoBehaviour
{

    private GameObject playerObject;
    public Vector3 someStartPosition;
    public Vector3 someStartRotation;

    private void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
    }

    public static void RestartGame()
    {
        Debug.Log(SceneManager.GetActiveScene().name);

        if (SceneManager.GetActiveScene().name != "TutorialScene")
        {
            //Restart the entire game, equivalent to closing and opening game.
            Debug.Log($"Restarting Game");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
        else if (Time.time > 25)
        {
            Debug.Log(Time.time);
            RestartTutorialScene();
        }
    }

    public static void RestartTutorialScene()
    {
        SceneManager.LoadScene("TutorialSceneNoIntro");
    }

    public void RestartLastCheckPoint()
    {
        //Only restart to the last time you triggered the collider for starting the ghost mechanic

        //Reset position. 
        playerObject.transform.position = someStartPosition;
        playerObject.transform.rotation = Quaternion.Euler(someStartRotation);

        //Reset abilities gained

        var playerAbilities = playerObject.GetComponent<PlayerPowerUp>();
        foreach (var abilityKVP in playerAbilities.GetPowerUps)
        {
            //Reset value here.
            playerAbilities.GetPowerUps[abilityKVP.Key] = 0;
        }
    }
}
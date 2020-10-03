using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalTimer : MonoBehaviour
{
    public KeyCode restartKey = KeyCode.R;
    private float timeSinceStart;
    public float maxLevelTime = 15f;
    private static float timeToBuzzer;
    public float timeScaleOfTimer = 1f;
    
    public TextMeshProUGUI txt;
    private void OnEnable()
    {
        timeSinceStart = 0;
        timeToBuzzer = maxLevelTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(restartKey))
        {
            RestartMechanic.RestartGame();
        }
        
        
        timeSinceStart += Time.deltaTime;
        timeToBuzzer -= Time.deltaTime * timeScaleOfTimer;

        txt.text = timeToBuzzer.ToString("F2");
        
        if (timeToBuzzer <= 0)
        {
            LevelNotCompleted();
        }
    }

    private void LevelNotCompleted()
    {
        Debug.Log($"YOU DIDN'T COMPLETE THE LEVEL YOU FUCK");
        RestartMechanic.RestartGame();
    }

    public static void AddTimeThroughPowerUps(float addedTime)
    {
        timeToBuzzer += addedTime;
    }
}

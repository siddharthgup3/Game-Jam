using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintStamina : MonoBehaviour
{
    private float maxPlayerSprintStamina = 100f;
    private float regenTime = 20f;
    public float playerSprintStamina;

    // Start is called before the first frame update
    void Start()
    {
        playerSprintStamina = maxPlayerSprintStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerSprintStamina < maxPlayerSprintStamina)
        {
            playerSprintStamina += regenTime * Time.deltaTime;
        }
    }
}

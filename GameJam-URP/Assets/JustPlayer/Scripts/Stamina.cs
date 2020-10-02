using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    private float maxPlayerStamina = 100f;
    private float regenTime = 25f;
    public float playerStamina;

    // Start is called before the first frame update
    void Start()
    {
        playerStamina = maxPlayerStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerStamina < maxPlayerStamina)
        {
            playerStamina += regenTime * Time.deltaTime;
        }
    }
}

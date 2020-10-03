using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

public enum CollectibleType
{
    SingleJump = 0, //Affects player controller.
    DoubleJump = 2, //Affects player controller.
    IncreaseTime = 1, //Doesn't affect controller
    DecreaseTime = 3 //Doesn't affect controller
}

public class Collectible : MonoBehaviour
{
    [SerializeField] private CollectibleType _collectibleType;
    [SerializeField] private int modifyingCount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if ((int) _collectibleType % 2 == 0) //Affects player controller
            {
                var playerPowerUp = other.GetComponent<PlayerPowerUp>();
                playerPowerUp.UpdateStat(_collectibleType, modifyingCount);

                Destroy(gameObject);
            }
            else
            {
                //Doesn't affect player controller
                if (_collectibleType == CollectibleType.DecreaseTime ||
                    _collectibleType == CollectibleType.IncreaseTime)
                {
                    GlobalTimer.AddTimeThroughPowerUps(modifyingCount);
                }
            }

            Destroy(gameObject);
        }
    }
}
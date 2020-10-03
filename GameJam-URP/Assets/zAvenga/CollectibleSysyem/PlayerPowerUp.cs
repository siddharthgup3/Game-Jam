using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    private Dictionary<CollectibleType, int> availablePowerUps;

    public Dictionary<CollectibleType, int> GetPowerUps => availablePowerUps;

    public void UpdateStat(CollectibleType _colType, int count = 1)
    {
        availablePowerUps[_colType] += count;
        Debug.Log($"{_colType.ToString()} updated by {count}");
    }


    private void Awake()
    {
        availablePowerUps = new Dictionary<CollectibleType, int>
        {
            {CollectibleType.DoubleJump, 0}, {CollectibleType.SingleJump, 0}
        };
    }
}
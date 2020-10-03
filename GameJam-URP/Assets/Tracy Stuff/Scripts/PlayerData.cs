using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerData {

    public List<SerializableVector3> ghost_Wall_1;
    public List<SerializableVector3> ghost_Wall_2;
    public List<SerializableVector3> ghost_Wall_3;

    public List<SerializableVector3> ghost_Kill_1;
    public List<SerializableVector3> ghost_Kill_2;
    public List<SerializableVector3> ghost_Kill_3;

    public PlayerData (GameProgress game)
    {
        ghost_Kill_1 = game.ghost_Kill_1;
        ghost_Kill_2 = game.ghost_Kill_2;
        ghost_Kill_3 = game.ghost_Kill_3;

        ghost_Wall_1 = game.ghost_Wall_1;
        ghost_Wall_2 = game.ghost_Wall_2;
        ghost_Wall_3 = game.ghost_Wall_3;
    }
    
}

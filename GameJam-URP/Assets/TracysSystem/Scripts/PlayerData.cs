using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerData {

    public List<SerializableVector3> ghost_Wall;
   

    public PlayerData (GameProgress game)
    {
        ghost_Wall = game.ghost_Wall;
    }
    
}

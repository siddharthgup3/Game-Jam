using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour {

    public List<SerializableVector3> ghost_Wall;
   
    public void ResetData()
    {
        ghost_Wall = new List<SerializableVector3>();

        SaveSystem.SavePlayer(this);
    }

    private void Awake()
    {
        PlayerData data = SaveSystem.LoadPLayer();
        ghost_Wall = data.ghost_Wall;

    }
    public void OnDisable()
    {
        SaveSystem.SavePlayer(this);
    }
}

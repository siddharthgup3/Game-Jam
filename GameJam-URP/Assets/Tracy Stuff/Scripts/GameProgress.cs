using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoBehaviour {

    public List<SerializableVector3> ghost_Wall_1;
    public List<SerializableVector3> ghost_Wall_2;
    public List<SerializableVector3> ghost_Wall_3;
    
    public List<SerializableVector3> ghost_Kill_1;
    public List<SerializableVector3> ghost_Kill_2;
    public List<SerializableVector3> ghost_Kill_3;

    public void ResetData()
    {
        ghost_Wall_1 = new List<SerializableVector3>();
        ghost_Wall_2 = new List<SerializableVector3>();
        ghost_Wall_3 = new List<SerializableVector3>();

        ghost_Kill_1 = new List<SerializableVector3>();
        ghost_Kill_2 = new List<SerializableVector3>();
        ghost_Kill_3 = new List<SerializableVector3>();
        SaveSystem.SavePlayer(this);
    }

    private void Awake()
    {
        PlayerData data = SaveSystem.LoadPLayer();
        ghost_Wall_1 = data.ghost_Wall_1;
        ghost_Wall_2 = data.ghost_Wall_2;
        ghost_Wall_3 = data.ghost_Wall_3;

        ghost_Kill_1 = data.ghost_Kill_1;
        ghost_Kill_2 = data.ghost_Kill_2;
        ghost_Kill_3 = data.ghost_Kill_3;
    }
    public void OnDisable()
    {
        SaveSystem.SavePlayer(this);
    }
}

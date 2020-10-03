using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomReplay : MonoBehaviour 
{
	public GameProgress game;
    public GameObject posMarker;

    [HideInInspector]
    public int tick;
    public float distanceThreshold = 25;
    int boxTicks;
    bool hasPLayedOnce;
    GameObject lastBox;
    void OnEnable()
    {
        SaveSystem.LoadPLayer();
        tick += 1;
    }
	void FixedUpdate()
	{
       
         transform.position = game.ghost_Wall[tick];

         if (tick < game.ghost_Wall.Count - 1)
         {
             tick += 1;
            boxTicks += 1;
            if (lastBox == null)
            {
                lastBox = Instantiate(posMarker, transform.position, transform.rotation);
            }
            if (Vector3.Distance(game.ghost_Wall[tick], lastBox.transform.position) >= distanceThreshold && !hasPLayedOnce)
            {
                lastBox = Instantiate(posMarker, transform.position, transform.rotation);
            }

         }
         else
         {
             hasPLayedOnce = true;
             tick = 0;
         }       
            
	}

    //check distance between current tick and last box instead of amount of ticks
}

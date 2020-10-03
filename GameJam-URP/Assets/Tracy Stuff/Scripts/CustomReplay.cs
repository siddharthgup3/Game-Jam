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

    public enum GhostType { wall, kill, placeholder2, placeholder3 }
    public int ghostNumber;

    public GhostType ghostType;

    bool hasPlayedOnce;
    GameObject lastBox;
    void OnEnable()
    {
        SaveSystem.LoadPLayer();
        tick += 1;
    }

	void FixedUpdate()
	{
        if (ghostType == GhostType.wall && ghostNumber == 1)
        {
            transform.position = game.ghost_Wall_1[tick];

            if (tick < game.ghost_Wall_1.Count - 1)
            {
                tick += 1;
                if (lastBox == null)
                {
                    lastBox = Instantiate(posMarker, transform.position, transform.rotation);
                }

                //check distance between current tick and last box instead of amount of ticks
                if (Vector3.Distance(game.ghost_Wall_1[tick], lastBox.transform.position) >= distanceThreshold && !hasPlayedOnce)
                {
                    lastBox = Instantiate(posMarker, transform.position, transform.rotation);
                }

            }
            else
            {
                hasPlayedOnce = true;
                tick = 0;
            }
        }
        else if (ghostType == GhostType.wall && ghostNumber == 2)
        {
            //
            transform.position = game.ghost_Wall_2[tick];

            if (tick < game.ghost_Wall_2.Count - 1)
            {
                tick += 1;
                if (lastBox == null)
                {
                    lastBox = Instantiate(posMarker, transform.position, transform.rotation);
                }

                //check distance between current tick and last box instead of amount of ticks
                if (Vector3.Distance(game.ghost_Wall_2[tick], lastBox.transform.position) >= distanceThreshold && !hasPlayedOnce)
                {
                    lastBox = Instantiate(posMarker, transform.position, transform.rotation);
                }

            }
            else
            {
                hasPlayedOnce = true;
                tick = 0;
            }
        }
        else if (ghostType == GhostType.wall && ghostNumber == 3)
        {
            //
            transform.position = game.ghost_Wall_3[tick];

            if (tick < game.ghost_Wall_3.Count - 1)
            {
                tick += 1;
                if (lastBox == null)
                {
                    lastBox = Instantiate(posMarker, transform.position, transform.rotation);
                }

                //check distance between current tick and last box instead of amount of ticks
                if (Vector3.Distance(game.ghost_Wall_3[tick], lastBox.transform.position) >= distanceThreshold && !hasPlayedOnce)
                {
                    lastBox = Instantiate(posMarker, transform.position, transform.rotation);
                }

            }
            else
            {
                hasPlayedOnce = true;
                tick = 0;
            }
        }

        else if (ghostType == GhostType.kill && ghostNumber == 1)
        {
            print("Kill Ghost 1 is playing");
            transform.position = game.ghost_Kill_1[tick];

            if (tick < game.ghost_Kill_1.Count - 1)
            {
                tick += 1;
                if (lastBox == null)
                {
                    lastBox = Instantiate(posMarker, transform.position, transform.rotation);
                }

                //check distance between current tick and last box instead of amount of ticks
                if (Vector3.Distance(game.ghost_Kill_1[tick], lastBox.transform.position) >= distanceThreshold && !hasPlayedOnce)
                {
                    lastBox = Instantiate(posMarker, transform.position, transform.rotation);
                }

            }
            else
            {
                hasPlayedOnce = true;
                tick = 0;
            }
        }
        else if (ghostType == GhostType.kill && ghostNumber == 2)
        {
            //
            transform.position = game.ghost_Kill_2[tick];

            if (tick < game.ghost_Kill_2.Count - 1)
            {
                tick += 1;
                if (lastBox == null)
                {
                    lastBox = Instantiate(posMarker, transform.position, transform.rotation);
                }

                //check distance between current tick and last box instead of amount of ticks
                if (Vector3.Distance(game.ghost_Kill_2[tick], lastBox.transform.position) >= distanceThreshold && !hasPlayedOnce)
                {
                    lastBox = Instantiate(posMarker, transform.position, transform.rotation);
                }

            }
            else
            {
                hasPlayedOnce = true;

            }
        }
        else if (ghostType == GhostType.kill && ghostNumber == 3)
        {
            //
            transform.position = game.ghost_Kill_3[tick];

            if (tick < game.ghost_Kill_3.Count - 1)
            {
                tick += 1;
                if (lastBox == null)
                {
                    lastBox = Instantiate(posMarker, transform.position, transform.rotation);
                }

                //check distance between current tick and last box instead of amount of ticks
                if (Vector3.Distance(game.ghost_Kill_3[tick], lastBox.transform.position) >= distanceThreshold && !hasPlayedOnce)
                {
                    lastBox = Instantiate(posMarker, transform.position, transform.rotation);
                }

            }
            else
            {
                hasPlayedOnce = true;

            }
        }
        
            
	}

   
}

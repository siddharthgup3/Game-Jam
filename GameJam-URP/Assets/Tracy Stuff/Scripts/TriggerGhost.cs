using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGhost : MonoBehaviour
{
    RecordTransformCustom record;
    GameProgress game;

    [Tooltip("assign CustomReplay.cs to a gameobject in your scene and assign it here")]
    public GameObject ghost;
    public enum GhostType { wall, kill, placeholder2, placeholder3 }
    public int ghostNumber;

    public GhostType ghostType;
    private void OnEnable()
    {
        game = FindObjectOfType<GameProgress>();
        record = FindObjectOfType<RecordTransformCustom>();
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (ghostType == GhostType.wall && ghostNumber == 1)
            {
                game.ghost_Wall_1 = record.positions;
            }
            else if (ghostType == GhostType.wall && ghostNumber == 2)
            {
                game.ghost_Wall_2 = record.positions;
            }
            else if (ghostType == GhostType.wall && ghostNumber == 3)
            {
                game.ghost_Wall_3 = record.positions;
            }

            else if(ghostType == GhostType.kill && ghostNumber == 1)
            {
                game.ghost_Kill_1 = record.positions;
            }
            else if (ghostType == GhostType.kill && ghostNumber == 2)
            {
                game.ghost_Kill_2 = record.positions;
            }
            else if (ghostType == GhostType.kill && ghostNumber == 3)
            {
                game.ghost_Kill_3 = record.positions;
            }
            record.NewGhost(ghostType.GetHashCode(), ghostNumber);
            SaveSystem.SavePlayer(game);
            ghost.SetActive(true);
        }
    }
}

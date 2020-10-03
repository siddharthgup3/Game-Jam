using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGhost : MonoBehaviour
{
    RecordTransformCustom record;
    GameProgress game;

    [Tooltip("assign CustomReplay.cs to a gameobject in your scene and assign it here")]
    public GameObject ghost;
    public enum GhostType { wall, placeholder1, placeholder2, placeholder3 }

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
            if (ghostType == GhostType.wall)
            {
                game.ghost_Wall = record.positions;
            }
            record.NewGhost();
        }
    }
}

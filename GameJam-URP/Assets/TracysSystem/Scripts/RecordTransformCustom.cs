using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct SerializableVector3
{
    /// <summary>
    /// x component
    /// </summary>
    public float x;

    /// <summary>
    /// y component
    /// </summary>
    public float y;

    /// <summary>
    /// z component
    /// </summary>
    public float z;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="rX"></param>
    /// <param name="rY"></param>
    /// <param name="rZ"></param>
    public SerializableVector3(float rX, float rY, float rZ)
    {
        x = rX;
        y = rY;
        z = rZ;
    }

    /// <summary>
    /// Returns a string representation of the object
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return string.Format("[{0}, {1}, {2}]", x, y, z);
    }

    /// <summary>
    /// Automatic conversion from SerializableVector3 to Vector3
    /// </summary>
    /// <param name="rValue"></param>
    /// <returns></returns>
    public static implicit operator Vector3(SerializableVector3 rValue)
    {
        return new Vector3(rValue.x, rValue.y, rValue.z);
    }

    /// <summary>
    /// Automatic conversion from Vector3 to SerializableVector3
    /// </summary>
    /// <param name="rValue"></param>
    /// <returns></returns>
    public static implicit operator SerializableVector3(Vector3 rValue)
    {
        return new SerializableVector3(rValue.x, rValue.y, rValue.z);
    }
}
public class RecordTransformCustom : MonoBehaviour 
{
    public GameObject ghost;

    public GameProgress game;

	[HideInInspector]
	public int currentTick = 0;
	public List<SerializableVector3> positions;
    [HideInInspector]
	public bool isRecording = true;

    void OnEnable () 
	{
        PlayerData data = SaveSystem.LoadPLayer();
	}
    void FixedUpdate()
    {
        if (isRecording)
        {
            positions.Add(transform.position);
        }
        else
        {
            /*
            foreach (SerializableVector3 pos in positions)
            {
                Instantiate(recentPosMarker, pos, transform.rotation);
            }
            */
            positions.Clear();
            isRecording = true;
        }
    }
    public void NewGhost()
    {
        isRecording = false;

        game.ghost_Wall = new List<SerializableVector3>(positions);
       
        ghost.SetActive(true);

        positions.Clear();
        isRecording = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct SerializableVector3 {
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
    public SerializableVector3(float rX, float rY, float rZ) {
        x = rX;
        y = rY;
        z = rZ;
    }

    /// <summary>
    /// Returns a string representation of the object
    /// </summary>
    /// <returns></returns>
    public override string ToString() {
        return string.Format("[{0}, {1}, {2}]", x, y, z);
    }

    /// <summary>
    /// Automatic conversion from SerializableVector3 to Vector3
    /// </summary>
    /// <param name="rValue"></param>
    /// <returns></returns>
    public static implicit operator Vector3(SerializableVector3 rValue) {
        return new Vector3(rValue.x, rValue.y, rValue.z);
    }

    /// <summary>
    /// Automatic conversion from Vector3 to SerializableVector3
    /// </summary>
    /// <param name="rValue"></param>
    /// <returns></returns>
    public static implicit operator SerializableVector3(Vector3 rValue) {
        return new SerializableVector3(rValue.x, rValue.y, rValue.z);
    }
}
public class RecordTransformCustom : MonoBehaviour {

    public GameProgress game;

    [HideInInspector]
    public int currentTick = 0;
    public List<SerializableVector3> positions;

    public GameObject startingPadLight1;
    public GameObject startingPadLight2;
    public Material waitingMaterial;
    public Material startedMaterial;
    public AudioClip startedBuzzer;

    private bool hasEntered = false;
    private bool isRecording = false;

    void OnEnable() {
        PlayerData data = SaveSystem.LoadPLayer();
    }

    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("RecordStarter")) {

            if (hasEntered == false) {
                hasEntered = true;
            }
        }
    }

    private void OnTriggerExit(Collider other) {

        if (other.CompareTag("RecordStarter")) {

            if (hasEntered == true && isRecording == false) {
                isRecording = true;
                SoundController.instance.PlayClip(startedBuzzer);
                startingPadLight1.GetComponent<MeshRenderer>().material = startedMaterial;
                startingPadLight2.GetComponent<MeshRenderer>().material = startedMaterial;
            }
        }
    }

    void FixedUpdate() {

        if (isRecording == true) {
            positions.Add(transform.position);

        } else {
            positions.Clear();
        }
    }

    public void NewGhost(int type, int num) {
        startingPadLight1.GetComponent<MeshRenderer>().material = waitingMaterial;
        startingPadLight2.GetComponent<MeshRenderer>().material = waitingMaterial;
        isRecording = false;
        type += 1;

        print("New Ghost type = " + type + " amd num = " + num);


        if (type == 1 && num == 1) {
            print("Ghost wall 1 overWritten");

            game.ghost_Wall_1 = new List<SerializableVector3>(positions);

        } else if (type == 1 && num == 2) {
            game.ghost_Wall_2 = new List<SerializableVector3>(positions);

        } else if (type == 1 && num == 3) {
            game.ghost_Wall_3 = new List<SerializableVector3>(positions);

        } else if (type == 2 && num == 1) {
            print("Ghost Kill 1 overWritten");
            game.ghost_Kill_1 = new List<SerializableVector3>(positions);

        } else if (type == 2 && num == 2) {
            game.ghost_Kill_2 = new List<SerializableVector3>(positions);

        } else if (type == 2 && num == 3) {
            game.ghost_Kill_3 = new List<SerializableVector3>(positions);
        }

        positions.Clear();
    }
}

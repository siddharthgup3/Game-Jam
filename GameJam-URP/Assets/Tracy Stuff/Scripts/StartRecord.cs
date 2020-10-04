using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRecord : MonoBehaviour
{
    RecordTransformCustom rec;
    private void OnEnable()
    {
        rec = FindObjectOfType<RecordTransformCustom>();
    }
    private void OnTriggerExit(Collider col)
    {
        rec.isRecording = true;
    }
}

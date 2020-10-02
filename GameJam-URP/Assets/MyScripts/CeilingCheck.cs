using UnityEngine;

public class CeilingCheck : MonoBehaviour
{
    public bool obstacleOverhead;

    private void OnTriggerEnter(Collider other)
    {
        obstacleOverhead = true;
    }

    private void OnTriggerExit(Collider other)
    {
        obstacleOverhead = false;
    }
}
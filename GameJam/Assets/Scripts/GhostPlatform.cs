using UnityEngine;


public class GhostPlatform : MonoBehaviour
{

    public GameObject solid;

    public void Solidify()
    {
        solid.SetActive(true);
    }
}
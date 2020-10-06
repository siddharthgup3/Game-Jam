using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCounter : MonoBehaviour
{
    public int portalsInLevel;
    public int portalCount;
    public GameObject winElement;


    // Start is called before the first frame update
    void Start()
    {
        portalCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (portalCount >= portalsInLevel)
        {
            winElement.SetActive(true);
        }
    }
}

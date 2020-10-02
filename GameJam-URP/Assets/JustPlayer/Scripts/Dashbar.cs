using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dashbar : MonoBehaviour
{
    public Stamina playerStamina;
    private Image dashBarFill;

    // Start is called before the first frame update
    void Start()
    {
        dashBarFill = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        // shouldn't be calling this every update
        dashBarFill.fillAmount = playerStamina.playerStamina / 100;
    }
}

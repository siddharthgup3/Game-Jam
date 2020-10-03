using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public SprintStamina playerStamina;
    private Image sprintBarFill;

    // Start is called before the first frame update
    void Start()
    {
        sprintBarFill = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        // shouldn't be calling this every update
        sprintBarFill.fillAmount = playerStamina.playerSprintStamina / 100;
    }
}

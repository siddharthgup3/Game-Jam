using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update


    private AudioSource audioSrc;
    public AudioClip hoverAudio;
    public AudioClip clickAudio;

    public Button topButton;
    public Button middleButton;
    public Button exitButton;

    public Image top;
    public Image middle;
    public Image bottom;

    public RawImage topBG;
    public RawImage middleBG;
    public RawImage bottomBG;

    private Color topColor;
    private Color middleColor;
    private Color bottomColor;

    public Color topColorHover;
    public Color middleColorHover;
    public Color bottomColorHover;



    void Start()
    {

        topColor = topBG.color;
        middleColor = middleBG.color;
        bottomColor = bottomBG.color;


        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }






    public void ScaleTop()
    {
        topBG.color = topColorHover;
        top.transform.localScale = new Vector3(1.02f, 1,1);
    }
    public void ScaleMiddle()
    {
        middleBG.color = middleColorHover;
        middle.transform.localScale = new Vector3(1.02f, 1, 1);
    }
    public void ScaleBottom()
    {
        bottomBG.color = bottomColorHover;
        bottom.transform.localScale = new Vector3(1.02f, 1, 1);
    }


    public void UnscaleAll()
    {
        top.transform.localScale = new Vector3(1, 1, 1);
        middle.transform.localScale = new Vector3(1, 1, 1);
        bottom.transform.localScale = new Vector3(1, 1, 1);
        topBG.color = topColor;
        middleBG.color = middleColor;
        bottomBG.color = bottomColor;
    }

    public void PlayerHoverAudio()
    {
        audioSrc.PlayOneShot(hoverAudio);
    }

    public void PlayClickAudio()
    {
        audioSrc.PlayOneShot(clickAudio);
    }

}

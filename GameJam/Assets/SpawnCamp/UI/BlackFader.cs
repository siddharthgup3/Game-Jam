using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackFader : MonoBehaviour
{

    private static Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static void FadeFromBlack()
    {
        anim.SetTrigger("FadeFromBlack");
    }

    public static void FadeToBlack()
    {
        anim.SetTrigger("FadeToBlack");
    }
}

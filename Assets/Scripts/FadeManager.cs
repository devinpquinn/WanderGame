using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    private Animator anim;

    //singleton
    private static FadeManager fm;
    public static FadeManager instance { get { return fm; } }

    private void Awake()
    {
        //singleton
        if (fm != null && fm != this)
        {
            Destroy(gameObject);
        }
        else
        {
            fm = this;

        }

        anim = GetComponent<Animator>();
    }

    public static void FadeIn()
    {
        instance.anim.Play("FadeIn");
    }

    public static void FadeOut()
    {
        instance.anim.Play("FadeOut");
    }

    public static void FadeCross()
    {
        instance.anim.Play("FadeCross");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FadeManager : MonoBehaviour
{
    private Animator anim;

    //singleton
    private static FadeManager fm;
    public static FadeManager instance { get { return fm; } }

    //events
    [HideInInspector]
    public UnityEvent fadeEvent;

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

    public static void FadeIn(UnityEvent myEvent = null)
    {
        instance.fadeEvent = myEvent;
        instance.anim.Play("FadeIn");
    }

    public static void FadeOut(UnityEvent myEvent = null)
    {
        instance.fadeEvent = myEvent;
        instance.anim.Play("FadeOut");
    }

    public static void FadeCross(UnityEvent myEvent = null)
    {
        instance.fadeEvent = myEvent;
        instance.anim.Play("FadeCross");
    }

    public void FadeEvent()
    {
        if(fadeEvent != null)
        {
            UnityEvent myEvent = fadeEvent;
            fadeEvent = null;
            myEvent.Invoke();
        }
    }
}

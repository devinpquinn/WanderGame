using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public Animator anim;
    public AudioSource src;
    public AudioClip clip;

    public void Hover()
    {
        if (!(anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1))
        {
            src.PlayOneShot(clip);
        }
    }
}

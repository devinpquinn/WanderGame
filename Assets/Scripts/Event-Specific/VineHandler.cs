using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineHandler : MonoBehaviour
{
    private Animator anim;
    private AudioSource src;
    public AudioClip clip;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        src = GetComponent<AudioSource>();
    }

    public void Crawl()
    {
        if (!PlayerPrefs.HasKey("Data_Vine"))
        {
            anim.Play("Vine_Crawl");
            src.PlayOneShot(clip);
            PlayerPrefs.SetInt("Data_Vine", 1);
        }
    }
}

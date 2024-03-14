using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineHandler : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Crawl()
    {
        if (!PlayerPrefs.HasKey("Data_Vine"))
        {
            anim.Play("Vine_Crawl");
            PlayerPrefs.SetInt("Data_Vine", 1);
        }
    }
}

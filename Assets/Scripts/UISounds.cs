using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{
    private AudioSource src;
    public List<AudioClip> clickSounds;
    private int lastIndex = -1;

    //singleton
    private static UISounds _sounds;
    public static UISounds instance { get { return _sounds; } }

    private void Awake()
    {
        //singleton
        if (_sounds != null && _sounds != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _sounds = this;

        }

        //variable fetching
        src = GetComponent<AudioSource>();
    }

    public static void Advance()
    {
        instance.Click();
    }

    public void Click()
    {
        int key = Random.Range(0, clickSounds.Count);
        src.PlayOneShot(clickSounds[key]);
    }
}

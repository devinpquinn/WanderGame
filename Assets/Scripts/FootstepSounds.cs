using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSounds : MonoBehaviour
{
    private AudioSource footstepSource;

    public List<AudioClip> grassSounds;
    public List<AudioClip> stoneSounds;
    public bool grass = true;

    private int lastIndex;

    private void Awake()
    {
        footstepSource = GetComponent<AudioSource>();
    }

    public void PlayFootstepSound()
    {
        footstepSource.PlayOneShot(GetGrassSound());
    }

    public AudioClip GetGrassSound()
    {
        if (grass)
        {
            int key = Random.Range(0, grassSounds.Count);
            while (key == lastIndex)
            {
                key = Random.Range(0, grassSounds.Count);
            }
            lastIndex = key;
            return grassSounds[key];
        }
        else
        {
            int key = Random.Range(0, stoneSounds.Count);
            while (key == lastIndex)
            {
                key = Random.Range(0, stoneSounds.Count);
            }
            lastIndex = key;
            return stoneSounds[key];
        }
    }
}

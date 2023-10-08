using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSounds : MonoBehaviour
{
    public AudioSource footstepSource;

    public List<AudioClip> dirtSounds;
    public List<AudioClip> grassSounds;
    public List<AudioClip> stoneSounds;
    public List<AudioClip> woodSounds;

    private int lastIndex;

    public void PlayFootstepSound(string stepMaterial)
    {
        switch (stepMaterial)
        {
            
            case "Dirt":
                footstepSource.PlayOneShot(GetDirtSound());
                break;
            case "Grass":
                footstepSource.PlayOneShot(GetGrassSound());
                break;
            case "Stone":
                footstepSource.PlayOneShot(GetStoneSound());
                break;
            case "Wood":
                footstepSource.PlayOneShot(GetWoodSound());
                break;
        }
    }

    public AudioClip GetDirtSound()
    {
        int key = Random.Range(0, dirtSounds.Count);
        while (key == lastIndex)
        {
            key = Random.Range(0, dirtSounds.Count);
        }
        lastIndex = key;
        return dirtSounds[key];
    }

    public AudioClip GetGrassSound()
    {
        int key = Random.Range(0, grassSounds.Count);
        while (key == lastIndex)
        {
            key = Random.Range(0, grassSounds.Count);
        }
        lastIndex = key;
        return grassSounds[key];
    }

    public AudioClip GetStoneSound()
    {
        int key = Random.Range(0, stoneSounds.Count);
        while (key == lastIndex)
        {
            key = Random.Range(0, stoneSounds.Count);
        }
        lastIndex = key;
        return stoneSounds[key];
    }

    public AudioClip GetWoodSound()
    {
        int key = Random.Range(0, woodSounds.Count);
        while (key == lastIndex)
        {
            key = Random.Range(0, woodSounds.Count);
        }
        lastIndex = key;
        return woodSounds[key];
    }
}

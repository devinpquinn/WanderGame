using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppedHandler : MonoBehaviour
{
    public List<AudioClip> ChopSounds;
    private int lastIndex = -1;
    private AudioSource src;

    public Transform bloodOrigin;
    public GameObject ps;

    private void Awake()
    {
        src = GetComponent<AudioSource>();
    }

    public void ChoppedSound()
    {
        int index = Random.Range(0, ChopSounds.Count);
        while (index == lastIndex)
        {
            index = Random.Range(0, ChopSounds.Count);
        }
        src.PlayOneShot(ChopSounds[index]);

        Instantiate(ps, bloodOrigin.transform.position, Quaternion.identity, bloodOrigin);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusiciansHandler : MonoBehaviour
{
    private AudioSource src;

    public Animator flute;
    public Animator drums;
    public Animator strings;

    private void Awake()
    {
        src = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(Perform());
    }

    IEnumerator Perform()
    {
        yield return new WaitUntil(() => PlayerController.instance.state == PlayerController.playerState.Exploring);
        src.Play();
        yield return new WaitForSeconds(0.3f);
        flute.Play("Flute_Play");
        strings.Play("Strings_Bob");
        yield return new WaitForSeconds(6.5f);
        drums.Play("Drums_Play");
        yield return new WaitForSeconds(14.7f);
        strings.Play("Strings_Play");
        yield return new WaitForSeconds(14.5f);
        flute.Play("Flute_Tap");
        yield return new WaitForSeconds(14f);
        flute.Play("Flute_Play");
    }
}

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
        StartCoroutine(LoopPerformance());
    }

    IEnumerator LoopPerformance()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        StartCoroutine(Perform());
    }

    IEnumerator Perform()
    {
        yield return new WaitUntil(() => PlayerController.instance.state == PlayerController.playerState.Exploring);
        src.Play();
        yield return new WaitForSecondsRealtime(0.3f);
        flute.Play("Flute_Play");
        strings.Play("Strings_Bob");
        yield return new WaitForSecondsRealtime(6.5f);
        drums.Play("Drums_Play");
        yield return new WaitForSecondsRealtime(14.7f);
        strings.Play("Strings_Play");
        yield return new WaitForSecondsRealtime(14.5f);
        flute.Play("Flute_Tap");
        yield return new WaitForSecondsRealtime(14f);
        flute.Play("Flute_Play");
        yield return new WaitForSecondsRealtime(14f);
        flute.Play("Flute_Idle");
        yield return new WaitForSecondsRealtime(0.5f);
        strings.Play("Strings_Idle");
        yield return new WaitForSecondsRealtime(0.5f);
        drums.Play("Drums_Idle");
        yield return new WaitForSecondsRealtime(10f);
        StartCoroutine(LoopPerformance());
    }
}

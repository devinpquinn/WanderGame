using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    private Image fade;

    public bool fadeInOnStart = true;
    private float fadeTime = 0.5f;
    private Color tempColor;

    //singleton
    private static FadeManager fm;
    public static FadeManager instance { get { return fm; } }

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

        fade = GetComponent<Image>();
        tempColor = fade.color;
    }

    private void Start()
    {
        if (fadeInOnStart)
        {
            //FadeIn();
            FadeInIntro();
        }
    }

    public void SetFadeAlpha(float val)
    {
        tempColor.a = val;
        fade.color = tempColor;
    }

    public static void FadeIn(UnityEvent myEvent = null)
    {
        instance.StopAllCoroutines();
        instance.StartCoroutine(instance.DoFadeIn(myEvent));
    }

    public IEnumerator DoFadeIn(UnityEvent myEvent = null)
    {
        float timer = 0;
        SetFadeAlpha(1);
        while (timer < fadeTime)
        {
            SetFadeAlpha(Mathf.Lerp(1, 0, (timer / fadeTime)));
            timer += Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        SetFadeAlpha(0);
        if(myEvent != null)
        {
            myEvent.Invoke();
        }
        yield return null;
    }

    public static void FadeOut(UnityEvent myEvent = null)
    {
        instance.StopAllCoroutines();
        instance.StartCoroutine(instance.DoFadeOut(myEvent));
    }

    public IEnumerator DoFadeOut(UnityEvent myEvent = null)
    {
        float timer = 0;
        SetFadeAlpha(0);
        while (timer < fadeTime)
        {
            SetFadeAlpha(Mathf.Lerp(0, 1, (timer / fadeTime)));
            timer += Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        SetFadeAlpha(1);
        if (myEvent != null)
        {
            myEvent.Invoke();
        }
        yield return null;
    }

    public static void FadeCross(UnityEvent myEvent = null)
    {
        instance.StopAllCoroutines();
        instance.StartCoroutine(instance.DoFadeCross(myEvent));
    }

    public IEnumerator DoFadeCross(UnityEvent myEvent = null)
    {
        float timer = 0;
        SetFadeAlpha(0);
        while (timer < fadeTime)
        {
            SetFadeAlpha(Mathf.Lerp(0, 1, (timer / fadeTime)));
            timer += Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        SetFadeAlpha(1);
        if (myEvent != null)
        {
            myEvent.Invoke();
        }
        timer = 0;
        while (timer < fadeTime)
        {
            SetFadeAlpha(Mathf.Lerp(1, 0, (timer / fadeTime)));
            timer += Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        SetFadeAlpha(0);
        yield return null;
    }

    private void FadeInIntro()
    {
        StopAllCoroutines();
        StartCoroutine(DoFadeInIntro());
    }

    IEnumerator DoFadeInIntro()
    {
        SetFadeAlpha(1);
        yield return new WaitForSecondsRealtime(1);
        StartCoroutine(DoFadeIn());
    }
}

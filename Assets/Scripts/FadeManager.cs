using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    private Image fade;

    public bool fadeInOnStart = true;
    [HideInInspector]
    public bool isFading = false;
    public GameObject mainMenu;
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
        isFading = false;
    }

    private void Start()
    {
        if (fadeInOnStart)
        {
            //FadeIn();
            FadeInIntro();
        }
    }

    public float GetFadeAlpha()
    {
        return fade.color.a;
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
        isFading = true;
        float timer = 0;
        //SetFadeAlpha(1);
        float startVal = GetFadeAlpha();
        if(startVal < 1)
        {
            //timer = fadeTime * (1 - startVal);
        }
        while (timer < fadeTime)
        {
            SetFadeAlpha(Mathf.Lerp(startVal, 0, (timer / fadeTime)));
            timer += Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        SetFadeAlpha(0);
        if(myEvent != null)
        {
            myEvent.Invoke();
        }
        isFading = false;
        yield return null;
    }

    public static void FadeOut(UnityEvent myEvent = null)
    {
        instance.StopAllCoroutines();
        instance.StartCoroutine(instance.DoFadeOut(myEvent));
    }

    public IEnumerator DoFadeOut(UnityEvent myEvent = null)
    {
        isFading = true;
        float timer = 0;
        //SetFadeAlpha(0);
        float startVal = GetFadeAlpha();
        if(startVal > 0)
        {
            //timer = fadeTime - (fadeTime * startVal);
        }
        while (timer < fadeTime)
        {
            SetFadeAlpha(Mathf.Lerp(startVal, 1, (timer / fadeTime)));
            timer += Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        SetFadeAlpha(1);
        if (myEvent != null)
        {
            myEvent.Invoke();
        }
        isFading = false;
        yield return null;
    }

    public static void FadeCross(UnityEvent myEvent = null)
    {
        instance.StopAllCoroutines();
        instance.StartCoroutine(instance.DoFadeCross(myEvent));
    }

    public IEnumerator DoFadeCross(UnityEvent myEvent = null)
    {
        isFading = true;
        float timer = 0;
        //SetFadeAlpha(0);
        float startVal = GetFadeAlpha();
        if(startVal > 0)
        {
            timer = fadeTime - (fadeTime * startVal);
        }
        while (timer < fadeTime)
        {
            SetFadeAlpha(Mathf.Lerp(startVal, 1, (timer / fadeTime)));
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
        isFading = false;
        yield return null;
    }

    private void FadeInIntro()
    {
        StopAllCoroutines();
        StartCoroutine(DoFadeInIntro());
    }

    private void ActivateMenu()
    {
        mainMenu.SetActive(true);
    }

    IEnumerator DoFadeInIntro()
    {
        SetFadeAlpha(1);

        //set timescale to 0
        //Time.timeScale = 0;

        //show cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        yield return new WaitForSecondsRealtime(1);
        ActivateMenu();
        StartCoroutine(DoFadeIn());
    }
}

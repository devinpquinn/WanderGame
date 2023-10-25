using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextLerper : MonoBehaviour
{
    private TextMeshProUGUI txt;
    public float minSize;
    public float maxSize; 
    public float lerpTime = 0.5f;
    private float timer = 0;
    private bool collapse = false;

    private void Awake()
    {
        txt = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        collapse = false;
    }

    public void CloseOut()
    {
        collapse = true;
        StopAllCoroutines();
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(DoLerpDown());
        }
    }

    public void LerpUp()
    {
        if (!collapse)
        {
            StopAllCoroutines();
            StartCoroutine(DoLerpUp());
        }
    }

    IEnumerator DoLerpUp()
    {
        timer = 0;
        float startSize = txt.fontSize;
        if(startSize - minSize > 0)
        {
            timer = ((startSize - minSize) / (maxSize - minSize)) * lerpTime;
        }
        while(timer < lerpTime)
        {
            timer += Time.unscaledDeltaTime;
            txt.fontSize = Mathf.Lerp(minSize, maxSize, timer / lerpTime);
            yield return new WaitForEndOfFrame();
        }
        txt.fontSize = maxSize;
        yield return null;
    }

    public void LerpDown()
    {
        if (!collapse)
        {
            StopAllCoroutines();
            StartCoroutine(DoLerpDown());
        }
    }

    IEnumerator DoLerpDown()
    {
        timer = 0;
        float startSize = txt.fontSize;
        if (maxSize - startSize > 0)
        {
            timer = ((maxSize - startSize) / (maxSize - minSize)) * lerpTime;
        }
        while (timer < lerpTime)
        {
            timer += Time.unscaledDeltaTime;
            txt.fontSize = Mathf.Lerp(maxSize, minSize, timer / lerpTime);
            yield return new WaitForEndOfFrame();
        }
        txt.fontSize = minSize;
        yield return null;
    }
}

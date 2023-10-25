using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CreditsScript : MonoBehaviour
{
    public GameObject herald;
    public RectTransform scroll;
    private Vector2 startPos;
    public float scrollSpeed;
    public float maxPos;

    private bool locked = true;
    private bool moving = false;

    private void Awake()
    {
        startPos = scroll.anchoredPosition;
    }

    private void OnEnable()
    {
        scroll.anchoredPosition = startPos;
        locked = false;
        moving = false;

        StartCoroutine(WaitForStart());
    }

    IEnumerator WaitForStart()
    {
        herald.SetActive(true);
        yield return new WaitForSecondsRealtime(2.5f);
        UnityEvent myEvent = new UnityEvent();
        myEvent.AddListener(StartScroll);
        FadeManager.FadeCross(myEvent);
    }

    public void StartScroll()
    {
        herald.SetActive(false);
        moving = true;
    }

    private void Update()
    {
        if (moving)
        {
            scroll.Translate(0, 1 * scrollSpeed * Time.unscaledDeltaTime, 0);
        }

        if (Input.anyKey && !locked)
        {
            TriggerClose();
        }

        if (!locked)
        {
            if(scroll.anchoredPosition.y >= maxPos)
            {
                TriggerClose();
            }
        }
    }

    public void TriggerClose()
    {
        locked = true;
        UnityEvent myEvent = new UnityEvent();
        myEvent.AddListener(Close);
        FadeManager.FadeCross(myEvent);
        RandomMusic.Fade(0.5f, 1f);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}

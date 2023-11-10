using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepyHandler : MonoBehaviour
{
    public SpriteRenderer guy;
    public Sprite guyAsleep;
    public AudioSource speaker;
    public Interaction before;
    public Image fader;
    private Color tempColor;

    public float timer;
    public float endTime;
    private bool wasEnabled = false;

    private void Awake()
    {
        tempColor = fader.color;
    }

    //if the player is returning to this room, set the guy to asleep
    private void OnEnable()
    {
        if (wasEnabled || PlayerPrefs.HasKey("Dialog_" + before.id))
        {
            guy.sprite = guyAsleep;
            SetFadeAlpha(0);
        }
        else
        {
            wasEnabled = true;
        }
    }

    //start the sleepy timer and music
    public void StartSleepTimer()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        Canvas faderCanvas = fader.transform.parent.GetComponent<Canvas>();
        faderCanvas.worldCamera = Camera.main;
        faderCanvas.sortingLayerName = "UI";
        faderCanvas.sortingOrder = -1;

        yield return new WaitForSeconds(10);

        speaker.Play();
        RandomMusic.Fade(timer, 0);

        float timerStart = timer;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            SetFadeAlpha(Mathf.Lerp(1, 0, timer / timerStart));
            yield return new WaitForEndOfFrame();
        }
        SetFadeAlpha(1);

        PlayerController.instance.state = PlayerController.playerState.Locked;
        timer = endTime - timerStart;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        EndSleepTimer();
    }

    public void SetFadeAlpha(float val)
    {
        tempColor.a = val;
        fader.color = tempColor;
    }

    //trigger the end event
    public void EndSleepTimer()
    {
        StartCoroutine(CountIn());
    }

    IEnumerator CountIn()
    {
        RandomMusic.Fade(5f, 1);

        SetFadeAlpha(1);
        guy.sprite = guyAsleep;

        float timer = 0;
        float goal = 5f;
        while (timer < goal)
        {
            SetFadeAlpha(Mathf.Lerp(1, 0, timer / goal));
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        SetFadeAlpha(0);

        PlayerController.instance.state = PlayerController.playerState.Exploring;
    }
}

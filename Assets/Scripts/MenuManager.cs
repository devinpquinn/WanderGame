using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuManager : MonoBehaviour
{
    public Texture2D pointer;
    public GameObject continueButton;

    //animation
    private Animator anim;

    //intro
    public Interaction intro;

    //credits
    public GameObject credits;

    //text lerpers
    public List<TextLerper> lerpers;

    //singleton
    private static MenuManager _menu;
    public static MenuManager instance { get { return _menu; } }

    private void Awake()
    {
        //singleton
        if (_menu != null && _menu != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _menu = this;

        }

        //set cursor
        Cursor.SetCursor(pointer, new Vector2(0, 0), CursorMode.Auto);

        //variable fetching
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        //set timescale to 0
        Time.timeScale = 0;

        //show cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //check if continue button should be shown
        continueButton.SetActive(true);
        if (Time.timeSinceLevelLoad < 1 && !PlayerPrefs.HasKey("CurrentRoom"))
        {
            continueButton.SetActive(false);
        }
    }

    public void CloseMenu()
    {
        PlayerController.instance.state = PlayerController.playerState.Exploring;
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        //set timescale to 1
        Time.timeScale = 1;

        //hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Continue()
    {
        UnityEvent myEvent = new UnityEvent();
        myEvent.AddListener(CloseMenu);
        FadeManager.FadeCross(myEvent);

        //animate out
        anim.Play("Menu_Out");
        CollapseLerpers();
    }

    public void OpenMenu()
    {
        anim.Play("Menu_In");
        ExpandLerpers();
    }

    public void New()
    {
        PlayerPrefs.DeleteAll();
        RoomManager.instance.InitializeRooms();

        UnityEvent myEvent = new UnityEvent();
        myEvent.AddListener(CloseMenu);
        myEvent.AddListener(intro.Interact);

        FadeManager.FadeCross(myEvent);

        //fade out audio
        RandomMusic.Fade(0.5f, 0.25f);

        //animate out
        anim.Play("Menu_Out");
        CollapseLerpers();
    }

    public void Credits()
    {
        UnityEvent myEvent = new UnityEvent();
        myEvent.AddListener(OpenCredits);
        FadeManager.FadeCross(myEvent);

        //animate out
        anim.Play("Menu_Out");
        CollapseLerpers();

        //fade out audio
        RandomMusic.Fade(0.5f, 0.25f);
    }

    public void OpenCredits()
    {
        credits.SetActive(true);
    }

    public void Quit()
    {
        UnityEvent myEvent = new UnityEvent();
        myEvent.AddListener(Application.Quit);
        FadeManager.FadeOut(myEvent);

        //animate out
        anim.Play("Menu_Out");

        //fade out audio
        RandomMusic.Fade(0.5f, 0f);
    }

    public void CollapseLerpers()
    {
        foreach(TextLerper lerper in lerpers)
        {
            lerper.CloseOut();
        }
    }

    public void ExpandLerpers()
    {
        foreach (TextLerper lerper in lerpers)
        {
            lerper.EnableAgain();
        }
    }
}

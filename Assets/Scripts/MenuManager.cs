using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuManager : MonoBehaviour
{
    public Texture2D pointer;
    public GameObject continueButton;

    //singleton
    private static MenuManager _menu;
    public static MenuManager instance { get { return _menu; } }

    private void Awake()
    {
        //set cursor
        Cursor.SetCursor(pointer, new Vector2(0, 0), CursorMode.Auto);
    }

    private void OnEnable()
    {
        //set timescale to 0
        Time.timeScale = 0;

        //show cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //check if continue button should be shown
        if (PlayerPrefs.HasKey("CurrentRoom"))
        {
            continueButton.SetActive(true);
        }
        else
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
    }

    public void New()
    {
        PlayerPrefs.DeleteAll();
        RoomManager.instance.InitializeRooms();

        UnityEvent myEvent = new UnityEvent();
        myEvent.AddListener(CloseMenu);
        FadeManager.FadeCross(myEvent);
    }

    public void Credits()
    {

    }

    public void Quit()
    {
        UnityEvent myEvent = new UnityEvent();
        myEvent.AddListener(Application.Quit);
        FadeManager.FadeOut(myEvent);
    }
}

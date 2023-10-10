using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Texture2D pointer;

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
    }

    private void OnDisable()
    {
        //hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Continue()
    {

    }

    public void New()
    {

    }

    public void Credits()
    {

    }

    public void Quit()
    {
        
    }
}

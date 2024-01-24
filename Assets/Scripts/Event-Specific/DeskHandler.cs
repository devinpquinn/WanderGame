using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskHandler : MonoBehaviour
{
    private Interaction i;

    private void Awake()
    {
        i = GetComponent<Interaction>();
    }

    private void Start()
    {
        
    }

    public void SetDialogs()
    {
        int doorsLeft = 0;
        if (PlayerPrefs.HasKey("Data_DoorsLeft"))
        {
            doorsLeft = PlayerPrefs.GetInt("Data_DoorsLeft");
        }
        Debug.Log("Left: " + doorsLeft);

        int doorsRight = 0;
        if (PlayerPrefs.HasKey("Data_DoorsRight"))
        {
            doorsRight = PlayerPrefs.GetInt("Data_DoorsRight");
        }
        Debug.Log("Right: " + doorsRight);

        int doorsUp = 0;
        if (PlayerPrefs.HasKey("Data_DoorsUp"))
        {
            doorsUp = PlayerPrefs.GetInt("Data_DoorsUp");
        }
        Debug.Log("Up: " + doorsUp);

        int doorsDown = 0;
        if (PlayerPrefs.HasKey("Data_DoorsDown"))
        {
            doorsDown = PlayerPrefs.GetInt("Data_DoorsDown");
        }
        Debug.Log("Down: " + doorsDown);

        List<int> doorFreqs = new List<int> { doorsLeft, doorsRight, doorsUp, doorsDown };

        int greatestIndex = 0;
        for(int i = 1; i < doorFreqs.Count; i++)
        {
            if(doorFreqs[i] > doorFreqs[greatestIndex])
            {
                greatestIndex = i;
            }
        }

        if(greatestIndex == 0)
        {
            i.dialogs[0].lines[1] = "Left";
        }
        else if (greatestIndex == 1)
        {
            i.dialogs[0].lines[1] = "Right";
        }
        else if (greatestIndex == 2)
        {
            i.dialogs[0].lines[1] = "Up";
        }
        else
        {
            i.dialogs[0].lines[1] = "Down";
        }
    }
}

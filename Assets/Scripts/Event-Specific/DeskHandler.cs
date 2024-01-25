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
        if (PlayerPrefs.HasKey("Dialog_999999999"))
        {
            i.dialogs[1].lines[0] = "\"How about that Skull-Taker, eh? Hubba hubba.\"";
            i.dialogs[1].lines[1] = "\"He can take my skull whenever he'd like.\"";
        }
        else
        {
            i.dialogs[1].lines[0] = "\"If you see the Skull-Taker, tell him I said hiii:)\"";
            i.dialogs[1].lines[1] = "\"Before he Takes your Skull, of course.\"";
        }

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
            i.dialogs[0].lines[1] = "\"I'm seeing here that you've been traipsing in a general right-to-left direction. That's fairly unusual.\"";
        }
        else if (greatestIndex == 1)
        {
            i.dialogs[0].lines[1] = "\"Looks like you like to go left to right, mostly-- no surprises there. That's the most common pattern.\"";
        }
        else if (greatestIndex == 2)
        {
            i.dialogs[0].lines[1] = "\"Let's see... you're mostly headed upwards, huh? Makes sense. I usually see up favored over down, and right over left.\"";
        }
        else
        {
            i.dialogs[0].lines[1] = "\"You've mostly been headed in a downward direction, huh? I guess that makes sense, on an instinctual level or whatever.\"";
        }
    }
}

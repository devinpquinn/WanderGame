using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopsHandler : MonoBehaviour
{
    public GameObject chopper;
    public GameObject chopped;

    private void Start()
    {
        BlockerManager.SetupBlockers("NES");
        RoomManager.instance.currentRoom.GetComponent<Room>().doors = "NES";

        //determine if this is the first or second clearing
        if((PlayerPrefs.HasKey("Room_101010101") && GetComponent<Room>().id != 101010101) || (PlayerPrefs.HasKey("Room_010101010") && GetComponent<Room>().id != 010101010))
        {
            chopper.SetActive(false);
            chopped.SetActive(true);
        }
        else
        {
            chopper.SetActive(true);
            chopped.SetActive(false);
        }
    }

    private void OnEnable()
    {
        GameObject.Find("Persistent Environment/Blockers/Perimeter/Blocker15").SetActive(false);
    }

    private void OnDisable()
    {
        GameObject.Find("Persistent Environment/Blockers/Perimeter/Blocker15").SetActive(true);
    }

    private void OnDestroy()
    {
        GameObject.Find("Persistent Environment/Blockers/Perimeter/Blocker15").SetActive(true);
    }
}

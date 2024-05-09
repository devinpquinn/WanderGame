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
        if(PlayerPrefs.HasKey("Data_Chopping") && PlayerPrefs.GetInt("Data_Chopping") != GetComponent<Room>().id)
        {
            chopped.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("Data_Chopping", GetComponent<Room>().id);
            chopper.SetActive(true);
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
}

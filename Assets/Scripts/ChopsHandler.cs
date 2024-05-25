using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopsHandler : MonoBehaviour
{
    private void Start()
    {
        BlockerManager.SetupBlockers("NES");
        RoomManager.instance.currentRoom.GetComponent<Room>().doors = "NES";
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

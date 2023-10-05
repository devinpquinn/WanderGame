using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> roomsAvailable;
    public GameObject currentRoom;
    public GameObject prevRoom;
    public string prevRoomDirection;

    //singleton
    private static RoomManager rm;
    public static RoomManager instance { get { return rm; } }

    private void Awake()
    {
        //singleton
        if (rm != null && rm != this)
        {
            Destroy(gameObject);
        }
        else
        {
            rm = this;

        }
    }

    private void Start()
    {
        //fill list of rooms
        roomsAvailable = new List<GameObject>();
        Object[] roomsToCheck = Resources.LoadAll("Rooms", typeof(GameObject));
        for(int i = 0; i < roomsToCheck.Length; i++)
        {
            GameObject thisRoom = (GameObject)roomsToCheck[i];
            if(thisRoom.GetComponent<Room>().id > 1) //replace this with a playerprefs check
            {
                roomsAvailable.Add(thisRoom);
            }
        }

        //setup currentRoom
        currentRoom = Instantiate(currentRoom);
    }

    public static void NewRoom(string enterFrom)
    {
        //are we returning to the previous room?
        bool returning = false;
        if(instance.prevRoomDirection != null)
        {
            string prevDir = instance.prevRoomDirection;
            if((enterFrom == "N" && prevDir == "S") || (enterFrom == "E" && prevDir == "W") || (enterFrom == "S" && prevDir == "N") || (enterFrom == "W" && prevDir == "E"))
            {
                //if so:
                returning = true;

                GameObject tempRoom = instance.currentRoom;
                instance.currentRoom = instance.prevRoom;
                instance.prevRoom = tempRoom;
            }
        }
        //if not, get a new room:
        if (!returning)
        {
            //randomly pick a new room
            //check if you've exhausted all the rooms:
            if(instance.roomsAvailable.Count < 1)
            {
                Debug.Log("OUT OF ROOMS!");
                return;
            }
            else
            {
                Destroy(instance.prevRoom);

                GameObject newRoom;
                int key = Random.Range(0, instance.roomsAvailable.Count);
                newRoom = instance.roomsAvailable[key];

                //remove it from list and set as current room
                instance.roomsAvailable.RemoveAt(key);
                instance.prevRoom = instance.currentRoom;
                instance.currentRoom = newRoom;

                //generate and store random blockers for currentRoom
            }
        }

        //in both cases:
        instance.prevRoomDirection = enterFrom;
        instance.prevRoom.SetActive(false);
        if (returning)
        {
            instance.currentRoom.SetActive(true);
        }
        else
        {
            instance.currentRoom = Instantiate(instance.currentRoom);
        }

        //send message to blockermanager from currentroom's blockers string
    }
}

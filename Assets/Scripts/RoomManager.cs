using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> roomsAvailable;
    public GameObject currentRoom;
    public GameObject prevRoom;
    private string prevRoomDirection;

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

        //if not starting room, setup blockers from currentroom
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
            bool validRooms = false;
            for(int i = 0; i < instance.roomsAvailable.Count; i++)
            {
                if(instance.roomsAvailable[i].GetComponent<Room>().doors.Length < 1 || instance.roomsAvailable[i].GetComponent<Room>().doors.Contains(enterFrom))
                {
                    validRooms = true;
                    break;
                }
            }
            if (!validRooms)
            {
                Debug.Log("OUT OF ROOMS!");
                return;
            }
            else
            {
                Destroy(instance.prevRoom);

                //pick room
                GameObject newRoom;
                int key = Random.Range(0, instance.roomsAvailable.Count);
                newRoom = instance.roomsAvailable[key];
                while(newRoom.GetComponent<Room>().doors.Length > 0 && !newRoom.GetComponent<Room>().doors.Contains(enterFrom))
                {
                    key = Random.Range(0, instance.roomsAvailable.Count);
                    newRoom = instance.roomsAvailable[key];
                }

                //generate and store blockers for currentRoom
                string newRoomDoorsPreset = newRoom.GetComponent<Room>().doors;
                if (newRoomDoorsPreset.Length < 1)
                {
                    //random doors
                    newRoom.GetComponent<Room>().doors = RandomDoors(enterFrom);
                }

                //remove it from list and set as current room
                instance.roomsAvailable.RemoveAt(key);
                instance.prevRoom = instance.currentRoom;
                instance.currentRoom = newRoom;
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
        BlockerManager.SetupBlockers(instance.currentRoom.GetComponent<Room>().doors);
    }

    public static string RandomDoors(string entry)
    {
        string theseDoors = "";
        if(entry.Contains("N") || Random.Range(0, 2) > 0)
        {
            theseDoors += "N";
        }
        if (entry.Contains("E") || Random.Range(0, 2) > 0)
        {
            theseDoors += "E";
        }
        if (entry.Contains("S") || Random.Range(0, 2) > 0)
        {
            theseDoors += "S";
        }
        if (entry.Contains("W") || Random.Range(0, 2) > 0)
        {
            theseDoors += "W";
        }


        return theseDoors;
    }
}

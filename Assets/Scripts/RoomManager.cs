using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> roomsAvailable;
    public GameObject currentRoom;
    public GameObject prevRoom;
    [SerializeField]
    private string prevRoomDirection;

    [HideInInspector]
    public bool reachedEnding = false;
    public bool reachedEndingPlusOne = false;
    public AudioSource endingSource;
    public GameObject endingSplash;

    //passthrough
    public GameObject passthroughRoom;
    [HideInInspector]
    public int passthroughTimer = 0;
    //private string passthroughString = "0011112223";
    private string passthroughString = "0";

    //misc
    public GameObject startingRoom;

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
        InitializeRooms();
    }

    public void InitializeRooms()
    {
        //cleanup
        if(currentRoom != null)
        {
            Destroy(currentRoom);
        }
        if(prevRoom != null)
        {
            Destroy(prevRoom);
        }

        //fill list of rooms
        roomsAvailable = new List<GameObject>();
        bool loadingSave = PlayerPrefs.HasKey("CurrentRoom");
        Object[] roomsToCheck = Resources.LoadAll("Rooms", typeof(GameObject));
        for (int i = 0; i < roomsToCheck.Length; i++)
        {
            GameObject thisRoom = (GameObject)roomsToCheck[i];

            if (!PlayerPrefs.HasKey("Room_" + thisRoom.GetComponent<Room>().id))
            {
                roomsAvailable.Add(thisRoom);
            }
            if (loadingSave)
            {
                //check if this is the current room
                if (PlayerPrefs.GetInt("CurrentRoom") == thisRoom.GetComponent<Room>().id)
                {
                    currentRoom = Instantiate(thisRoom);
                }
                //check if this is the previous room
                else if (PlayerPrefs.GetInt("PrevRoom") == thisRoom.GetComponent<Room>().id)
                {
                    prevRoom = Instantiate(thisRoom);
                    prevRoom.SetActive(false);
                }
                //set prev room direction
                prevRoomDirection = PlayerPrefs.GetString("PrevRoomDirection");
            }
            else
            {
                //reset rooms with randomly generated doors
                if (thisRoom.GetComponent<Room>().doors.Contains("*"))
                {
                    thisRoom.GetComponent<Room>().doors = "";
                }
            }
        }

        if (!loadingSave)
        {
            //fresh start
            currentRoom = startingRoom;
            currentRoom = Instantiate(currentRoom);
            prevRoom = prevRoom = Instantiate(passthroughRoom);
            prevRoom.SetActive(false);
            prevRoomDirection = null;
            GeneratePassthroughTimer();

            //fresh start after completing game
            if (reachedEnding)
            {
                reachedEnding = false;
                reachedEndingPlusOne = false;

                //fade out ending song
                StartCoroutine(EndEndingSong());
            }
        }
        else
        {
            //if we saved in a passthrough room, which is not in the resources folder
            if (currentRoom == null)
            {
                currentRoom = Instantiate(passthroughRoom);
            }
            passthroughTimer = PlayerPrefs.GetInt("PassthroughTimer");
        }

        //if the prev room when saved was a passthrough room, which is not in the resources folder
        if (prevRoom == null)
        {
            if (PlayerPrefs.GetInt("PrevRoom") == 1)
            {
                //lmao you saved right out of the starting room? really?
                prevRoom = Instantiate(startingRoom);
                prevRoom.SetActive(false);
            }
            else
            {
                prevRoom = Instantiate(passthroughRoom);
                prevRoom.SetActive(false);
            }
        }

        //load current and previous room blockers?
        if (loadingSave)
        {
            currentRoom.GetComponent<Room>().doors = PlayerPrefs.GetString("CurrentRoomDoors");
            if (prevRoom != null)
            {
                prevRoom.GetComponent<Room>().doors = PlayerPrefs.GetString("PrevRoomDoors");
            }
        }

        //send message to blockermanager from currentroom's blockers string
        BlockerManager.SetupBlockers(instance.currentRoom.GetComponent<Room>().doors);

        //set player position
        PlayerController.instance.InitializePosition();
    }

    public static void NewRoom(string enterFrom)
    {
        instance.endingSplash.SetActive(false);
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
            Destroy(instance.prevRoom);
            //if passthrough timer > 0, generate a passthrough rooom
            if(instance.passthroughTimer > 0)
            {
                instance.passthroughTimer--;
                PlayerPrefs.SetInt("PassthroughTimer", instance.passthroughTimer);
                instance.prevRoom = instance.currentRoom;
                instance.currentRoom = instance.passthroughRoom;
            }
            else
            {
                //if not, generate a new passthrough timer and a new room
                instance.GeneratePassthroughTimer();
                PlayerPrefs.SetInt("PassthroughTimer", instance.passthroughTimer);
                //check if you've exhausted all the rooms:
                bool validRooms = false;
                for (int i = 0; i < instance.roomsAvailable.Count; i++)
                {
                    if (instance.roomsAvailable[i].GetComponent<Room>().doors.Length < 1 || instance.roomsAvailable[i].GetComponent<Room>().doors.Contains(enterFrom))
                    {
                        validRooms = true;
                        break;
                    }
                }
                if (!validRooms)
                {
                    //couldn't find a valid entrance to a remaining room
                    instance.passthroughTimer = 0;
                    instance.prevRoom = instance.currentRoom;
                    instance.currentRoom = instance.passthroughRoom;

                    //if there are no rooms left period, start playing an ending cue song
                    if(instance.roomsAvailable.Count < 1 && !instance.reachedEnding)
                    {
                        instance.reachedEnding = true;
                    }
                    else if (instance.reachedEnding && !instance.reachedEndingPlusOne)
                    {
                        instance.reachedEndingPlusOne = true;
                        instance.StartCoroutine(instance.StartEndingSong());
                        instance.passthroughTimer = 999;
                    }
                }
                else
                {
                    Destroy(instance.prevRoom);

                    //pick room
                    GameObject newRoom;
                    int key = Random.Range(0, instance.roomsAvailable.Count);
                    newRoom = instance.roomsAvailable[key];
                    while (newRoom.GetComponent<Room>().doors.Length > 0 && !newRoom.GetComponent<Room>().doors.Contains(enterFrom))
                    {
                        key = Random.Range(0, instance.roomsAvailable.Count);
                        newRoom = instance.roomsAvailable[key];
                    }

                    //update save data
                    string addID = "Room_" + instance.roomsAvailable[key].GetComponent<Room>().id.ToString();
                    PlayerPrefs.SetInt(addID, 1);

                    //remove it from list and set as current room
                    instance.roomsAvailable.RemoveAt(key);
                    instance.prevRoom = instance.currentRoom;
                    instance.currentRoom = newRoom;
                }
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
            //generate and store blockers for currentRoom
            instance.passthroughRoom.GetComponent<Room>().doors = "";
            string newRoomDoorsPreset = instance.currentRoom.GetComponent<Room>().doors;
            if (newRoomDoorsPreset.Length < 1)
            {
                //random doors
                instance.currentRoom.GetComponent<Room>().doors = RandomDoors(enterFrom);
            }

            instance.currentRoom = Instantiate(instance.currentRoom);
        }

        //send message to blockermanager from currentroom's blockers string
        BlockerManager.SetupBlockers(instance.currentRoom.GetComponent<Room>().doors);

        //save current room, previous room, and previous room direction
        PlayerPrefs.SetInt("CurrentRoom", instance.currentRoom.GetComponent<Room>().id);
        PlayerPrefs.SetInt("PrevRoom", instance.prevRoom.GetComponent<Room>().id);
        PlayerPrefs.SetString("PrevRoomDirection", enterFrom);

        //save blockers
        PlayerPrefs.SetString("PrevRoomDoors", instance.prevRoom.GetComponent<Room>().doors);
        PlayerPrefs.SetString("CurrentRoomDoors", instance.currentRoom.GetComponent<Room>().doors);
    }

    public static string RandomDoors(string entry)
    {
        string theseDoors = "*";
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

    public void GeneratePassthroughTimer()
    {
        int key = Random.Range(0, passthroughString.Length);
        int timer = int.Parse(passthroughString.Substring(key, 1));
        passthroughTimer = timer;
    }

    IEnumerator StartEndingSong()
    {
        endingSplash.SetActive(true);
        if (reachedEndingPlusOne)
        {
            BGMManager.Fade(0.5f, 0f);

            endingSource.volume = 0.5f;
            endingSource.Play();
        }
        yield return null;
    }

    IEnumerator EndEndingSong()
    {
        BGMManager.Fade(3f, 1f);

        endingSplash.SetActive(false);
        float startVolume = endingSource.volume;
        float timer = 0;
        while(endingSource.volume > 0.01f)
        {
            endingSource.volume = Mathf.Lerp(startVolume, 0, timer / 1);
            timer += Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        endingSource.volume = 0;
    }
}

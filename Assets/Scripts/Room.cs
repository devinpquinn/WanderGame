using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour, ISerializationCallbackReceiver
{
    public int id = 0; //used to track if this room has been used
    public string doors = ""; //door configuration

    public void OnBeforeSerialize()
    {
        if(id == 0)
        {
            id = Random.Range(100000000, 999999999);
            if (gameObject.name.Contains("Room"))
            {
                gameObject.name = "Room " + id.ToString();
            }
        }
    }

    public void OnAfterDeserialize()
    {
        //has to be implemented
    }
}

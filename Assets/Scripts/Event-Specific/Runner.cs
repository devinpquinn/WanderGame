using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    public bool solo;

    private Rigidbody2D rb;
    public float moveSpeed = 2.5f;
    public Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if(solo)
        {
            BlockerManager.SetupBlockers("NESW");
            RoomManager.instance.currentRoom.GetComponent<Room>().doors = "NESW";

            //check if we're returning to this room
            if (PlayerPrefs.HasKey("Data_Runner"))
            {
                gameObject.SetActive(false);
            }
            else
            {
                PlayerPrefs.SetInt("Data_Runner", 1);
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}

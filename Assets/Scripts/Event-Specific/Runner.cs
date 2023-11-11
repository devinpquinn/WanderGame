using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 2.5f;
    Vector2 movement = new Vector2(-1, 0);

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
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

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}

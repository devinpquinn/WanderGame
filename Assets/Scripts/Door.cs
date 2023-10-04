using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum Direction { Horizontal, Vertical };

    public Direction direction;

    Vector2 vector;

    private PlayerController pc;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pc = PlayerController.instance;
            //when player enters, they must move in the direction opposite to their angle of entry until they exit
            if (direction == Direction.Horizontal)
            {
                if(collision.transform.position.x > transform.position.x)
                {
                    //move left
                    vector = new Vector2(-1, 0);
                }
                else
                {
                    //move right
                    vector = new Vector2(1, 0);
                }
            }
            else if(direction == Direction.Vertical)
            {
                if (collision.transform.position.y < transform.position.y)
                {
                    //move up
                    vector = new Vector2(0, 1);
                }
                else
                {
                    //move down
                    vector = new Vector2(0, -1);
                }
            }

            pc.StartCross(vector);

        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pc = PlayerController.instance;
            pc.EndCross();
        }
    }
}

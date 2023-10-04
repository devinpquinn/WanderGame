using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    #region Variables

    //state
    public enum playerState { Exploring, Interacting, Crossing, Locked };

    public playerState state;

    //movement
    private Rigidbody2D rb;
    public float moveSpeed = 3f;
    Vector2 movement;
    private bool enterDoor;

    //interaction
    public GameObject dialogueCard;
    public TextMeshProUGUI dialogueText;
    public Interaction interaction;

    //singleton
    private static PlayerController _player;
    public static PlayerController instance { get { return _player; } }

    #endregion

    private void Awake()
    {
        //singleton
        if (_player != null && _player != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _player = this;

        }

        //variable fetching
        state = playerState.Exploring;
        rb = GetComponent<Rigidbody2D>();
    }

    #region Updates

    void Update()
    {
        if(state == playerState.Exploring)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

        if(state == playerState.Interacting)
        {
            if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space)) && interaction != null)
            {
                //advance interaction
                interaction.Advance();
            }
        }
    }

    void FixedUpdate()
    {
        //player movement
        if (state == playerState.Exploring || state == playerState.Crossing)
        {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }

    #endregion

    public void StartCross(Vector2 vector)
    {
        if(state != playerState.Crossing)
        {
            //first port
            state = playerState.Crossing;
            enterDoor = true;
        }
        else
        {
            enterDoor = false;
        }
        movement = vector;
    }

    public void EndCross()
    {
        if (enterDoor)
        {
            //teleport
            if(movement.x == 0 && movement.y > 0)
            {
                //top -> bottom
                transform.position = new Vector2(transform.position.x, -6f);
            }
            else if(movement.x > 0 && movement.y == 0)
            {
                //right -> left
                transform.position = new Vector2(-6.5f, transform.position.y);
            }
            else if(movement.x == 0 && movement.y < 0)
            {
                //bottom -> top
                transform.position = new Vector2(transform.position.x, 6f);
            }
            else
            {
                //left -> right
                transform.position = new Vector2(6.5f, transform.position.y);
            }
        }
        else
        {
            state = playerState.Exploring;
        }
    }

}

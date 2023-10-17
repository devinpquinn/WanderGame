using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

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
    private bool stopped = true;

    //animation
    private Animator anim;
    private SpriteRenderer pawn;

    //interaction
    public GameObject dialogueCard;
    public TextMeshProUGUI dialogueText;
    public RectTransform alignment;
    [HideInInspector]
    public Interaction interaction;

    //menu
    public GameObject menu;

    //singleton
    private static PlayerController _player;
    public static PlayerController instance { get { return _player; } }

    #endregion

    private void Awake()
    {
        //singleton
        if (_player != null && _player != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _player = this;

        }

        //variable fetching
        state = playerState.Locked;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pawn = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InitializePosition();
    }

    public void InitializePosition()
    {
        //load position
        if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY"))
        {
            transform.position = new Vector2(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"));
        }
        else
        {
            transform.position = new Vector2(0, -0.5f);
            pawn.flipX = false;
        }
    }

    #region Updates

    void Update()
    {
        if(state == playerState.Exploring)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            //if we've stopped, save player position
            if(movement.sqrMagnitude == 0)
            {
                if (!stopped)
                {
                    stopped = true;
                    PlayerPrefs.SetFloat("PlayerX", transform.position.x);
                    PlayerPrefs.SetFloat("PlayerY", transform.position.y);
                }
            }
            else
            {
                stopped = false;
            }

            //check for menu input
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                state = playerState.Locked;
                UnityEvent myEvent = new UnityEvent();
                myEvent.AddListener(OpenMenu);
                FadeManager.FadeCross(myEvent);
            }
        }

        if(state == playerState.Interacting)
        {
            //manually zero movement
            movement = Vector2.zero;

            if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space)) && interaction != null)
            {
                //advance interaction
                interaction.Advance();
            }
        }

        anim.SetFloat("Speed", movement.sqrMagnitude);
        if(movement.x > 0)
        {
            pawn.flipX = false;
        }
        else if(movement.x < 0)
        {
            pawn.flipX = true;
        }
    }

    public void OpenMenu()
    {
        menu.gameObject.SetActive(true);
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
            //entering door
            state = playerState.Crossing;
            enterDoor = true;
            FadeManager.FadeOut();
        }
        else
        {
            //exiting door
            enterDoor = false;
            FadeManager.FadeIn();
        }
        movement = vector;
    }

    public void EndCross()
    {
        if (enterDoor)
        {
            //teleport
            string dir = "";
            if(movement.x == 0 && movement.y > 0)
            {
                //top -> bottom
                transform.position = new Vector2(transform.position.x, -6.1f);
                dir = "S";
            }
            else if(movement.x > 0 && movement.y == 0)
            {
                //right -> left
                transform.position = new Vector2(-6.5f, transform.position.y);
                dir = "W";
            }
            else if(movement.x == 0 && movement.y < 0)
            {
                //bottom -> top
                transform.position = new Vector2(transform.position.x, 5.1f);
                dir = "N";
            }
            else
            {
                //left -> right
                transform.position = new Vector2(6.5f, transform.position.y);
                dir = "E";
            }

            //spawn new room or retrieve previous one
            RoomManager.NewRoom(dir);
        }
        else
        {
            state = playerState.Exploring;
        }
    }

}

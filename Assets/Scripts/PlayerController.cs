using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    #region Variables

    //state
    public enum playerState { Exploring, Interacting, Locked };

    public playerState state;

    //movement
    private Rigidbody2D rb;
    public float moveSpeed = 3f;
    Vector2 movement;

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
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

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
        if (state == playerState.Exploring)
        {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }

    #endregion

}

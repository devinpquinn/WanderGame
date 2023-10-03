using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    public enum playerState { Exploring, Interacting, Locked };

    public playerState state;

    private Rigidbody2D rb;
    public float moveSpeed = 3f;
    Vector2 movement;

    private static PlayerController _player;
    public static PlayerController Instance { get { return _player; } }

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

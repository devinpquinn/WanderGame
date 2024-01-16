using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggedHandler : MonoBehaviour
{
    public float speed;
    private bool dragging = false;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("Data_Dragged"))
        {
            GetComponent<AudioSource>().enabled = false;
            gameObject.SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("Data_Dragged", 1);
        }
    }

    public void StartDrag()
    {
        dragging = true;
    }

    private void FixedUpdate()
    {
        if (dragging)
        {
            rb.MovePosition(rb.position + Vector2.right * speed * Time.fixedDeltaTime);
        }
    }
}

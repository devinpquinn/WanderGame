using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReflection : MonoBehaviour
{
    private SpriteRenderer myRenderer;
    public SpriteRenderer targetRenderer;

    private void Awake()
    {
        myRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        myRenderer.sprite = targetRenderer.sprite;
        myRenderer.flipX = targetRenderer.flipX;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorReflection : MonoBehaviour
{
    public float divider = 2f; //the Y position at which the player and mirror meet

    private SpriteRenderer myRenderer;
    private Transform myTransform;
    private SpriteRenderer targetRenderer;
    private Transform targetTransform;

    private Vector2 setPos = new Vector2();

    private void Awake()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        myTransform = GetComponent<Transform>();
    }

    private void Start()
    {
        targetRenderer = PlayerController.instance.GetComponent<SpriteRenderer>();
        targetTransform = PlayerController.instance.transform;
    }

    private void Update()
    {
        myRenderer.sprite = targetRenderer.sprite;
        myRenderer.flipX = targetRenderer.flipX;

        //set pos
        float setX = targetTransform.position.x;
        float setY = divider;

        if(targetTransform.position.y <= divider)
        {
            setY = divider + (divider - targetTransform.position.y);
        }
        else
        {
            setY = 999;
        }

        setPos.x = setX;
        setPos.y = setY;
        myTransform.position = setPos;
    }

    public void FlipText(bool flipped)
    {
        if (flipped)
        {
            PlayerController.instance.dialogueText.transform.localRotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            PlayerController.instance.dialogueText.transform.localRotation = new Quaternion(0, 0, 0, 0);
        }
    }
}

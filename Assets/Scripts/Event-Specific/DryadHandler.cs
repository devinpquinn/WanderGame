using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryadHandler : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("Data_Dryad_X"))
        {
            transform.position = new Vector2(PlayerPrefs.GetFloat("Data_Dryad_X"), PlayerPrefs.GetFloat("Data_Dryad_Y"));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!PlayerPrefs.HasKey("Data_Dryad"))
            {
                StartTransform();
            }
        }
    }

    public void StartTransform()
    {
        PlayerPrefs.SetInt("Data_Dryad", 1);
        anim.Play("Dryad_Transform");
        PlayerController.instance.state = PlayerController.playerState.Locked;
        PlayerController.instance.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        PlayerController.instance.gameObject.GetComponent<Animator>().Play("Player_Transform");

        PlayerPrefs.SetFloat("Data_Dryad_X", PlayerController.instance.transform.position.x);
        PlayerPrefs.SetFloat("Data_Dryad_Y", PlayerController.instance.transform.position.y);
    }

    public void EndTransform()
    {
        Vector2 playerLocation = PlayerController.instance.transform.position;
        Vector2 treeLocation = transform.position;

        transform.position = playerLocation;
        PlayerController.instance.transform.position = treeLocation;
        PlayerController.instance.state = PlayerController.playerState.Exploring;
        PlayerController.instance.gameObject.GetComponent<Animator>().Play("Player_Idle");
        anim.Play("Dryad_Tree");
    }
}

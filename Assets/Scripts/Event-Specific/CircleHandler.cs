using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleHandler : MonoBehaviour
{
    public GameObject circlers;

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("Data_Raptured"))
        {
            circlers.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PlayerController.instance.state == PlayerController.playerState.Exploring)
            {
                Rapture();
            }
        }
    }

    public void Rapture()
    {
        foreach(Animator anim in circlers.GetComponentsInChildren<Animator>())
        {
            anim.enabled = true;
        }

        GetComponent<AudioSource>().Play();

        PlayerPrefs.SetInt("Data_Raptured", 1);

        GetComponent<BoxCollider2D>().enabled = false;
    }
}

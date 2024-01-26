using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkulkHandler : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Time.timeSinceLevelLoad > 1)
        {
            anim.enabled = true;
        }
    }

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("Data_Skulk"))
        {
            gameObject.SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("Data_Skulk", 1);
        }
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("Data_Skulk", 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachHandler : MonoBehaviour
{
    private SpriteRenderer rend;
    public Sprite handDown;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    public void HandDown()
    {
        rend.sprite = handDown;
        PlayerPrefs.SetInt("Data_Reach", 1);
    }


    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("Data_Reach"))
        {
            rend.sprite = handDown;
        }
    }
}

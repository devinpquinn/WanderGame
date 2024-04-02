using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InscrutableHandler : MonoBehaviour
{
    public SpriteRenderer rend;
    public Sprite handDown;

    public void HandDown()
    {
        rend.sprite = handDown;
        PlayerPrefs.SetInt("Data_Inscrutable", 1);
    }

    
    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("Data_Inscrutable"))
        {
            rend.sprite = handDown;
        }
    }
}

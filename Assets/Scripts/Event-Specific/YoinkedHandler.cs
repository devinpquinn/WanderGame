using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoinkedHandler : MonoBehaviour
{
    public GameObject before;
    public GameObject after;

    public Rigidbody2D rb;

    private bool yoinking = false;
    private float speed = 5f;

    public void Countdown()
    {
        StartCoroutine(DoCountdown());
    }

    IEnumerator DoCountdown()
    {
        yield return new WaitForSeconds(.8f);
        GetComponent<AudioSource>().enabled = true;
        yield return new WaitForSeconds(.2f);
        PlayerController.instance.interaction.ForceEnd();
        Yoink();
    }

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("Data_Yoinked"))
        {
            gameObject.SetActive(false);
        }
    }

    public void Yoink()
    {
        before.SetActive(false);
        after.SetActive(true);
        yoinking = true;

        PlayerPrefs.SetInt("Data_Yoinked", 1);
    }

    private void FixedUpdate()
    {
        if (yoinking)
        {
            rb.MovePosition(rb.position + Vector2.left * speed * Time.fixedDeltaTime);
            if(rb.transform.localPosition.x < -4f)
            {
                yoinking = false;
            }
        }
    }
}

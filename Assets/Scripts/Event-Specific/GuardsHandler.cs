using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardsHandler : MonoBehaviour
{
    public List<Animator> anims;

    private bool trespass = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PlayerController.instance.state == PlayerController.playerState.Exploring)
        {
            trespass = true;
            foreach (Animator anim in anims)
            {
                anim.Play("Guard_Close");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && PlayerController.instance.state == PlayerController.playerState.Exploring)
        {
            trespass = false;
            StopAllCoroutines();
            StartCoroutine(AtEase());
        }
    }

    IEnumerator AtEase()
    {
        yield return new WaitForSeconds(1f);
        if (!trespass)
        {
            foreach (Animator anim in anims)
            {
                anim.Play("Guard_Open");
                trespass = false;
            }
        }
    }
}

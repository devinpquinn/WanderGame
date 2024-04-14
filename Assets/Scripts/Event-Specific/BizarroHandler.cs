using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BizarroHandler : MonoBehaviour
{
    private GameObject player;

    private void OnEnable()
    {
        player = GameObject.Find("Player");
        player.GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Persistent Environment/Blockers/Perimeter").SetActive(false);

        GameObject.Find("Persistent Environment/Blockers/Block N").SetActive(false);
        GameObject.Find("Persistent Environment/Blockers/Block W").SetActive(false);
    }

    private void FixedUpdate()
    {
        transform.position = player.transform.position;
    }

    private void OnDisable()
    {
        player.GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("Persistent Environment/Blockers/Perimeter").SetActive(true);
    }

    private void OnDestroy()
    {
        player.GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("Persistent Environment/Blockers/Perimeter").SetActive(true);
    }
}

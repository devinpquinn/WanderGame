using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFlicker : MonoBehaviour
{
    private SpriteRenderer rend;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        float interval = Random.Range(0.1f, 1.5f);
        yield return new WaitForSeconds(interval);
        SetAlpha(Random.Range(0.05f, 0.15f));
        StartCoroutine(Flicker());
    }

    public void SetAlpha(float val)
    {
        rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, val);
    }
}

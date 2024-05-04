using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassMan : MonoBehaviour
{
    private SpriteRenderer rend;
    private ParticleSystem ps;
    public bool shattered = false;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        ps = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !shattered)
        {
            shattered = true;
            rend.enabled = false;

            ps.Emit(180);
        }
    }
}

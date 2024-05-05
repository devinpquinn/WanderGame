using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassMan : MonoBehaviour, ISerializationCallbackReceiver
{
    private SpriteRenderer rend;
    private ParticleSystem ps;
    private AudioSource src;
    private bool shattered = false;

    public int id;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        ps = GetComponent<ParticleSystem>();
        src = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if(PlayerPrefs.HasKey("Data_Shattered_" + id))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !shattered)
        {
            shattered = true;
            rend.enabled = false;

            ps.Emit(240);

            src.pitch = Random.Range(0.8f, 1.2f);
            src.Play();

            PlayerPrefs.SetInt("Data_Shattered_" + id, 1);
        }
    }

    public void OnBeforeSerialize()
    {
        if (id == 0)
        {
            id = Random.Range(100000000, 999999999);
        }
    }

    public void OnAfterDeserialize()
    {
        //has to be implemented
    }
}

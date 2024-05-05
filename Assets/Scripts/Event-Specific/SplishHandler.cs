using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplishHandler : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
        StartCoroutine(DestroyAfterTime());
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}

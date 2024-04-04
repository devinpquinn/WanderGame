using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NervousHandler : MonoBehaviour
{
    private Animator anim;

    private bool left = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Jitter();
    }

    public void Jitter()
    {
        StartCoroutine(DoJitter());
    }

    public IEnumerator DoJitter()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 2f));
        if (left)
        {
            anim.Play("Nervous_TurnRight");
        }
        else
        {
            anim.Play("Nervous_TurnLeft");
        }
        left = !left;
    }
}

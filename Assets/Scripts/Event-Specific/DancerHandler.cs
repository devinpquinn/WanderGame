using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancerHandler : MonoBehaviour
{
    private Animator anim;

    private float interval = 0.1f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SetState(int index)
    {
        StartCoroutine(DoState(index));
    }

    IEnumerator DoState(int index)
    {
        yield return new WaitForSecondsRealtime(Random.Range(0, interval));

        if(index == 0)
        {
            anim.Play("Dancer_Idle");
        }
        else if(index == 1)
        {
            anim.Play("Dancer_Bob");
        }
        else
        {
            anim.Play("Dancer_Jig");
        }
    }
}

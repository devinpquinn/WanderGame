using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopperHandler : MonoBehaviour
{
    public Animator treeAnim;
    public AudioClip chopSound;

    private Animator myAnim;
    private AudioSource src;

    private void OnEnable()
    {
        GameObject.Find("Persistent Environment/Blockers/Perimeter/Blocker15").SetActive(false);
    }

    private void OnDisable()
    {
        GameObject.Find("Persistent Environment/Blockers/Perimeter/Blocker15").SetActive(true);
    }

    private void OnDestroy()
    {
        GameObject.Find("Persistent Environment/Blockers/Perimeter/Blocker15").SetActive(true);
    }

    public void ShakeTree()
    {
        treeAnim.Play("ChoppedTree_Shake");
        //src.PlayOneShot(chopSound);
    }

    public void SwitchState(bool chopping)
    {
        if (chopping)
        {
            myAnim.Play("Chopper_Chop");
        }
        else
        {
            myAnim.Play("Chopper_Idle");
        }
    }
}

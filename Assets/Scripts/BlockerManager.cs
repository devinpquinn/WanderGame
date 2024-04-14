using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerManager : MonoBehaviour
{
    public GameObject blockerN;
    public GameObject blockerE;
    public GameObject blockerS;
    public GameObject blockerW;

    //singleton
    private static BlockerManager bm;
    public static BlockerManager instance { get { return bm; } }

    private void Awake()
    {
        //singleton
        if (bm != null && bm != this)
        {
            Destroy(gameObject);
        }
        else
        {
            bm = this;

        }
    }

    public static void SetupBlockers(string key)
    {
        if(key == "TREE")
        {
            instance.blockerN.SetActive(false);
            instance.blockerE.SetActive(false);
            instance.blockerS.SetActive(false);
            instance.blockerW.SetActive(false);
        }
        else
        {
            if (key.ToLower().Contains("n"))
            {
                instance.blockerN.SetActive(false);
            }
            else
            {
                instance.blockerN.SetActive(true);
            }
            if (key.ToLower().Contains("e"))
            {
                instance.blockerE.SetActive(false);
            }
            else
            {
                instance.blockerE.SetActive(true);
            }
            if (key.ToLower().Contains("s"))
            {
                instance.blockerS.SetActive(false);
            }
            else
            {
                instance.blockerS.SetActive(true);
            }
            if (key.ToLower().Contains("w"))
            {
                instance.blockerW.SetActive(false);
            }
            else
            {
                instance.blockerW.SetActive(true);
            }
        }
    }
}

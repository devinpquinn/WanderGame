using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppedHandler : MonoBehaviour
{

    public Transform bloodOrigin;
    public GameObject ps;

    public void ChoppedSound()
    {
        Instantiate(ps, bloodOrigin.transform.position, Quaternion.identity, bloodOrigin);
    }
}

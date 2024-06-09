using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlabManager : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerController.instance.transform.Find("PlayerReflection").gameObject.SetActive(true);
        PlayerController.instance.gameObject.GetComponent<FootstepSounds>().grass = false;
    }

    private void OnDisable()
    {
        PlayerController.instance.transform.Find("PlayerReflection").gameObject.SetActive(false);
        PlayerController.instance.gameObject.GetComponent<FootstepSounds>().grass = true;
    }
}

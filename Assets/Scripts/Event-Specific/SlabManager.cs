using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlabManager : MonoBehaviour
{
    public List<GameObject> guys;

    private void Start()
    {
        //check if we're returning to this room
        if (PlayerPrefs.HasKey("Data_Slab"))
        {
            gameObject.SetActive(false);
        }
        else
        {
            PlayerPrefs.SetInt("Data_Slab", 1);

            StartCoroutine(StaggerGuys());
        }
    }

    IEnumerator StaggerGuys()
    {
        foreach (GameObject b in guys)
        {
            b.SetActive(false);
        }

        int index = 0;
        while (index < guys.Count)
        {
            yield return new WaitForSeconds(Random.Range(0, 0.25f));
            guys[index].SetActive(true);
            index++;
        }
    }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardsHandler : MonoBehaviour
{
    public List<Animator> anims;

    private bool trespass = false;

    public Interaction interaction;

    private List<string> entry = new List<string> { "entry", "admittance", "passage", "further" };
    private List<string> title = new List<string> { "Her Benevolence", "Her Worshipfulness", "Her Graciousness", "Her Beneficence", "Her Magnificence" };
    private List<string> bestow = new List<string> { "accepting", "hosting", "welcoming", "bestowing her favors upon", "blessing", "offering her time to" };
    private List<string> guest = new List<string> { "supplicants", "petitioners", "admirers", "vagrants", "guests", "stray dogs", "travellers" };
    private List<string> time = new List<string> { "at this time", "currently", "right now", "for the time being" };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            trespass = true;
            foreach (Animator anim in anims)
            {
                anim.Play("Guard_Close");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            trespass = false;
            StopCoroutine(AtEase());
            StartCoroutine(AtEase());
        }
    }

    private void OnEnable()
    {
        StartCoroutine(OpenPath());
    }

    public void RandomizeDialog()
    {
        string first = "No " + GetEntry();
        string second = GetTitle() + " is not " + GetBestow() + " " + GetGuest() + " " + GetTime();

        interaction.dialogs[0].lines[0] = "\"" + first +  ".\"";
        interaction.dialogs[0].lines[1] = "\"" + second + ".\"";
    }

    public string GetEntry()
    {
        int i = Random.Range(0, entry.Count);
        return entry[i];
    }

    public string GetTitle()
    {
        int i = Random.Range(0, title.Count);
        return title[i];
    }

    public string GetBestow()
    {
        int i = Random.Range(0, bestow.Count);
        return bestow[i];
    }

    public string GetGuest()
    {
        int i = Random.Range(0, guest.Count);
        return guest[i];
    }

    public string GetTime()
    {
        int i = Random.Range(0, time.Count);
        return time[i];
    }

    IEnumerator AtEase()
    {
        yield return new WaitForSeconds(1f);
        if (!trespass)
        {
            foreach (Animator anim in anims)
            {
                anim.Play("Guard_Open");
                trespass = false;
            }
        }
    }

    IEnumerator OpenPath()
    {
        yield return new WaitForEndOfFrame();
        BlockerManager.SetupBlockers("NES");
    }
}

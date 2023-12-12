using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarHandler : MonoBehaviour
{
    string altarText = "The glowing inscription reads: ";
    public Interaction i;

    public void SetInscription()
    {
        string toAdd = "Test";

        altarText += toAdd;
        i.dialogs[0].lines[1] = altarText;
    }
}

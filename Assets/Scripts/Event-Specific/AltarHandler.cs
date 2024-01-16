using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarHandler : MonoBehaviour
{
    string altarText;
    public Interaction i;

    public void SetInscription()
    {
        altarText = "The glowing inscription reads:\n\n<smallcaps>";
        string toAdd = "Null";

        try
        {
            toAdd = "\"o " + System.Environment.UserName.ToLower();
        }
        finally
        {
            toAdd += "\n\n preserve us\"";
            altarText += toAdd;
            i.dialogs[0].lines[1] = altarText;
        }
    }
}

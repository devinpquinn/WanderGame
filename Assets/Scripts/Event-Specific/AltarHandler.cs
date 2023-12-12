using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarHandler : MonoBehaviour
{
    string altarText;
    public Interaction i;

    public void SetInscription()
    {
        altarText = "The glowing inscription reads:\n\n";
        string toAdd = "Null";

        try
        {
            toAdd = "\"O " + System.Environment.UserName.ToUpper();
        }
        finally
        {
            toAdd += "\n\n CARRY US\"";
            altarText += toAdd;
            i.dialogs[0].lines[1] = altarText;
        }
    }
}

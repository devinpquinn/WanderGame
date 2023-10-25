using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTextHandler : MonoBehaviour
{
    public void AdvanceText()
    {
        PlayerController.instance.interaction.UpdateText();
        PlayerController.instance.interaction.advancing = false;
    }
}

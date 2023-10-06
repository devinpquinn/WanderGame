using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Interaction : MonoBehaviour
{
    public List<DialogText> dialogs;
    private int dialogIndex = 0;
    private int lineIndex = 0;

    private GameObject dialogueCard;
    private TextMeshProUGUI dialogueText;

    [System.Serializable]
    public class DialogText
    {
        [TextArea]
        public List<string> lines; //the text blocks that make up this event

        public DialogText()
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Time.timeSinceLevelLoad > 1)
        {
            if (PlayerController.instance.state == PlayerController.playerState.Exploring)
            {
                Interact();
            }
        }
    }

    private void Start()
    {
        dialogueCard = PlayerController.instance.dialogueCard;
        dialogueText = PlayerController.instance.dialogueText;

        //check if loading save state?
    }

    public void Interact()
    {
        if(dialogIndex < dialogs.Count)
        {
            //lock player
            PlayerController.instance.state = PlayerController.playerState.Interacting;
            PlayerController.instance.interaction = this;

            //start playing text
            dialogueCard.SetActive(true);
            lineIndex = 0;
            dialogueText.SetText(dialogs[dialogIndex].lines[lineIndex]);
        }
    }

    public void Advance()
    {
        lineIndex++;

        //get next line of dialogue, check for events or end of interaction
        if (dialogs[dialogIndex].lines.Count > lineIndex)
        {
            //continue dialogue
            dialogueText.SetText(dialogs[dialogIndex].lines[lineIndex]);
        }
        else
        {
            //end dialogue
            dialogIndex++;
            dialogueCard.SetActive(false);
            PlayerController.instance.state = PlayerController.playerState.Exploring;

            //save dialogue state?
        }
    }
}

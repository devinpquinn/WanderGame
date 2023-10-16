using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Interaction : MonoBehaviour, ISerializationCallbackReceiver
{
    public int id = 0; //used to track this interaction for saving/loading

    public List<DialogText> dialogs;
    public List<UnityEvent> events;
    private int dialogIndex = 0;
    private int lineIndex = 0;

    private GameObject dialogueCard;
    private TextMeshProUGUI dialogueText;
    private RectTransform alignment;

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
        alignment = PlayerController.instance.alignment;

        //check if loading save state?
        if(PlayerPrefs.HasKey("Dialog_" + id))
        {
            dialogIndex = PlayerPrefs.GetInt("Dialog_" + id);
        }
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
            LayoutRebuilder.ForceRebuildLayoutImmediate(alignment);

            //play event
            if(events.Count > dialogIndex && events[dialogIndex] != null)
            {
                events[dialogIndex].Invoke();
            }
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
            LayoutRebuilder.ForceRebuildLayoutImmediate(alignment);
        }
        else
        {
            //end dialogue
            if(id > 0)
            {
                dialogIndex++;
            }
            
            dialogueCard.SetActive(false);
            PlayerController.instance.state = PlayerController.playerState.Exploring;

            //save dialogue state?
            if(id > 0)
            {
                PlayerPrefs.SetInt("Dialog_" + id, dialogIndex);
            }
        }
    }

    public void OnBeforeSerialize()
    {
        if (id == 0)
        {
            id = Random.Range(100000000, 999999999);
        }
    }

    public void OnAfterDeserialize()
    {
        //has to be implemented
    }
}

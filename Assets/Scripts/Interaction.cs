using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Interaction : MonoBehaviour, ISerializationCallbackReceiver
{
    public int id = 0; //used to track this interaction for saving/loading

    public bool repeatLast = false;
    public List<DialogText> dialogs;
    public List<UnityEvent> events;
    private int dialogIndex = 0;
    private int lineIndex = 0;

    private GameObject dialogueCard;
    private Animator anim;
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
        anim = dialogueCard.GetComponent<Animator>();
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
        //check if this is a repeat and set index
        if (repeatLast && dialogIndex >= dialogs.Count)
        {
            dialogIndex = dialogs.Count - 1;
        }

        if (dialogIndex < dialogs.Count)
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

            //duck audio
            RandomMusic.Fade(0.1f, 0.25f);
        }
    }

    public void Advance()
    {
        lineIndex++;

        //get next line of dialogue, check for events or end of interaction
        if (dialogs[dialogIndex].lines.Count > lineIndex)
        {
            //animate
            anim.Play("Dialog_Chew", 0, 0f);
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

            //unduck audio
            RandomMusic.Fade(0.1f, 1f);
        }

        //sfx
        UISounds.Advance();
    }

    public void UpdateText()
    {
        dialogueText.SetText(dialogs[dialogIndex].lines[lineIndex]);
        LayoutRebuilder.ForceRebuildLayoutImmediate(alignment);
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

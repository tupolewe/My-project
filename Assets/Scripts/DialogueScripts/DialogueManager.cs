using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI speakerNameText;
    public TextMeshProUGUI dialogueText; 
    public Sprite speakerImage; 

    public DialogueScript currentDialogueScript;
    public int currentLineIndex;

    public PlayerRayCast playerRayCast;
    public PlayerMovement playerMovement;
    public DialogueHUD dialogueHUD;

    public void StartDialogue(DialogueScript dialogueScript)
    {
        playerMovement.inBattle = true;
        currentDialogueScript = dialogueScript;
        currentLineIndex = 0;
        DisplayCurrentLine();
    }


    public void DisplayCurrentLine()
    {
        if (currentLineIndex < currentDialogueScript.dialogueLines.Count)
        {
            DialogueLine line = currentDialogueScript.dialogueLines[currentLineIndex];
            dialogueHUD.speakerName.text = line.speakerName;
            dialogueHUD.dialogue.text = line.dialogueText;
            dialogueHUD.speakerImage.sprite = line.speakerImage;
            
            Debug.Log("koniec open dialog");

            
        }
        else
        {
            EndDialogue();
        }
    }

    public void NextLine()
    {
        if(Input.GetKeyDown(KeyCode.Space) && currentDialogueScript != null) 
        {
            currentLineIndex++;
            DisplayCurrentLine();
        }
        
       
       
    }


    public void EndDialogue()
    {
        dialogueHUD.gameObject.SetActive(false);
        playerMovement.inBattle = false;
        currentLineIndex = 0;
        currentDialogueScript = null;

        Debug.Log("Dialogue Ended");
    }

    public void Update()
    {
        NextLine();
    }

    
}

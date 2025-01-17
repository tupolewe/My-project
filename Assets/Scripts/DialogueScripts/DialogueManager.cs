using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI speakerNameText;
    public TextMeshProUGUI dialogueText; 
    public Sprite speakerImage; 

    public DialogueScript currentDialogueScript;
    public int currentLineIndex;

    public PlayerRayCast playerRayCast;
    public DialogueHUD dialogueHUD;

    public void StartDialogue(DialogueScript dialogueScript)
    {
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
            dialogueHUD.speakerImage = line.speakerImage;
            Debug.Log("koniec open dialog");

            
        }
        else
        {
            EndDialogue();
        }
    }

    public void NextLine()
    {
        currentLineIndex++;
        DisplayCurrentLine();
    }


    public void EndDialogue()
    {
        speakerNameText.text = null;
        dialogueText.text = null;
        speakerImage = null;
        Debug.Log("Dialogue Ended");
    }
}

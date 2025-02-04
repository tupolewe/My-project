using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    public NPC_Interaction npc;

    public BattleSystem battleSystem;
    public BattleHUD battleHUD;

    

    public void StartDialogue(DialogueScript dialogueScript)
    {
        playerRayCast.interacted = true;
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
            
            if (line.choices != null && line.choices.Count > 0) 
            {
                DisplayChoices(line.choices);   
            }
            else
            {
                dialogueHUD.ClearChoices();
            }

            
        }
        else
        {
            EndDialogue();
        }
    }

    public void NextLine()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentDialogueScript != null)
        {
            if (currentLineIndex < currentDialogueScript.dialogueLines.Count - 1)
            {
                currentLineIndex++;
                DisplayCurrentLine();
            }
            else
            {
                EndDialogue();
            }
        }
    }


    public void EndDialogue()
    {
        if (currentLineIndex < currentDialogueScript.dialogueLines.Count)
        {
            DialogueLine line = currentDialogueScript.dialogueLines[currentLineIndex];
            playerRayCast.interacted = false;
            

            if (line.combatLine)
            {
                Debug.Log("Dialogue Ended with fight");
                dialogueHUD.gameObject.SetActive(false);
                currentLineIndex = 0;
                currentDialogueScript = null;
                battleSystem.StartCoroutine(battleSystem.SetupBattle());
                
            }
            else
            {
                Debug.Log("Dialogue Ended without fight");
                playerMovement.inBattle = false;
                dialogueHUD.gameObject.SetActive(false);
                currentLineIndex = 0;
                currentDialogueScript = null;
            }

            if(line.endingLine) 
            {
                npc.dialogueScript = line.nextDialogue;
                Debug.Log("next dialogue");
            }
        }
        else
        {
            Debug.LogError("EndDialogue called with an out-of-range index!");
        }

        
    }



    public void DisplayChoices(List<DialogueChoice> choices)
    {


        dialogueHUD.ClearChoices();

        for (int i = 0; i < choices.Count; i++)
        {
            int capturedIndex = i; // Capture the current index in a local variable
            Button choiceButton = dialogueHUD.CreateChoiceButton(capturedIndex);
            choiceButton.GetComponentInChildren<TextMeshProUGUI>().text = choices[capturedIndex].choiceText;
            choiceButton.GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = true;
            choiceButton.GetComponentInChildren<TextMeshProUGUI>().fontSizeMin = 12;
            choiceButton.GetComponentInChildren<TextMeshProUGUI>().fontSizeMax = 20;

            choiceButton.onClick.AddListener(() =>
            {
                HandleChoiceSelection(choices[capturedIndex]);
            });
        }
    }

    public void HandleChoiceSelection(DialogueChoice choice)
    {
        if (choice.nextDialogue != null)
        {
            StartDialogue(choice.nextDialogue);
        }
        else
        {
            EndDialogue();
        }
    }

    public void Update()
    {
        NextLine();
    }

    
}

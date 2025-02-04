using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Dialogue Line", menuName = "Dialogue/Dialogue Line")]

public class DialogueLine : ScriptableObject
{
    public string speakerName; // Name of the speaker
    public string dialogueText; // Dialogue text
    public Sprite speakerImage;
    public bool combatLine;
    public bool endingLine;
    public List<DialogueChoice> choices; // branching choices 
    public DialogueScript nextDialogue; //the dialogue script to load after the transition 
}

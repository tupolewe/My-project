using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Script", menuName = "Dialogue/Dialogue Script")]
public class DialogueScript : ScriptableObject
{
    public List<DialogueLine> dialogueLines;
}

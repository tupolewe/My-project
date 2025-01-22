using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ChoiceName", menuName = "Scriptable Objects/DialogueChoice" )]
public class DialogueChoice : ScriptableObject
{
    public string Text;
    public DialogueChoice[] Choices;
    Button buttonPrefab;
    DialogueChoice choice;

    
}

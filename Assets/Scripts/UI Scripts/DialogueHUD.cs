using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DialogueHUD : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public Image speakerImage;
    public TextMeshProUGUI speakerName;
    public TextMeshProUGUI dialogue;


    

    public void Start()
    {
        gameObject.SetActive(false);
    }
}

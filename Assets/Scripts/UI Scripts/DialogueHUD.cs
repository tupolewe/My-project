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


    public Transform choicesContainer;
    public Button choiceButtonPrefab;


    public void ClearChoices()
    {
        foreach (Transform child in choicesContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public Button CreateChoiceButton(int index)
    {
        Button button = Instantiate(choiceButtonPrefab, choicesContainer);

        RectTransform rectTransform = button.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(-index * 400, 0); // Adjust X offset for spacing

        return button;
    }



    public void Start()
    {
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class NPC_Interaction : MonoBehaviour, Interactable
{
    public NPC_prefab npc;

    public TextMeshPro nameText;
    
    public Image artwork;

    public BattleSystem battleSystem;

    public PlayerStats playerStats;

    public static int npcHealth;
    [Header("Dialogue Related Variables")]
    [Space]
    public DialogueManager dialogueManager;
    public DialogueScript dialogueScript;
    public DialogueHUD dialogueHUD;

    public void Start()
    {
        npc.health = npc.maxHealth;
        npc.stamina = npc.maxStamina;
    }

    public void Interact()
    {
        dialogueHUD.gameObject.SetActive(true);
        dialogueManager.npc = this; 
        dialogueManager.StartDialogue(dialogueScript);

        //battleSystem.StartCoroutine(battleSystem.SetupBattle());
    }

    public bool TakeDamage(PlayerStats playerStats)
    {
        Debug.Log("taken damage");
        npc.health -= playerStats.damage; 

        if (npc.health <= 0)
        {   
            npc.health = 0;
            Debug.Log("death");
            return true;
        }
        else 
            return false;   
    }

    public int GetEnemyDamage()
    {
        return Random.Range(npc.minDamage, npc.maxDamage);
    }

    public bool GetEnemyStamina()
    {
        if (npc.stamina >= 2)
        {
            npc.stamina -= 2;
            return true;
        }
        else if (npc.stamina <= 0)
        {
            npc.stamina = 0;
            return false;
        }

        else if (npc.stamina == 1)
        {
            return false;
        }
        else { return true; }
            
    }
}

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

    public void Start()
    {
        npc.health = npc.maxHealth;
    }

    public void Interact()
    {
        battleSystem.StartCoroutine(battleSystem.SetupBattle());
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


}

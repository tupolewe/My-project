using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public int lvl;
    public int health;
    public int damage;
    public int stamina;
    public int agility;

    public int minDamage;
    public int maxDamage;

    public BattleSystem battleSystem;
    public NPC_prefab npc; 
    public NPC_Interaction npcInteraction;
    public PlayerRayCast playerRayCast;
    
   public int GetDamage()
    {
        return Random.Range(minDamage, maxDamage);
        
    }


    public bool PlayerTakeDamage(NPC_prefab nPC_Prefab)
    {
        Debug.Log("taken damage");
        health -= nPC_Prefab.damage;

        if (health <= 0)
        {
            Debug.Log("death");
            health = 0;
            return true;
        }
        else
            return false;
    }

   

        
}

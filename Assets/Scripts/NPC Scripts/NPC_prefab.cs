using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New NPC", menuName = "NPC")]
public class NPC_prefab : ScriptableObject
{
    public new string name;
    public int lvl;

    public int health; 
    public int damage;
    public int stamina; 

    
}

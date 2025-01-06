using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BattleHUD : MonoBehaviour
{
    public BattleSystem battleSystem;
    public BattleState battleState;
    public TextMeshProUGUI playerBattleDialogue;
    public TextMeshProUGUI enemyBattleDialogue;
    public Canvas battleCanvas;
    
    void Start()
    {
        battleCanvas.gameObject.SetActive(false);
    }


    public void OnAttackButton()
    {
        
        if (battleSystem.battleState == BattleState.PLAYERTURN)
        {
            battleSystem.PlayerAttack();
        }
           
       
    }

    public void OnHealButton()
    {
        if (battleSystem.battleState != BattleState.PLAYERTURN)
            return;

        //StartCoroutine(PlayerHeal());
    }


}

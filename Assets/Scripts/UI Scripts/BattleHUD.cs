using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BattleHUD : MonoBehaviour
{
    public BattleSystem battleSystem;
    public BattleState battleState;

    [Header("Text Related Variables")]
    public TextMeshProUGUI playerBattleDialogue;
    public TextMeshProUGUI enemyBattleDialogue;
    public TextMeshProUGUI playerHealthHUD;
    public TextMeshProUGUI enemyHealthHUD;
    public TextMeshProUGUI enemyName;
    public TextMeshProUGUI playerStaminaHUD;
    public TextMeshProUGUI enemyStaminaHUD;
    [Space]
    public Canvas battleCanvas;
    public Image enemyImage; 
    public DialogueManager dialogueManager;
    public SoundController soundController;
    
    void Start()
    {
        battleCanvas.gameObject.SetActive(false);
    }


    public void OnAttackButton()
    {
        
        if (battleSystem.battleState == BattleState.PLAYERTURN)
        {
            StartCoroutine(battleSystem.PlayerAttack());
            soundController.AttackSound();
        }
           
       
    }

    public void OnHealButton()
    {
        
        if (battleSystem.battleState == BattleState.PLAYERTURN)
        {
            battleSystem.PlayerHeal();
            
        }
            

       
    }

    public void OnWaitButton()
    {
        if (battleSystem.battleState == BattleState.PLAYERTURN)
        {
            battleSystem.PlayerWait();

        }
    }


}

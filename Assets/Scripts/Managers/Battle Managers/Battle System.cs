using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}
public class BattleSystem : MonoBehaviour
{
    public BattleState battleState;

    [Header ("Player Related Variables")]
    public GameObject player;
    public PlayerRayCast playerRayCast;
    public PlayerMovement playerMovement;
    public PlayerStats playerStats;
    public Transform playerBattlePosition;
    [Space]
    [Header("Enemy Related Variables")]
    public GameObject enemy;
    public NPC_prefab npc;
    public NPC_Interaction npcInteraction;
    public Transform enemyBattlePosition;
    [Space]
    [Header("Battle View")]
    [Space]
    public Camera mainCamera;
    public Transform battleCameraPosition;
    public Canvas battleCanvas;
    public BattleHUD battleHUD;
    public Transform cameraPos;
    [Header("Animation Controllers")]
    [Space] 
    public NPCBattleAnimationController npcBattleAnimCon;
    [Header("Dice Roll")]
    [Space]
    public DiceRoll diceRoll;
    public SoundController soundController;


    public void Update()
    {
        RayCastNPCCheck();
        
        
    }

    #region IENUMERATORS
    public IEnumerator SetupBattle()
    {

   

        enemy = playerRayCast.hitObject;
       mainCamera.gameObject.transform.SetParent(null);
       battleState = BattleState.START;
       playerMovement.inBattle = true;
       battleCanvas.gameObject.SetActive(true);
       player.transform.position = playerBattlePosition.position;
        player.transform.localScale += new Vector3(5, 5, 0);
       enemy.transform.position = enemyBattlePosition.position;
       mainCamera.gameObject.transform.position = battleCameraPosition.transform.position;
       npcInteraction = enemy.GetComponent<NPC_Interaction>();
        soundController.CombatTheme();
       battleHUD.enemyImage.sprite = npcInteraction.artwork.sprite;



        battleHUD.playerBattleDialogue.text = "The fight starts now!";
       UpdateHUDStats();

       yield return new WaitForSeconds(2f);

       battleState = BattleState.PLAYERTURN;
       PlayerTurn();
    }

    IEnumerator EnemyTurn()
    {

        yield return new WaitForSeconds(1f);
        battleHUD.enemyBattleDialogue.text = "Juz po tobie";

        if (npcInteraction.GetEnemyStamina())
        {
            
            EnemyRoll();
            EnemyHitChance();
          
            if (EnemyHitChance())
            {
                EnemyDamage();
                bool isDead = playerStats.PlayerTakeDamage(npcInteraction.npc);


                yield return new WaitForSeconds(1f);

                if (isDead)
                {
                    battleState = BattleState.LOST;
                    battleHUD.playerBattleDialogue.text = "You lost!";
                    StartCoroutine(EndBattle());
                }
                else
                {
                    battleHUD.playerBattleDialogue.text = "You took damage";

                    yield return new WaitForSeconds(1f);
                    battleState = BattleState.PLAYERTURN;
                    PlayerTurn();
                }
            }
            else
            {
                Debug.Log("enemy missed");
                battleHUD.playerBattleDialogue.text = "Enemy missed!";
                yield return new WaitForSeconds(1f);
                battleState = BattleState.PLAYERTURN;
                PlayerTurn();
            }
        }
        else 
        { battleHUD.enemyBattleDialogue.text = "ni ma sily";
            //yield return new WaitForSeconds(1f);
            npcInteraction.npc.stamina += 1;
            battleState = BattleState.PLAYERTURN;
            PlayerTurn();
        }

        //npcInteraction.npc.stamina += 1;
        UpdateHUDStats();

    }

    IEnumerator EndBattle()
    {
      
        if(battleState == BattleState.WON) 
        {
            yield return new WaitForSeconds(1f);

            battleHUD.playerBattleDialogue.text = "YOU WON!";


            yield return new WaitForSeconds(1f);

            mainCamera.gameObject.transform.SetParent(cameraPos);
            mainCamera.transform.position = cameraPos.transform.position;
            playerMovement.inBattle = false;
            battleCanvas.gameObject.SetActive(false);
            EnemyHealthReset();
            EndPositionSetup();
        }
        else if (battleState == BattleState.LOST)
        {
            yield return new WaitForSeconds(1f);

            battleHUD.playerBattleDialogue.text = "YOU LOST!";


            yield return new WaitForSeconds(1f);

            mainCamera.gameObject.transform.SetParent(cameraPos);
            mainCamera.transform.position = cameraPos.transform.position;
            playerMovement.inBattle = false;
            battleCanvas.gameObject.SetActive(false);
            EnemyHealthReset();
            EndPositionSetup();
        }

        soundController.MainTheme();
        player.transform.localScale -= new Vector3(5, 5, 0);
    }

    public IEnumerator PlayerAttack()
    {
       
        if (playerStats.GetStamina())
        {
            PlayerRoll();
            PlayerHitChance();
            playerMovement.animator.SetBool("isAttacking", true);

            if (PlayerHitChance())
            {
                PlayerDamage();
                bool isDead = npcInteraction.TakeDamage(playerStats);




                if (isDead)
                {
                    battleState = BattleState.WON;
                    battleHUD.playerBattleDialogue.text = "Enemy defeated!";
                    StartCoroutine(EndBattle());
                }
                else
                {
                    battleHUD.enemyBattleDialogue.text = "ALAAA";
                    battleHUD.playerBattleDialogue.text = "Enemy still alive!";
                    battleState = BattleState.ENEMYTURN;
                    StartCoroutine(EnemyTurn());
                }
            }
            else
            {
                battleHUD.playerBattleDialogue.text = "YOU MISSED";
                battleState = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
        else 
        {
            battleHUD.playerBattleDialogue.text = "You have no stamina!";
            battleState = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        
       
        UpdateHUDStats();
        //playerStats.stamina += 1;
        yield return new WaitForSeconds(0.5f);
        playerMovement.animator.SetBool("isAttacking", false);



    }
    #endregion

    #region  PLAYER ACTION METHODS
    public void PlayerTurn()
    {
        battleHUD.playerBattleDialogue.text = "Your turn!";
    }

    

    public void PlayerHeal()
    {
        playerStats.health+=2;
        battleHUD.playerBattleDialogue.text = "You healed";
        UpdateHUDStats();
        battleState = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    public void PlayerWait()
    {
        playerStats.stamina+=3;
        battleHUD.playerBattleDialogue.text = "Stamina Restored";
        UpdateHUDStats();
        battleState = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
    #endregion 


    public void UpdateHUDStats() // updates the stats that are displayed on the screen 
    {
        battleHUD.enemyName.text = npcInteraction.npc.name;
        battleHUD.playerHealthHUD.text = "Health:" + playerStats.health.ToString();
        battleHUD.enemyHealthHUD.text = "Health:" + npcInteraction.npc.health.ToString();
        battleHUD.playerStaminaHUD.text = "Stamina:" + playerStats.stamina.ToString();
        battleHUD.enemyStaminaHUD.text = "Stamina:" + npcInteraction.npc.stamina.ToString();
    }

    public void RayCastNPCCheck()
    {
        if (playerMovement.inBattle == false)
        {
            enemy = playerRayCast.hitObject;
        }
    }

    public void EndPositionSetup()
    {
        // Access the static player position
        Vector2 battleEndPosition = PlayerRayCast.playerInteractPos;

        // Move the player back to the original position
        player.transform.position = battleEndPosition;

        // Access the static enemy position
        Vector2 npcBattleEndPosition = PlayerRayCast.npcInteractPos;

        // Move the enemy back to the original position
        enemy.transform.position = npcBattleEndPosition;
    }
    #region HEALTH & DMG METHODS 
    public void PlayerDamage()
    {
        int damageDealt = playerStats.GetDamage();
        playerStats.damage = damageDealt;
    }

    public void EnemyDamage()
    {
        int damageDealt = npcInteraction.GetEnemyDamage();
        npcInteraction.npc.damage = damageDealt;
    }

    public void EnemyHealthReset()
    {
        npcInteraction.npc.health = npcInteraction.npc.maxHealth;
    }
    #endregion

    #region ROLLS METHODS
    public int PlayerRoll()
    {
        diceRoll.RollDice();
        if(diceRoll.RollDice() > 0)
            return diceRoll.RollDice() + playerStats.agility;
          
        else 
            return 0;

    }

    public bool PlayerHitChance()
    {
        if(PlayerRoll() > 6)
            return true;
        else 
            return false;
    }

    public int EnemyRoll()
    {
        diceRoll.RollDice();
        if (diceRoll.RollDice() > 0)
            return diceRoll.RollDice() + npcInteraction.npc.agility;

        else
            return 0;
    }

    public bool EnemyHitChance() 
    {
        if (EnemyRoll() > 6)
            return true;
        else
            return false;
    }
    #endregion
}

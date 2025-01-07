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
       enemy.transform.position = enemyBattlePosition.position;
       mainCamera.gameObject.transform.position = battleCameraPosition.transform.position;
       npcInteraction = enemy.GetComponent<NPC_Interaction>();

       
       battleHUD.playerBattleDialogue.text = "The fight starts now!";
       UpdateHUDStats();

       yield return new WaitForSeconds(2f);

       battleState = BattleState.PLAYERTURN;
       PlayerTurn();
    }

    IEnumerator EnemyTurn()
    {

        battleHUD.enemyBattleDialogue.text = "Ale ci zaraz dopierdole";

        yield return new WaitForSeconds(1f);

        bool isDead = playerStats.PlayerTakeDamage(npcInteraction.npc);
        UpdateHUDStats();

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            battleState = BattleState.LOST;
            battleHUD.playerBattleDialogue.text = "You lost!";
        }
        else
        {
            battleHUD.playerBattleDialogue.text = "you took damage";

            yield return new WaitForSeconds(1f);
            battleState = BattleState.PLAYERTURN;
            PlayerTurn();
        }

        

    }

    IEnumerator EndBattle()
    {
        yield return new WaitForSeconds(1f);

        battleHUD.playerBattleDialogue.text = "you WON!";
        

        yield return new WaitForSeconds(1f);

        mainCamera.gameObject.transform.SetParent(cameraPos);
        mainCamera.transform.position = cameraPos.transform.position;
        playerMovement.inBattle = false;
        battleCanvas.gameObject.SetActive(false);

        EndPositionSetup();

        





    }

    public IEnumerator PlayerAttack()
    {
        bool isDead = npcInteraction.TakeDamage(playerStats);
        playerMovement.animator.SetBool("isAttacking", true);
        UpdateHUDStats();

        if (isDead)
        {
            battleState = BattleState.WON;
            battleHUD.playerBattleDialogue.text = "Enemy defeated!";
            StartCoroutine(EndBattle());
        }
        else
        {
            battleHUD.playerBattleDialogue.text = "Enemy still alive!";
            battleState = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

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
        StartCoroutine(EnemyTurn());
    }
    #endregion 


    public void UpdateHUDStats() // updates the stats that are displayed on the screen 
    {
        battleHUD.enemyName.text = npcInteraction.npc.name;
        battleHUD.playerHealthHUD.text = "Health:" + playerStats.health.ToString();
        battleHUD.enemyHealthHUD.text = "Health:" + npcInteraction.npc.health.ToString();
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
}

using System.Collections;
using System.Collections.Generic;
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

    public void Update()
    {
        enemy = playerRayCast.hitObject;
        
        
    }

    #region IENUMERATORS
    public IEnumerator SetupBattle()
    {
        
       mainCamera.gameObject.transform.SetParent(null);
       battleState = BattleState.START;
       playerMovement.inBattle = true;
       battleCanvas.gameObject.SetActive(true);
       player.transform.position = playerBattlePosition.position;
       enemy.transform.position = enemyBattlePosition.position;
       mainCamera.gameObject.transform.position = battleCameraPosition.transform.position;
       npcInteraction = enemy.GetComponent<NPC_Interaction>();
       

       battleHUD.playerBattleDialogue.text = "The fight starts now!";

       yield return new WaitForSeconds(2f);

       battleState = BattleState.PLAYERTURN;
       PlayerTurn();
    }

    IEnumerator EnemyTurn()
    {

        battleHUD.enemyBattleDialogue.text = "Ale ci zaraz dopierdole";

        yield return new WaitForSeconds(1f);

        bool isDead = playerStats.PlayerTakeDamage(npcInteraction.npc);

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


        // Access the static player position
        Vector2 battleEndPosition = PlayerRayCast.playerInteractPos;

        // Move the player back to the original position
        player.transform.position = battleEndPosition;



    }
    #endregion

    #region  PLAYER ACTIONS
    public void PlayerTurn()
    {
        battleHUD.playerBattleDialogue.text = "Your turn!";
    }

    public void PlayerAttack()
    {
        bool isDead = npcInteraction.TakeDamage(playerStats);

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
    }
    #endregion 

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, PLAYERATTACK, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    public GameObject playerPrefab;
    public GameObject[] enemyPrefab;
    [SerializeField] PlayerManager playerManager;
    [SerializeField] EnemyBehaviour enemyUnit;
    
    /*
    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Player playerUnit;
    Player enemyUnit;
    */

    public BattleHud playerHUD;
    public BattleHud enemyHUD;
    [SerializeField] HUDController hudController;


    private void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        /*
        int randomIndex = Random.Range(0, enemyPrefab.Length);

        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Player>();
        */

        /*
        GameObject enemyGo = Instantiate(enemyPrefab[randomIndex], enemyBattleStation);
        enemyUnit = enemyGo.GetComponent<Player>();
        */


        hudController.ChangeDialogueText("A test " + enemyUnit.myName + " approaches ");


        hudController.SetPlayerHUD(playerManager);
        hudController.SetEnemyHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();

    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerManager.MechAttackCheck());

        hudController.UpdateEnemyHp();

        yield return new WaitForSeconds(2f);

        if( isDead )
        {
            state = BattleState.WON;
            EndBattle();
        } else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }


        
    }

    IEnumerator EnemyTurn()
    {
        hudController.ChangeDialogueText(enemyUnit.myName + " Turn ");

        yield return new WaitForSeconds(1f);

        int actionTurn = Random.Range(0, 10);

        bool isDead = false;

        Debug.Log("Attacked");

        isDead = playerManager.TakeDamage(enemyUnit.DamageCheck());
        hudController.UpdatePlayerHp();

        yield return new WaitForSeconds(2f);


        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {

            state = BattleState.PLAYERTURN;

            EventManager.PlayerTurnFunction();

            hudController.ChangeDialogueText("Player 1 Turn ");
        }

    }

    void PlayerTurn()
    {
        hudController.ChangeDialogueText("Choose an action:");
        hudController.UpdateEnemyIntent();
        playerManager.DrawCards();
    }

    public void OnAttackButton()
    {
        Debug.Log("OnAttackButton");
        if (state != BattleState.PLAYERTURN )
        {
            return;
        }
        state = BattleState.PLAYERATTACK;
        StartCoroutine(PlayerAttack());
    }

    void EndBattle()
    {
        if( state == BattleState.WON )
        {
            hudController.ChangeDialogueText("You won!");
        }
        if( state == BattleState.LOST )
        {
            hudController.ChangeDialogueText("You lost!");
        }
    }
}

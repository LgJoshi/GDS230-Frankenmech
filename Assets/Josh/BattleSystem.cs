using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    public GameObject playerPrefab;
    public GameObject[] enemyPrefab;
    [SerializeField] PlayerManager playerManager;

    

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Player playerUnit;
    Player enemyUnit;

    public Text dialogueText;


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

        int randomIndex = Random.Range(0, enemyPrefab.Length);

        /*
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Player>();
        */


        GameObject enemyGo = Instantiate(enemyPrefab[randomIndex], enemyBattleStation);
        enemyUnit = enemyGo.GetComponent<Player>();


        dialogueText.text = "A test " + enemyUnit.Name + " approaches ";


        hudController.SetPlayerHUD(playerManager);
        hudController.SetEnemyHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();

    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerManager.MechAttackCheck());

        enemyHUD.SetHp(enemyUnit.currHealth);


        yield return new WaitForSeconds(2f);

        enemyUnit.TakeDamage(playerManager.MechAttackCheck());

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
        dialogueText.text = enemyUnit.Name + " Turn ";

        yield return new WaitForSeconds(1f);

        int actionTurn = Random.Range(0, 10);

        bool isDead = false;

        if (actionTurn >= 5)
        {
            Debug.Log("Attacked");

            isDead = playerManager.TakeDamage(enemyUnit.damage);
            hudController.UpdatePlayerHp();

        }
        else
        {
            Debug.Log("Healed");
        }
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

            dialogueText.text = "Player 1 Turn ";
        }

    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action: ";
        playerManager.DrawCards();
    }

    public void OnAttackButton()
    {
        Debug.Log("OnAttackButton");
        if (state != BattleState.PLAYERTURN)
            return;


        StartCoroutine(PlayerAttack());
    }

    void EndBattle()
    {
        if( state == BattleState.WON )
        {
            dialogueText.text = "You won! ";
        }
        if( state == BattleState.LOST )
        {
            dialogueText.text = "You lost! ";
        }
    }
}

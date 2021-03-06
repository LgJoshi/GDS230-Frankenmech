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



    private void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());

        

    }

    IEnumerator SetupBattle()
    {

        int randomIndex = Random.Range(0, enemyPrefab.Length);


        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Player>();


        GameObject enemyGo = Instantiate(enemyPrefab[randomIndex], enemyBattleStation);
        enemyUnit = enemyGo.GetComponent<Player>();


        dialogueText.text = "A test " + enemyUnit.Name + " approaches ";


        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
        playerManager.DrawCards();

    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerManager.MechAttackCheck());

        enemyHUD.SetHp(enemyUnit.currHealth);


        yield return new WaitForSeconds(2f);

        enemyUnit.TakeDamage(playerManager.MechAttackCheck());

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }


        void EndBattle()
        {
            if(state == BattleState.WON)
            {
                dialogueText.text = "You won! ";
            }
            if(state == BattleState.LOST)
            {
                dialogueText.text = "You lost! ";
            }
        }

        IEnumerator EnemyTurn()
        {
            dialogueText.text = enemyUnit.Name + " Turn ";

            yield return new WaitForSeconds(1f);

            int actionTurn = Random.Range(0, 10);



            if(actionTurn >= 5)
            {
                Debug.Log ("Attacked");

                bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
                playerHUD.SetHp(playerUnit.currHealth);

            }
            else
            {
               Debug.Log ("Healed");
                

            }
            yield return new WaitForSeconds(2f);

            //bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

            //playerHUD.SetHp(playerUnit.currHealth);

            //yield return new WaitForSeconds(1f);

            //state = BattleState.PLAYERTURN;

            //dialogueText.text = "Player 1 Turn ";

            if (isDead)
            {
                state = BattleState.LOST;
                EndBattle();
            } else{
                
                state = BattleState.PLAYERTURN;

                EventManager.PlayerTurnFunction();
                playerManager.DrawCards();

                dialogueText.text = "Player 1 Turn ";
            }

        }




    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action: ";
    }




    public void OnAttackButton()
    {
        Debug.Log("OnAttackButton");
        if (state != BattleState.PLAYERTURN)
            return;


        StartCoroutine(PlayerAttack());
    }



}

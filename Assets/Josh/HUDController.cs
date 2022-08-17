using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;
    
    public Text enemyNameText;
    [SerializeField] Image enemyHpBar;
    [SerializeField] TextMeshProUGUI enemyHpText;
    [SerializeField] TextMeshProUGUI enemyIntentText;

    [SerializeField] Image playerHpBar;
    [SerializeField] TextMeshProUGUI playerHpText;

    [SerializeField] TextMeshProUGUI playerEnergyText;
    [SerializeField] Image playerEnergyBar;
    [SerializeField] Text playerBlockText;
    [SerializeField] TextMeshProUGUI deckSizeText;

    PlayerManager playerManager;
    EnemyBehaviour enemyUnit;

    public void SetEnemyHUD( EnemyBehaviour newEnemyUnit )
    {
        enemyUnit = newEnemyUnit;
        enemyNameText.text = enemyUnit.myName;

        UpdateEnemyHp();
    }

    public void UpdateEnemyHp()
    {
        enemyHpBar.fillAmount = (float) enemyUnit.currentHp / enemyUnit.maxHp;
        enemyHpText.text = enemyUnit.currentHp.ToString() +"/"+ enemyUnit.maxHp.ToString();
    }

    public void SetPlayerHUD(PlayerManager newPlayerManager)
    {
        playerManager = newPlayerManager;

        UpdateAllPlayerUI();
    }

    void UpdateAllPlayerUI()
    {
        UpdatePlayerHp();
        UpdatePlayerEnergy();
        UpdatePlayerBlock();
    }

    public void UpdatePlayerHp()
    {
        //playerHpSlider.value = playerManager.playHP;
        playerHpBar.fillAmount = (float) playerManager.playHP / playerManager.maxHp;
        playerHpText.text = playerManager.playHP.ToString() + "/"+ playerManager.maxHp.ToString();
    }

    public void UpdatePlayerEnergy()
    {
        playerEnergyText.text = playerManager.currentEnergy.ToString() + " / " + playerManager.maxEnergy.ToString();
        playerEnergyBar.fillAmount = (float)playerManager.currentEnergy / playerManager.maxEnergy;
    }

    public void UpdatePlayerBlock()
    {
        playerBlockText.text = playerManager.mechBlockTotal.ToString() + " Block";
    }

    public void ChangeDialogueText(string input )
    {
        dialogueText.text = input;
    }

    public void UpdateEnemyIntent()
    {
        enemyIntentText.text = "Next turn: " + enemyUnit.nextAttack + " for " + enemyUnit.DamageCheck() + " damage";
    }

    public void UpdateDeckSize()
    {
        deckSizeText.text = playerManager.playerDeck.Count.ToString();
    }
}

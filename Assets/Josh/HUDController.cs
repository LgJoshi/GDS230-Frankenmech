using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Text enemyNameText;
    [SerializeField] Slider enemyHpSlider;
    [SerializeField] Text enemyHpText;
    [SerializeField] Text energyText;

    [SerializeField] Slider playerHpSlider;
    [SerializeField] Text playerHpDisplay;
    [SerializeField] Text playerEnergyText;
    [SerializeField] Text playerBlockText;

    PlayerManager playerManager;
    EnemyBehaviour enemyUnit;

    public void SetEnemyHUD( EnemyBehaviour newEnemyUnit )
    {
        enemyUnit = newEnemyUnit;
        enemyNameText.text = enemyUnit.myName;
        enemyHpSlider.maxValue = enemyUnit.maxHp;
        
        UpdateEnemyHp();
    }

    public void UpdateEnemyHp()
    {
        enemyHpSlider.value = enemyUnit.currentHp;
        enemyHpText.text = enemyUnit.currentHp.ToString();
    }

    public void SetPlayerHUD(PlayerManager newPlayerManager)
    {
        playerManager = newPlayerManager;
        playerHpSlider.maxValue = playerManager.maxHp;

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
        playerHpSlider.value = playerManager.playHP;
        playerHpDisplay.text = playerManager.playHP.ToString();
    }

    public void UpdatePlayerEnergy()
    {
        playerEnergyText.text = playerManager.currentEnergy.ToString() + " / " + playerManager.maxEnergy.ToString();
    }

    public void UpdatePlayerBlock()
    {
        playerBlockText.text = playerManager.mechBlockTotal.ToString() + " Block";
    }
}

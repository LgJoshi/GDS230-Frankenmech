using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Text enemyNameText;
    [SerializeField] Slider enemyHpSlider;
    [SerializeField] Text enemyHpDisplay;
    [SerializeField] Text energyText;

    [SerializeField] Slider playerHpSlider;
    [SerializeField] Text playerHpDisplay;
    [SerializeField] Text playerEnergyText;

    PlayerManager playerManager;

    public void SetEnemyHUD( Player unit )
    {
        enemyNameText.text = unit.Name;
        enemyHpSlider.maxValue = unit.maxHealth;
        enemyHpSlider.value = unit.currHealth;
        enemyHpDisplay.text = unit.currHealth.ToString();
    }

    public void SetEnemyHp(int hp)
    {
        enemyHpSlider.value = hp;
        enemyHpDisplay.text = hp.ToString();
    }

    public void SetPlayerHUD(PlayerManager newPlayerManager)
    {
        playerManager = newPlayerManager;
        playerHpSlider.maxValue = playerManager.maxHp;
        UpdatePlayerHp();

        UpdatePlayerEnergy();
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
}

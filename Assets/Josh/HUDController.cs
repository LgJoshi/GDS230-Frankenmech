using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Slider hpSlider;
    [SerializeField] Text hpDisplay;
    public Text energyText;



    public void SetHUD( Player unit )
    {
        nameText.text = unit.Name;
        levelText.text = "Lv1 " + unit.Level;
        hpSlider.maxValue = unit.maxHealth;
        hpSlider.value = unit.currHealth;
        hpDisplay.text = unit.currHealth.ToString();
    }

    public void SetHp( int hp )
    {
        hpSlider.value = hp;
        hpDisplay.text = hp.ToString();
    }

    public void SetEnergy( int input )
    {
        energyText.text = input.ToString();
    }
}

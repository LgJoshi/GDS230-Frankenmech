using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Slider hpSlider;
    [SerializeField] Text hpDisplay;



    public void SetHUD(Player unit)
    {
        nameText.text = unit.Name;
        levelText.text = "Lv1 " + unit.Level;
        hpSlider.maxValue = unit.maxHealth;
        hpSlider.value = unit.currHealth;
        hpDisplay.text = unit.currHealth.ToString();
    }

    public void SetHp(int hp)
    {
        hpSlider.value = hp;
        hpDisplay.text = hp.ToString();
    }


}

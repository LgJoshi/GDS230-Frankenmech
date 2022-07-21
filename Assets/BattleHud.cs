using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Slider hpSlider;



    public void SetHUD(Player unit)
    {
        nameText.text = unit.Name;
        levelText.text = "Lv1 " + unit.Level;
        hpSlider.maxValue = unit.maxHealth;
        hpSlider.value = unit.currHealth;

    }

    public void SetHp(int hp)
    {
        hpSlider.value = hp;
    }


}

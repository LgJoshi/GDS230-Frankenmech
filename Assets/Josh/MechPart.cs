using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MechPart : MonoBehaviour
{
    public int damageDealt=5;

    [SerializeField] TextMeshPro myUI;

    private void Start()
    {
        UpdateUI();
    }

    public void CardUsed(string effect, int effectInt)
    {
        switch( effect )
        {
            case "statChange":
                ChangeStat(effectInt);
                break;

            case "statMultiply":
                MultiplyStat(effectInt);
                break;

            default:
                Debug.Log(this.gameObject.name + ": Error");
                break;
        }
        UpdateUI();
    }

    public void ChangeStat( int input )
    {
        damageDealt += input;
    }

    void MultiplyStat(int input)
    {
        damageDealt *= input;
    }

    void UpdateUI()
    {
        myUI.text = damageDealt.ToString();
    }
}

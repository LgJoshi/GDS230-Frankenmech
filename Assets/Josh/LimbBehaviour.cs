using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LimbBehaviour : MonoBehaviour
{
    public int damageDealt=5;

    [SerializeField] TextMeshPro myUI;
    PlayerManager playerManager;

    private void Start()
    {
        playerManager = GetComponentInParent(typeof(PlayerManager)) as PlayerManager;
        playerManager.limbs.Add(this.GetComponent<LimbBehaviour>());
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

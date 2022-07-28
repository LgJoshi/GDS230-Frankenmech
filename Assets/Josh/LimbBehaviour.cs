using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LimbBehaviour : MonoBehaviour
{
    public int effectInt=5;
    public string effectType = "defaultEffect";

    int myId = 0;
    string myName = "default limb";

    [SerializeField] TextMeshPro myUIStat;
    [SerializeField] TextMeshPro myUIName;

    [SerializeField] LimbLibrary limbLibrary;
    PlayerManager playerManager;

    private void OnEnable()
    {
        EventManager.PlayerTurnEvent += GetLimbStats;
    }
    private void OnDisable()
    {
        //might be useless
        EventManager.PlayerTurnEvent -= GetLimbStats;
    }

    private void Start()
    {
        myId = Random.Range(1,4);
        limbLibrary = GetComponentInParent(typeof(LimbLibrary)) as LimbLibrary;
        GetLimbStats();
        
        playerManager = GetComponentInParent(typeof(PlayerManager)) as PlayerManager;
        playerManager.limbs.Add(this.GetComponent<LimbBehaviour>());

        UpdateUI();
    }

    void GetLimbStats()
    {
        effectInt = limbLibrary.limbLibraryArray.limbDataLibrary[myId].limbEffectInt;
        effectType = limbLibrary.limbLibraryArray.limbDataLibrary[myId].limbEffect;

        myName = limbLibrary.limbLibraryArray.limbDataLibrary[myId].limbName;
        myUIName.text = myName;
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
        effectInt += input;
    }

    void MultiplyStat(int input)
    {
        effectInt *= input;
    }

    void UpdateUI()
    {
        myUIStat.text = effectInt.ToString();
    }
}

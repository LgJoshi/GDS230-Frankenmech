using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LimbBehaviour : MonoBehaviour
{
    public int effectInt=5;
    public string effectType = "defaultEffect";

    public int myId = 0;
    [SerializeField] int myLimbNumber;
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

    private void Awake()
    {
        limbLibrary = GetComponentInParent(typeof(LimbLibrary)) as LimbLibrary;
        
        //this is a circular reference but because my grabber requires the energy to be checked, this can't really be helped right now...
        playerManager = GetComponentInParent(typeof(PlayerManager)) as PlayerManager;
        
        //player manager takes care of the following:
        /*
        playerManager = GetComponentInParent(typeof(PlayerManager)) as PlayerManager;
        playerManager.limbs[myLimbNumber] = this.GetComponent<LimbBehaviour>();
        GetLimbStats();
        UpdateUI();
        */
    }

    public void GetLimbStats()
    {
        //player manager sets myId
        //myId = playerManager.limbsId[myLimbNumber];

        effectInt = limbLibrary.limbLibraryArray.limbDataLibrary[myId].limbEffectInt;
        effectType = limbLibrary.limbLibraryArray.limbDataLibrary[myId].limbEffect;

        myName = limbLibrary.limbLibraryArray.limbDataLibrary[myId].limbName;
        myUIName.text = myName;

        UpdateUI();
    }

    public bool CardUsed(string effect, int effectInt, int energyCost)
    {
        if( playerManager.CardEnergyCheck(energyCost) == true )
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

            return true;

        } else
        {
            return false;
        }


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

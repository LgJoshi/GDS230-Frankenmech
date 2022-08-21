using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField] ParticleSystem[] particle;


    public Transform enemyBattleStation;
    public Transform hitEffect;
    [SerializeField] Transform selfEffectLocation;
    [SerializeField] GameObject dodgeEffect;

    public string myName;
    public int myId = 0;
    public int maxHp = 30;
    public int currentHp = 15;
    public int myBlock = 0;
    int myDodge = 0;

    int nextAttackDamage = 0;
    public string nextAttack = "punch";
    public string nextAttackValue = "0";

    int charge = 0;

    EnemyLibrary enemyLibrary;
    public List<EnemyLibrary.AttackData> attackData;

    [SerializeField] GameObject[] enemyModelPrefabs;

    private void OnEnable()
    {
        EventManager.PlayerTurnEvent += ChooseNextAction;
    }

    private void OnDisable()
    {
        EventManager.PlayerTurnEvent -= ChooseNextAction;
    }

    




    void Start(){
        enemyLibrary = GetComponentInParent(typeof(EnemyLibrary)) as EnemyLibrary;
        GetStats();
        currentHp = maxHp;

        Instantiate(enemyModelPrefabs[myId], enemyBattleStation);

    }

    void GetStats(){
        switch( GameObject.FindObjectOfType<SingletonDataStorage>().enemyType )
        {
            case 1:
            myId = Random.Range(1, 3);
            break;

            case 2:
            myId = 3;
            break;

            case 3:
            myId = 4;
            break;

            default:
            myId = 0;
            break;
        }

        myName = enemyLibrary.enemyLibraryArray.enemyDataLibrary[myId].name;
        maxHp = enemyLibrary.enemyLibraryArray.enemyDataLibrary[myId].maxHp;
        myDodge = enemyLibrary.enemyLibraryArray.enemyDataLibrary[myId].dodge;
        Debug.Log("my name is " + enemyLibrary.enemyLibraryArray.enemyDataLibrary[myId].name);
        Debug.Log("enemy attack test " + enemyLibrary.enemyLibraryArray.enemyDataLibrary[myId].attackData[0].name);


        for ( int i = 0; i<enemyLibrary.enemyLibraryArray.enemyDataLibrary[myId].attackData.Count;i++ )
        {
            attackData.Add(enemyLibrary.enemyLibraryArray.enemyDataLibrary[myId].attackData[i]);
        }
    }

    //take damage and return true if dead
    public bool TakeDamage(int input){
        int dodgeRand = Random.Range(0, 101);

        if( dodgeRand > myDodge )
        {
            currentHp -= input;
            if( currentHp <= 0 )
            {
                return true;
            } else
            {
                return false;
            }
        } else
        {
            Debug.Log("DODGED");
            GameObject effect = Instantiate(dodgeEffect);
            effect.transform.position = selfEffectLocation.transform.position;
            return false;
        }


    }

    void ChooseNextAction()
    {
        Debug.Log("Choose next action");

        int choiceMax = 0;
        foreach( EnemyLibrary.AttackData data in attackData )
        {
            choiceMax += data.probability;
            Debug.Log("data.name = "+data.name);
        }

        //random integer
        int choiceNum = 0;
        choiceNum = Random.Range(0, choiceMax);
        Debug.Log("choiceNum: " + choiceNum);
        Debug.Log("choiceMax: " + choiceMax);
        foreach( EnemyLibrary.AttackData data in attackData )
        {
            if( choiceNum >= data.probability )
            {
                Debug.Log("Attack probability: " + data.probability);
                choiceNum -= data.probability;
                Debug.Log("Skip " + data.name);
                Debug.Log("new choiceNum: " + choiceNum);
            } else
            {
                nextAttack = data.name;
                Debug.Log("Chosen " + data.name);
                switch( data.type )
                {
                    case "attack":
                    nextAttackDamage = data.mainValue * data.subValue;
                    nextAttackValue = (data.mainValue + data.buffValue).ToString() + " x " + data.subValue.ToString() + " damage";
                    break;

                    case "buffAttack":
                    nextAttackDamage = (data.mainValue+data.buffValue) * data.subValue;
                    nextAttackValue = (data.mainValue + data.buffValue).ToString() + " x " + data.subValue.ToString() + " damage";
                    break;

                    case "buff":
                    ChangeBuffValue(data);
                    nextAttackDamage = 0;
                    nextAttackValue = "1 increase in damage";
                    break;

                    case "chargeAttack":
                    Debug.Log("chargeAttack");
                    charge += 1;
                    if( charge >= data.chargeValue )
                    {
                        nextAttackDamage = data.mainValue * data.subValue;
                        nextAttackValue = (data.mainValue + data.buffValue).ToString() + " x " + data.subValue.ToString() + " charge expended damage";
                        charge = 0;
                    } else
                    {
                        nextAttack = "Charge ";
                        nextAttackDamage = 0;
                        nextAttackValue = "1 charge";
                    }
                    break;

                    case "block":
                    myBlock = data.mainValue;
                    nextAttackValue = data.mainValue.ToString() + " block";
                    break;

                    case "blockAttack":
                    nextAttackDamage = (data.mainValue + data.buffValue) * data.subValue;
                    nextAttackValue = data.mainValue.ToString() + " x " + data.subValue.ToString() + " damage. Block for " + data.buffValue.ToString();
                    myBlock = data.buffValue;
                    break;

                    default:
                    Debug.Log(this.gameObject.name + ": Error");
                    break;
                }
                
                break;
            }
        }
        
    }

    void ChangeBuffValue( EnemyLibrary.AttackData data )
    {
        data.buffValue += 1;
    }

    public int DamageCheck()
    {
        Instantiate(particle[0], hitEffect);


        return nextAttackDamage;
    }
}

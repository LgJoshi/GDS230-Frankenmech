using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{




    public Transform enemyBattleStation;



    public string myName;
    public int myId = 1;
    public int maxHp = 30;
    public int currentHp = 15;

    int nextAttackDamage = 0;

    public string nextAttack = "punch";

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

        Instantiate(enemyModelPrefabs[4], enemyBattleStation);

    }

    void GetStats(){
        myId = GameObject.FindObjectOfType<SingletonDataStorage>().enemyType;

        myName = enemyLibrary.enemyLibraryArray.enemyDataLibrary[myId].name;
        maxHp = enemyLibrary.enemyLibraryArray.enemyDataLibrary[myId].maxHp;
        Debug.Log("my name is " + enemyLibrary.enemyLibraryArray.enemyDataLibrary[myId].name);
        Debug.Log("enemy attack test " + enemyLibrary.enemyLibraryArray.enemyDataLibrary[myId].attackData[0].name);


        for ( int i = 0; i<enemyLibrary.enemyLibraryArray.enemyDataLibrary[myId].attackData.Count;i++ )
        {
            attackData.Add(enemyLibrary.enemyLibraryArray.enemyDataLibrary[myId].attackData[i]);
        }
    }

    //take damage and return true if dead
    public bool TakeDamage(int input){
        currentHp -= input;
        if (currentHp<=0){
            return true;
        } else{
            return false;
        }
    }

    public void ChooseNextAction()
    {

        int choiceMax = 0;
        foreach( EnemyLibrary.AttackData data in attackData )
        {
            choiceMax += data.mainValue;
            Debug.Log("data.name = "+data.name);
        }

        //random integer
        int choiceNum = 0;
        choiceNum = Random.Range(0, choiceMax);
        
        foreach( EnemyLibrary.AttackData data in attackData )
        {
            if( choiceNum >= data.probability )
            {
                choiceNum -= data.probability;
            } else
            {
                nextAttack = data.name;
                nextAttackDamage = data.mainValue * data.subValue;
                break;
            }
        }
    }

    public int DamageCheck()
    {
        return nextAttackDamage;
    }
}

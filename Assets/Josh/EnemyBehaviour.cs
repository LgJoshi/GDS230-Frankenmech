using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public string myName;
    public int myId = 0;
    public int maxHp = 30;
    public int currentHp = 15;
    EnemyLibrary enemyLibrary;

    void Start(){
        enemyLibrary = GetComponentInParent(typeof(EnemyLibrary)) as EnemyLibrary;
        GetStats();
    }

    void GetStats(){
        myName = enemyLibrary.enemyLibraryArray.enemyDataLibrary[myId].name;
        maxHp = enemyLibrary.enemyLibraryArray.enemyDataLibrary[myId].maxHp;
        Debug.Log("my name is " + enemyLibrary.enemyLibraryArray.enemyDataLibrary[myId].name);
        Debug.Log("enemy attack test " + enemyLibrary.enemyLibraryArray.enemyDataLibrary[myId].attackData[0].name);
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
}

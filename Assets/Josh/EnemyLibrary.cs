using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnemyLibrary : MonoBehaviour
{
    string jsonString;
    public EnemyDataLibrary enemyLibraryArray;

    [System.Serializable]    
    public struct AttackData {
        public string name;
        public string type;
        public int probability;
        public int mainValue;
        public int subValue;
    }
    
    [System.Serializable]
    public class EnemyData {
        public int id = 0;
        public string name = "default enemyName";
        public string description = "default enemyDescription";
        public int maxHp = 99;
        public List<AttackData> attackData;
    }

    [System.Serializable]
    public class EnemyDataLibrary{
        public List<EnemyData> enemyDataLibrary;
    }


    void Awake()
    {
        jsonString = File.ReadAllText( "Assets/Josh/" + "EnemyDataLibrary.json" );
        Debug.Log("jsonstring is: " + jsonString);
        enemyLibraryArray = JsonUtility.FromJson<EnemyDataLibrary>(jsonString);
        //Debug.Log(cardLibraryArray.enemyDataLibrary[1].enemyName);
        //Debug.Log(cardLibraryArray.enemyDataLibrary.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

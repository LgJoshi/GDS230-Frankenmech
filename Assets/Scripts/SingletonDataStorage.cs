using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SingletonDataStorage : MonoBehaviour
{
    private static SingletonDataStorage _instance;
    public static SingletonDataStorage Instance { get { return _instance; } }

    //1 for regular, 2 for elite, 3 for boss
    public int enemyType;

    public int playerHp;
    public int playerMaxHp;

    //deck list int
    public List<int>[] playerDeckLoadoutIds = new List<int>[3];
    //3rd slot is for legs
    public int[] playerLimbLoadoutIds = new int[3];

    public int overworldCheckpoint = 0;

    string jsonString;
    SingletonDataClass singletonDataArray;

    [System.Serializable]
    public class SingletonData
    {
        public int playerMaxHp = 69;
        public List<int> limbIds = new List<int>();
        public List<LimbDataClass> limbDecks = new List<LimbDataClass>();
    }

    [System.Serializable]
    public class LimbDataClass
    {
        public List<int> limbCards = new List<int>();
    }

    [System.Serializable]
    public class SingletonDataClass
    {
        public SingletonData singletonDataClass;
    }

    private void Awake()
    {
        //singleton check
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }

        jsonString = File.ReadAllText("Assets/Scripts/JSON/" + "SingletonData.json");
        
        Debug.Log("SingletonData jsonstring is: " + jsonString);
        singletonDataArray = JsonUtility.FromJson<SingletonDataClass>(jsonString);

        /*
        playerLimbLoadoutIds = new int[3];

        playerDeckLoadoutIds[0] = new List<int>();
        playerDeckLoadoutIds[1] = new List<int>();
        playerDeckLoadoutIds[2] = new List<int>();

        playerDeckLoadoutIds[0].Add(1);
        playerDeckLoadoutIds[0].Add(2);
        playerDeckLoadoutIds[0].Add(1);

        playerDeckLoadoutIds[1].Add(1);
        playerDeckLoadoutIds[1].Add(2);
        playerDeckLoadoutIds[1].Add(1);

        playerDeckLoadoutIds[2].Add(1);
        playerDeckLoadoutIds[2].Add(4);
        playerDeckLoadoutIds[2].Add(1);
        string testJson = JsonUtility.ToJson(overworldCheckpoint);
        Debug.Log("test json: " + testJson);
        */
        InitializeLoadout();
    }

    public void InitializeLoadout()
    {
        playerLimbLoadoutIds = new int[3];

        playerDeckLoadoutIds[0] = new List<int>();
        playerDeckLoadoutIds[1] = new List<int>();
        playerDeckLoadoutIds[2] = new List<int>();
        Debug.Log("SOAOIGNEOANA: " + singletonDataArray.singletonDataClass.limbIds[1]);
        Debug.Log("SOAOIGNEOANA: " + singletonDataArray.singletonDataClass.limbDecks[0].limbCards[0]);


        for( int i = 0;i < singletonDataArray.singletonDataClass.limbIds.Count;i++ )
        {
            playerLimbLoadoutIds[i] = singletonDataArray.singletonDataClass.limbIds[i];

        }

        //get card ids and add them
        for( int i = 0;i < singletonDataArray.singletonDataClass.limbDecks.Count;i++ )
        {
            foreach( var cardID in singletonDataArray.singletonDataClass.limbDecks[i].limbCards )
            {
                playerDeckLoadoutIds[i].Add(cardID);
            }
        }


        playerMaxHp = singletonDataArray.singletonDataClass.playerMaxHp;
        playerHp = playerMaxHp;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonDataStorage : MonoBehaviour
{
    private static SingletonDataStorage _instance;
    public static SingletonDataStorage Instance { get { return _instance; } }

    //1 for regular, 2 for elite, 3 for boss
    public int enemyType;

    public List<int>[] playerDeckLoadoutIds = new List<int>[3];
    //3rd slot is for legs
    public int[] playerLimbLoadoutIds;

    public int overworldCheckpoint = 0;

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

        playerLimbLoadoutIds = new int[3];

        playerDeckLoadoutIds[0] = new List<int>();
        playerDeckLoadoutIds[1] = new List<int>();
        playerDeckLoadoutIds[2] = new List<int>();

        InitializeLoadout();
    }

    public void InitializeLoadout()
    {
        playerLimbLoadoutIds[0] = 1;
        playerLimbLoadoutIds[1] = 2;
        playerLimbLoadoutIds[2] = 11;

        playerDeckLoadoutIds[0].Add(1);
        playerDeckLoadoutIds[0].Add(2);
        playerDeckLoadoutIds[0].Add(1);

        playerDeckLoadoutIds[1].Add(1);
        playerDeckLoadoutIds[1].Add(2);
        playerDeckLoadoutIds[1].Add(1);

        playerDeckLoadoutIds[2].Add(1);
        playerDeckLoadoutIds[2].Add(4);
        playerDeckLoadoutIds[2].Add(1);
    }

    public void ChangeLimb(int limbInt, int newLimbId, List<int> newCardIds )
    {

    }

}

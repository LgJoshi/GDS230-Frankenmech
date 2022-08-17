using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LootUI : MonoBehaviour
{
    SingletonDataStorage singletonDataStorage;
    BattleSystem battleSystem;


    // Start is called before the first frame update
    void Start()
    {
        singletonDataStorage = GameObject.FindObjectOfType<SingletonDataStorage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Return()
    {
        SceneManager.LoadScene("RoomGeneration");
        battleSystem.LootMenuUI.SetActive(false);
    }

    public void Take()
    {
        singletonDataStorage.playerLimbLoadoutIds[0] = 4;

        SceneManager.LoadScene("RoomGeneration");

    }


}

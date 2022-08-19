using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LootUI : MonoBehaviour
{
    SingletonDataStorage singletonDataStorage;
    BattleSystem battleSystem;

    int lootId = 0;
    int replacedLimb = 0;
    [SerializeField] Image[] limbImages;

    // Start is called before the first frame update
    void Start()
    {
        singletonDataStorage = GameObject.FindObjectOfType<SingletonDataStorage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLootId(int input )
    {
        lootId = input;
    }

    public void Return()
    {
        SceneManager.LoadScene("RoomGeneration");
    }

    public void SelectLoot(int input )
    {
        foreach( var limb in limbImages )
        {
            limb.color = new Color32(100, 100, 100,255);
        }
        limbImages[input].color = new Color32(50, 50, 50, 255);
    }

    public void Take()
    {
        singletonDataStorage.playerLimbLoadoutIds[replacedLimb] = lootId;

        SceneManager.LoadScene("RoomGeneration");

    }


}

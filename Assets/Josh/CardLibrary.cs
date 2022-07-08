using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CardLibrary : MonoBehaviour
{
    string jsonString;
    public CardDataLibrary cardLibraryArray;

    [System.Serializable]
    public class CardData {
        public string cardName = "default cardName";
        public string cardDescription = "default cardDescription";
        public int cardCost = 99;
        public int cardID = 999;
    }

    [System.Serializable]
    public class CardDataLibrary{
        public List<CardData> cardDataLibrary;
    }


    void Awake()
    {
        jsonString = File.ReadAllText( "Assets/Josh/" + "CardDataLibrary.json" );
        Debug.Log("jsonstring is: " + jsonString);
        cardLibraryArray = JsonUtility.FromJson<CardDataLibrary>(jsonString);
        Debug.Log(cardLibraryArray.cardDataLibrary[1].cardName);
        //Debug.Log(cardLibraryArray.cardDataLibrary.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

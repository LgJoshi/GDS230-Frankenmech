using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CardLibrary : MonoBehaviour
{
    string jsonString;
    CardDataLibrary cardLibraryArray;


    public class CardData {
        public string cardName = "default cardName";
        public string cardDescription = "default cardDescription";
        public int cardCost = 99;
    }

    public class CardDataLibrary{
        public List<CardData> cardDataLibrary;
    }


    void Start()
    {
        jsonString = File.ReadAllText( "Assets/Josh/" + "CardDataLibrary.json" );
        Debug.Log("jsonstring is: " + jsonString);
        cardLibraryArray = JsonUtility.FromJson<CardDataLibrary>(jsonString);
        Debug.Log(cardLibraryArray.cardDataLibrary[0]);
        //Debug.Log(cardLibraryArray.cardDataLibrary.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

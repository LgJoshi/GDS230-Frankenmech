using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshPro cardTextName;
    [SerializeField] TextMeshPro cardTextDesc;

    CardLibrary cardLibrary;

    int myCardId=2;
    string myCardName="myCardName";
    string myCardDesc="myCardDesc";


    void Start()
    {
        //Generate random card id
        cardLibrary = FindObjectOfType<CardLibrary>();
        myCardName = cardLibrary.cardLibraryArray.cardDataLibrary[myCardId].cardName;
        myCardDesc = cardLibrary.cardLibraryArray.cardDataLibrary[myCardId].cardDescription;
        UpdateCardFace();
    }

    void UpdateCardFace(){
        UpdateName();
        UpdateDesc();
    }

    void UpdateName(){
        cardTextName.text = myCardName;
    }
    void UpdateDesc(){
        cardTextDesc.text = myCardDesc;
    }
    void UpdateCost(){

    }
}

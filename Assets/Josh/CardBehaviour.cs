using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshPro cardTextName;
    [SerializeField] TextMeshPro cardTextDesc;

    int myCardId=0;
    string myCardName="myCardName";
    string myCardDesc="myCardDesc";


    void Start()
    {
        //Generate random card id
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

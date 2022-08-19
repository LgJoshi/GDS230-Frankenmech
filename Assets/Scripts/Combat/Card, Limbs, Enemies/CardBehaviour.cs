using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class CardBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshPro cardTextName;
    [SerializeField] TextMeshPro cardTextDesc;
    [SerializeField] TextMeshPro cardTextCost;

    public CardLibrary cardLibrary;

    //card's position in the hand int list
    public int myHandId = 0;

    int myCardId=0;
    string myCardName="myCardName";
    string myCardDesc="myCardDesc";
    public string myEffect="myEffect";
    public int myEffectInt = 0;
    public int myEnergyCost = 0;

    [SerializeField] SpriteRenderer myCardArt;
    [SerializeField] Sprite[] cardArtArray;

    private void OnEnable()
    {
        EventManager.CardDrawEvent += UpdateCardData;
    }
    private void OnDisable()
    {
        //might be useless
        EventManager.CardDrawEvent -= UpdateCardData;
    }
    

    void Start()
    {
        //Generate random card id
        //UpdateCardFace();
        //Debug.Log(this.name + " instantiated");
    }

    public void GetNewId(int newId )
    {
        myCardId = newId;
        UpdateCardData();
    }

    void UpdateCardData(){
        myCardName = cardLibrary.cardLibraryArray.cardDataLibrary[myCardId].cardName;
        myCardDesc = cardLibrary.cardLibraryArray.cardDataLibrary[myCardId].cardDescription;
        myEffect = cardLibrary.cardLibraryArray.cardDataLibrary[myCardId].cardEffect;
        myEffectInt = cardLibrary.cardLibraryArray.cardDataLibrary[myCardId].cardEffectInt;
        myEnergyCost = cardLibrary.cardLibraryArray.cardDataLibrary[myCardId].cardCost;

        UpdateName();
        UpdateDesc();
        UpdateCost();
        UpdateArt();
    }

    void UpdateName(){
        cardTextName.text = myCardName;
    }

    void UpdateDesc(){
        cardTextDesc.text = myCardDesc;
    }

    void UpdateCost(){
        cardTextCost.text = myEnergyCost.ToString();
    }

    void UpdateArt()
    {
        myCardArt.sprite = cardArtArray[myCardId];
    }

    
}

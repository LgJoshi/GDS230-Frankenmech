using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class CardBehaviour : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
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

    public void OnBeginDrag(PointerEventData eventData){
        Debug.Log("start dragging");
    }

    public void OnDrag(PointerEventData eventData){
        Debug.Log("dragging");
    }

    public void OnEndDrag(PointerEventData eventData){
        Debug.Log("end dragging");
    }
}

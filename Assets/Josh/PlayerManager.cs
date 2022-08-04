using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public int playHP = 50;
    int maxHp = 50;
    [SerializeField] TextMeshProUGUI uiPlayerHP;
    
    int cardDrawPower = 3;
    int maxEnergy = 3;
    int currentEnergy = 3;

    [SerializeField] GameObject cardPrefab;
    [SerializeField] CardLibrary cardLibrary;
    SingletonDataStorage singletonDataStorage;

    [SerializeField] List<int> playerDeck;
    [SerializeField] List<int> playerHand;
    [SerializeField] List<int> playerDiscard;

    [SerializeField] LimbBehaviour[] limbs;
    int[] limbsId;

    [SerializeField] List<GameObject> spawnedCards;
    [SerializeField] Transform handStart;

    private void OnEnable()
    {
        EventManager.CardPlayedEvent += UpdateHand;
    }
    private void OnDisable()
    {
        EventManager.CardPlayedEvent -= UpdateHand;
    }

    void Start() {

        uiPlayerHP.text = playHP.ToString();

        singletonDataStorage = GameObject.FindObjectOfType<SingletonDataStorage>();

        limbsId = singletonDataStorage.playerLimbLoadoutIds;

        InitializeLoadout();

    }

    private void Update()
    {
        /*if( Input.GetKeyDown("f") )
        {
            DrawCards();
        }*/
    }

    void InitializeLoadout()
    {

        for (int i = 0; i < limbs.Length; i++)
        {
            limbs[i].myId = singletonDataStorage.playerLimbLoadoutIds[i];
            limbs[i].GetLimbStats();
        }

        foreach (var listObj in singletonDataStorage.playerDeckLoadoutIds)
        {
            foreach (var iD in listObj)
            {
                playerDeck.Add(iD);
            }
        }
    }

    public void DrawCards()
    {
        //discards the hand to the discard pile
        for( int i = spawnedCards.Count;i > 0;i-- )
        {
            //delete card objects
            GameObject.Destroy(spawnedCards[i-1]);
            spawnedCards.RemoveAt(i - 1);
            
            //move card from hand to discard
            playerDiscard.Add(playerHand[i - 1]);
            playerHand.RemoveAt(i - 1);
        }

        //instantiate new cards which make up the hand
        for (int i=0; i<cardDrawPower; i++ )
        {
            GameObject newCard = Instantiate(cardPrefab);
            //passes reference of card library
            newCard.GetComponent<CardBehaviour>().cardLibrary = cardLibrary;
            newCard.GetComponent<CardBehaviour>().myHandId = i;
            //sets the card's name so that I can delete it from the list easily
            newCard.name = i.ToString();

            //set positions
            newCard.transform.parent = this.transform;
            newCard.transform.position = handStart.position;
            spawnedCards.Add(newCard);
            newCard.transform.position += new Vector3(i*1.5f, 0, 0);

        }

        foreach(var handCard in spawnedCards )
        {
            CardBehaviour myCardBehaviour = handCard.GetComponent<CardBehaviour>();
            myCardBehaviour.GetNewId(playerDeck[0]);

            //update int lists
            playerHand.Add(playerDeck[0]);
            playerDeck.RemoveAt(0);

            if( playerDeck.Count == 0 )
            {
                //moves cards from discard to deck
                Debug.Log("moving cards from discard to deck");
                for (int i=playerDiscard.Count; i>0; i-- )
                {
                    Debug.Log("discard->deck");
                    playerDeck.Add(playerDiscard[0]);
                    playerDiscard.RemoveAt(0);
                }
                ShuffleDeck();
            }
        }

        EventManager.CardDrawFunction();
    }

    void UpdateHand(int input)
    {
        //delete card objects
        GameObject.Destroy(spawnedCards[input]);
        spawnedCards.RemoveAt(input);

        //update card IDs
        for( int i = spawnedCards.Count;i > 0;i-- )
        {
            spawnedCards[i-1].GetComponent<CardBehaviour>().myHandId = i - 1;
        }

        //move card from hand to discard
        playerDiscard.Add(playerHand[input]);
        playerHand.RemoveAt(input);
    }

    //this can be made into a static class using this method: https://stackoverflow.com/questions/273313/randomize-a-listt
    void ShuffleDeck()
    {

        for ( int i = playerDeck.Count; i > 0; i-- )
        {
            int k = Random.Range(0, i);
            int value = playerDeck[k];
            playerDeck[k] = playerDeck[i-1];
            playerDeck[i-1] = value;
        }
        Debug.Log("shuffle deck");
    }

    public int MechAttackCheck(){
        int mechAttackTotal=0;

        for( int i = 0;i < limbs.Length;i++ )
        {
            if (limbs[i].effectType == "attack"){
                mechAttackTotal += limbs[i].effectInt;
            }
        }

        return mechAttackTotal;
    }

    public bool TakeDamage(int input){

        int mechBlockTotal = 0;

        for( int i = 0;i < limbs.Length;i++ )
        {
            if (limbs[i].effectType == "block"){
                mechBlockTotal += limbs[i].effectInt;
            }
        }

        playHP += Mathf.Clamp(input+mechBlockTotal, -999999, 0);
        uiPlayerHP.text = Mathf.Clamp(playHP, 0, 999999).ToString();
        if( playHP <= 0 )
        {
            return true;
        } else
        {
            return false;
        }
    }

    public bool CardEnergyCheck(int input)
    {
        if( currentEnergy - input < 0 )
        {
            return false;
        } else
        {
            currentEnergy -= input;
            return true;
        }
    }
}

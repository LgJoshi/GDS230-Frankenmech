using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int playHP = 50;
    int cardDrawPower = 3;

    [SerializeField] GameObject cardPrefab;
    [SerializeField] CardLibrary cardLibrary;

    [SerializeField] List<int> playerDeck;
    [SerializeField] List<int> playerHand;
    [SerializeField] List<int> playerDiscard;

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
        playerDeck.Add(1);
        playerDeck.Add(2);
        playerDeck.Add(3);
        playerDeck.Add(1);
        playerDeck.Add(2);
        playerDeck.Add(3);
        playerDeck.Add(1);
        playerDeck.Add(2);
    }

    private void Update()
    {
        if( Input.GetKeyDown("f") )
        {
            DrawCards();
            EventManager.CardDrawFunction();
        }
    }

    void DrawCards()
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
}

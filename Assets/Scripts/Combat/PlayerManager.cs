using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public int playHP = 69;
    public int maxHp = 50;
    
    int cardDrawPower = 3;
    public int maxEnergy = 3;
    public int currentEnergy = 3;

    public int mechBlockTotal = 0;
    int dodgeTotal = 0;

    [SerializeField] GameObject cardPrefab;
    [SerializeField] CardLibrary cardLibrary;
    SingletonDataStorage singletonDataStorage;

    [SerializeField] HUDController hudController;

    [SerializeField] public List<int> playerDeck;
    [SerializeField] List<int> playerHand;
    [SerializeField] List<int> playerDiscard;

    public LimbBehaviour[] limbs;
    public int[] limbsId;

    [SerializeField] List<GameObject> spawnedCards;
    [SerializeField] Transform handStart;

    private void OnEnable()
    {
        EventManager.CardPlayedEvent += UpdateHand;
        EventManager.PlayerTurnEvent += DrawCards;
        EventManager.PlayerTurnEvent += RefreshEnergy;
        EventManager.PlayerTurnEvent += RefreshBlock;
    }
    private void OnDisable()
    {
        EventManager.CardPlayedEvent -= UpdateHand;
        EventManager.PlayerTurnEvent -= DrawCards;
        EventManager.PlayerTurnEvent -= RefreshEnergy;
        EventManager.PlayerTurnEvent -= RefreshBlock;
    }

    void Start() {

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

        currentEnergy = maxEnergy;
        playHP = singletonDataStorage.playerHp;
    }

    void DrawCards()
    {
        //discards the hand to the discard pile
        for (int i = spawnedCards.Count; i > 0; i--)
        {
            //delete card objects
            GameObject.Destroy(spawnedCards[i - 1]);
            spawnedCards.RemoveAt(i - 1);

            //move card from hand to discard
            if (playerHand.Count != 0)
            {
                playerDiscard.Add(playerHand[i - 1]);
                playerHand.RemoveAt(i - 1);
            }
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
            newCard.transform.rotation = handStart.rotation;
            spawnedCards.Add(newCard);
            newCard.transform.position += new Vector3(i*2f, 0, 0);

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
        hudController.UpdateDeckSize();
    }

    void RefreshEnergy()
    {
        currentEnergy = maxEnergy;
        hudController.UpdatePlayerEnergy();
    }

    void RefreshBlock()
    {
        mechBlockTotal = 0;
        hudController.UpdatePlayerBlock();
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
                mechAttackTotal += (limbs[i].effectIntOne * limbs[i].effectIntTwo);
            }
        }

        return mechAttackTotal;
    }

    public void UpdateBlockDodge()
    {
        //check each limb for its type and add to the stat totals
        for( int i = 0;i < limbs.Length;i++ )
        {
            //turn this into a switch statement if it becomes too unwieldy
            if( limbs[i].effectType == "dodge" )
            {
                dodgeTotal += limbs[i].effectIntOne;
            } else if( limbs[i].effectType == "block" )
            {
                mechBlockTotal += limbs[i].effectIntOne;
            }
        }

        hudController.UpdatePlayerBlock();
    }

    public bool TakeDamage(int input){

        //random dodge number
        float dodgeRand = Random.Range(0, 100);
        if (dodgeRand > dodgeTotal )
        {

            Debug.Log("Took " + Mathf.Clamp(input-mechBlockTotal, 0, 99999) + " damage");
            playHP -= Mathf.Clamp(input-mechBlockTotal, 0, 99999);

            mechBlockTotal = Mathf.Clamp(mechBlockTotal - input, 0, 99999);
            hudController.UpdatePlayerBlock();

        } else
        {
            Debug.Log("Dodged!");
        }

        dodgeTotal = 0;

        hudController.UpdatePlayerHp();
        
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
            hudController.UpdatePlayerEnergy();
            return true;
        }
    }

    public void ChangeBlock(int input )
    {
        mechBlockTotal += input;
        hudController.UpdatePlayerBlock();
    }

    //activates the loot script which will spawn buttons. these buttons will activate a public function on this playermanager script which updates the singleton database.
    public void ShowLoot()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHandController : MonoBehaviour {

    public PlayerController player;

    public Card[] hand;

    public Transform canvas;

    public bool cardInPlay;

    public int remainingPlays = 1;

    public bool myTurn = true;

    public Text moves;

    bool hasPlayedThisTurn;

    private void Start()
    {
        hand = new Card[5];

        for (int i = 0; i < hand.Length; i++)
        {
            GameObject nCard = Instantiate(Resources.Load("Card"), Vector3.zero, Quaternion.identity, canvas) as GameObject;
            nCard.GetComponent<RectTransform>().anchoredPosition = new Vector3(860 - (i * 225), -390);
            hand[i] = nCard.GetComponent<Card>();
            hand[i].type = Random.Range(0, 5);
            hand[i].id = i;
        }
    }

    public void replaceCard(int id){

        bool giveMovement = true;

        for (int i = 0; i < hand.Length; i++){
            if(hand[i] != null)
            {
                if(hand[i].type == 0 || hand[i].type == 1 && i != id)
                {
                    giveMovement = false;

                }
            }
        }

        if (!giveMovement)
        {
            GameObject nCard = Instantiate(Resources.Load("Card"), Vector3.zero, Quaternion.identity, canvas) as GameObject;
            nCard.GetComponent<RectTransform>().anchoredPosition = new Vector3(860 - (id * 225), -390);
            hand[id] = nCard.GetComponent<Card>();
            hand[id].type = Random.Range(0, 5);
            hand[id].id = id;
        }
        else
        {
            GameObject nCard = Instantiate(Resources.Load("Card"), Vector3.zero, Quaternion.identity, canvas) as GameObject;
            nCard.GetComponent<RectTransform>().anchoredPosition = new Vector3(860 - (id * 225), -390);
            hand[id] = nCard.GetComponent<Card>();
            hand[id].type = 0;
            hand[id].id = id;
        }
    }

    public void newTurn(){
        remainingPlays = 1;
        myTurn = true;
        hasPlayedThisTurn = false;
    }

    private void Update()
    {
        moves.text = "Moves: " + remainingPlays;


        if (remainingPlays == 0)
        {
            player.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            myTurn = false;
        }

    }

    public void runTurn(int cardID){

        switch (hand[cardID].type)
        {
            case 0:
                // Move 1 Card
                player.move(1);
                break;
            case 1:
                // Move 2 Card
                player.move(2);
                break;
            case 2:
                // Jump 1 Card
                if(!hasPlayedThisTurn)
                {
                    remainingPlays++;
                    player.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                }
                player.jump(1);
                break;
            case 3:
                // Jump 2 Card
                if (!hasPlayedThisTurn)
                {
                    remainingPlays++;
                    player.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                }
                player.jump(2);
                break;
            case 4:
                // Attack 1 Card
                player.attack(1);
                break;
            default:
                break;
        }

        hasPlayedThisTurn = true;

        remainingPlays--;

        Destroy(hand[cardID].gameObject);
        replaceCard(cardID);   
    }
}

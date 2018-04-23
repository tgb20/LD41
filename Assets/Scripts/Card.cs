using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerClickHandler {

    public int type;
    public int id;

    public Sprite[] cardTypes;

    public bool highLighted;

    PlayerController player;


    PlayerHandController hand;

    Text noticeText;



    private void Start()
    {

        hand = GameObject.FindWithTag("Hand").GetComponent<PlayerHandController>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        noticeText = GameObject.FindWithTag("NoticeText").GetComponent<Text>();

        gameObject.GetComponent<Image>().sprite = cardTypes[type];
    }

    public void discardCard(){
        if (hand.myTurn && !highLighted)
        {
            Destroy(hand.hand[id].gameObject);
            hand.replaceCard(id);
            hand.remainingPlays = 0;
        }
        else
        {
            print("Can't Do That");
            noticeText.enabled = true;
            Invoke("hideNotice", 3);
        }
    }

    void hideNotice()
    {
        noticeText.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        bool okay = false;

        if (hand.myTurn)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (!highLighted)
                {
                    if (!hand.cardInPlay)
                    {
                        // Check if Card is Playable

                        // Move 1
                        if (type == 0)
                        {
                            if (player.CheckForMove(1))
                            {
                                okay = true;
                            }
                        }

                        // Move 2
                        if (type == 1)
                        {
                            if (player.CheckForMove(2))
                            {
                                okay = true;
                            }
                        }

                        // Jump
                        if (type == 2 || type == 3)
                        {
                            okay = true;
                        }

                        // Attack 1
                        if (type == 4)
                        {
                            if(!player.CheckForAttack(1)){
                                okay = true;
                            }
                        }

                        if (okay)
                        {
                            gameObject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                            gameObject.GetComponent<RectTransform>().localScale += new Vector3(0.5f, 0.5f);
                            highLighted = true;
                            hand.cardInPlay = true;
                        }
                        else
                        {
                            print("Can't Do That");
                            noticeText.enabled = true;
                            Invoke("hideNotice", 3);
                        }
                    }
                }
                else
                {
                    hand.cardInPlay = false;
                    hand.runTurn(id);
                }
            }
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (highLighted)
                {
                    gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(860 - (id * 225), -390);
                    gameObject.GetComponent<RectTransform>().localScale -= new Vector3(0.5f, 0.5f);
                    highLighted = false;
                    hand.cardInPlay = false;
                }
            }
        }
    }

}

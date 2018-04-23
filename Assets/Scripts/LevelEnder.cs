using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnder : MonoBehaviour {

    GameController controller;

    private void Start()
    {
        controller = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            controller.newLevel();
        }
    }
}

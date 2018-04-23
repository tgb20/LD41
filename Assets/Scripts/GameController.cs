using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{



    public GameObject mainMenu;

    public GameObject game;

    public GameObject levelSelect;

    public GameObject gameOver;


    public Sprite regMusic;
    public Sprite offMusic;

    public Button musicButton;



    public int score;

    public PlayerHandController playerHand;

    public Image[] hearts;


    public PlayerController player;

    public Texture2D[] levels;

    public LevelGenerator levGen;

    int curLevel = 0;

    void Update()
    {
        if (!playerHand.myTurn)
        {

            Invoke("enemyMove", 1);
        }


        int playerHealth = player.gameObject.GetComponent<Health>().health;

        if (playerHealth == 0){
            hearts[0].enabled = false;
            hearts[1].enabled = false;
            hearts[2].enabled = false;

            game.SetActive(false);
            gameOver.SetActive(true);

        }
        if (playerHealth == 1)
        {
            hearts[0].enabled = true;
            hearts[1].enabled = false;
            hearts[2].enabled = false;
        }
        if (playerHealth == 2)
        {
            hearts[0].enabled = true;
            hearts[1].enabled = true;
            hearts[2].enabled = false;
        }
        if (playerHealth == 3)
        {
            hearts[0].enabled = true;
            hearts[1].enabled = true;
            hearts[2].enabled = true;
        }


    }

    public void toggleMusic(){
        if(Camera.main.gameObject.GetComponent<AudioSource>().isPlaying)
        {
            Camera.main.gameObject.GetComponent<AudioSource>().Pause();
            musicButton.image.sprite = offMusic;
        }
        else
        {
            Camera.main.gameObject.GetComponent<AudioSource>().UnPause(); 
            musicButton.image.sprite = regMusic;
        }
    }

    public void loadGame()
    {
        mainMenu.SetActive(false);
        game.SetActive(true);
        newLevel();
    }

    public void quitGame()
    {
        mainMenu.SetActive(true);
        game.SetActive(false);
        player.transform.position = new Vector3(1, 3, 0);
        curLevel = 0;
    }

    public void backFromSelect()
    {
        mainMenu.SetActive(true);
        levelSelect.SetActive(false); 
    }

    public void backFromGameOver()
    {
        mainMenu.SetActive(true);
        player.gameObject.GetComponent<Health>().health = 3;
        player.transform.position = new Vector3(1, 3, 0);
        gameOver.SetActive(false);
        curLevel = 0;
    }

    public void openLevelSelect()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(true); 
    }

    public void selectLevel(int lv)
    {
        curLevel = lv;
        levelSelect.SetActive(false);
        game.SetActive(true);
        newLevel();
    }

    public void quitProgram()
    {
        print("Bye Bye!");
        Application.Quit();
    }


    void enemyMove(){

        if (!playerHand.myTurn)
        {

            if (player.FindNearestEnemy() != null)
            {
                EnemyController en = player.FindNearestEnemy();

                en.move(1);

                playerHand.newTurn();
            }
            else
            {
                playerHand.newTurn();
            }
        }
    }

    public void endTurn(){
        playerHand.remainingPlays = 0;
    }

    public void newLevel(){

        score = 0;
        player.GetComponent<Health>().health = 3;

        player.transform.position = new Vector3(1, 3);

        if(curLevel > levels.Length - 1)
        {
            quitGame();
        }

        if(levels[curLevel] != null){
            levGen.level = levels[curLevel];
            levGen.generateLevel();
        }
        curLevel += 1;
    }

}

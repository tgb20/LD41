using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public CollisionChecker[] frontCheckers;

    public void move(int amount)
    {

        Vector3 curPos = transform.position;
        Vector3 newPos;


        newPos = new Vector3(curPos.x + amount, curPos.y);

        transform.position = newPos;

    }

    public void jump(int height)
    {
        Vector3 curPos = transform.position;

        Vector3 newPos;

        newPos = new Vector3(curPos.x, curPos.y + height);

        transform.position = newPos;
    }

    public void attack(int distance)
    {

        EnemyController enemy = FindNearestEnemy();

        enemy.gameObject.GetComponent<Health>().health--;

    }

    public bool CheckForMove(int distance){


        for (int i = 0; i < distance; i++)
        {

            if(frontCheckers[i].touchingWall){
                return false;
            }
        }
        return true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "EnemyHead")
        {
            collision.gameObject.transform.parent.gameObject.GetComponent<Health>().health--;
        }
    }

    public bool CheckForAttack(int distance){
        for (int i = 0; i < distance; i++)
        {

            if (frontCheckers[i].touchingEnemy)
            {
                return false;
            }
        }
        return true;
    }

    public EnemyController FindNearestEnemy()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length != 0)
        {

            int c = 0;

            float curDis = 1000;


            for (int i = 0; i < enemies.Length; i++)
            {


                float playerX = gameObject.transform.position.x;
                float playerY = gameObject.transform.position.y;

                float enemyX = enemies[i].transform.position.x;
                float enemyY = enemies[i].transform.position.y;

                float distance = Mathf.Sqrt((Mathf.Pow((playerX - enemyX), 2)) + (Mathf.Pow((playerY - enemyY), 2)));


                if (distance < curDis)
                {
                    curDis = distance;
                    c = i;
                }

            }



            return enemies[c].GetComponent<EnemyController>();
        }
        else
        {
            return null;
        }

    }
}

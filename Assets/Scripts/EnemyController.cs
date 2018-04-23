using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public CollisionChecker[] frontCheckers;

    public void move(int amount)
    {

        if(CheckForMove(amount))
        {
            Vector3 curPos = transform.position;
            Vector3 newPos;

            newPos = new Vector3(curPos.x - amount, curPos.y);

            transform.position = newPos;
        }

    }

    public void jump(int height)
    {
        Vector3 curPos = transform.position;

        Vector3 newPos;

        newPos = new Vector3(curPos.x, curPos.y + height);

        transform.position = newPos;
    }


    public bool CheckForMove(int distance)
    {


        for (int i = 0; i < distance; i++)
        {

            if (frontCheckers[i].touchingWall)
            {
                return false;
            }
        }
        return true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            float eY = transform.position.y;
            float pY = collision.transform.position.y;

            if ((pY - eY) > 0.4)
            {

                gameObject.GetComponent<Health>().health--;

            }
            else
            {
                GameObject player = collision.gameObject;

                player.GetComponent<Health>().health--;

                gameObject.SetActive(false);
            }

        }
    }

}

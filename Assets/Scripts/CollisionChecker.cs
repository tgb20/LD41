using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour {

    public bool touchingEnemy;
    public bool touchingWall;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            touchingWall = true;
        }

        if (other.gameObject.tag == "Enemy")
        {
            touchingEnemy = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            touchingWall = false;
        }

        if (other.gameObject.tag == "Enemy")
        {
            touchingEnemy = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public Texture2D level;


    public void generateLevel()
    {

        for (int i = 0; i < transform.childCount; i++){
            Destroy(transform.GetChild(i).gameObject);
        }


        for (int x = 0; x < level.width; x++)
        {
            for (int y = 0; y < level.height; y++)
            {
                generateTile(x, y);
            }
        }

    }

    void generateTile(int x, int y)
    {
        Color pixelColor = level.GetPixel(x, y);

        if(pixelColor.a == 0)
        {
            return;
        }

        if(pixelColor == Color.green)
        {
            Vector2 pos = new Vector2(x, y);
            Instantiate(Resources.Load("Ground1Tile"), pos, Quaternion.identity, gameObject.transform);
        }
        if (pixelColor == new Color(0, 1, 1))
        {
            Vector2 pos = new Vector2(x, y);
            Instantiate(Resources.Load("Ground2Tile"), pos, Quaternion.identity, gameObject.transform);
        }
    
        if(pixelColor == Color.red)
        {
            Vector2 pos = new Vector2(x, y);
            Instantiate(Resources.Load("Enemy"), pos, Quaternion.identity, gameObject.transform);
        }

        if(pixelColor == new Color(1, 1, 0))
        {
            Vector2 pos = new Vector2(x, y);
            Instantiate(Resources.Load("Coin"), pos, Quaternion.identity, gameObject.transform);
        }

        if(pixelColor == Color.blue)
        {
            Vector2 pos = new Vector2(x, y);
            Instantiate(Resources.Load("LevelEnder"), pos, Quaternion.identity, gameObject.transform);
        }


    }





}

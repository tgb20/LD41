using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player;

	
	// Update is called once per frame
	void Update () 
    {
        if (player != null)
        {
            Vector3 newPos = new Vector3(player.transform.position.x + 8, 2.3f, -10);
            Vector3 oldPos = transform.position;

            transform.position = Vector3.Lerp(oldPos, newPos, Time.deltaTime * 3f);
        }
	}
}

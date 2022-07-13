using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissapear : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        //it's opacity is increaced each frame
        gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.b, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.a - .02f);
        //if it has no oppacity, it destroys itself
        if (gameObject.GetComponent<SpriteRenderer>().color.a < .03f)
        {
            Destroy(gameObject);
        }
    }
}

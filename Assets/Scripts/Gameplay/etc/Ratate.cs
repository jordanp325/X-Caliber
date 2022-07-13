using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ratate : MonoBehaviour {
    
	// Update is called once per frame
	void Update () {
        //this simply rotates the object attatched to it every frame
        gameObject.transform.rotation = Quaternion.Euler(0, 0, gameObject.transform.rotation.eulerAngles.z + 5);
	}
}

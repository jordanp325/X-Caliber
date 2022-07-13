using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.transform.localScale = new Vector3(Vars.vars.Roomsize, Vars.vars.Roomsize, 1);
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = Vars.CurrentRoom.transform.position + new Vector3(-.5f, .5f);
	}
}

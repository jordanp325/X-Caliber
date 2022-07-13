using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour {

	// Use this for initialization
	public virtual void Start ()
    {

	}
	
	// Update is called once per frame
	public virtual void Update ()
    {
        
	}
    
    // FixedUpdate is called once per fixed frame
    public virtual void FixedUpdate()
    {
        //calculates the distance
        float distance = Vector2.Distance(gameObject.transform.position, Vars.vars.Player.gameObject.transform.position);
        if (distance <= .25f)
        {
            pickUp();
            //destroys the gameobject
            Destroy(gameObject);
        }
        else if (distance <= Vars.vars.PickUpDistance)
        {
            //moves the pickup closer if it is too far away, and increases speed the closer you are to it
            Vector3 force = Vars.vars.Player.gameObject.transform.position - gameObject.transform.position;
            gameObject.GetComponent<Rigidbody2D>().velocity = (force / (force.magnitude * force.magnitude)) * 5;
        }
    }

    //method for pickup
    public virtual void pickUp()
    {

    }
}

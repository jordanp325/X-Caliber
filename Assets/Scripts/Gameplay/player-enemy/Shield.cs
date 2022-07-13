using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    //durability variable
    public float durability = 100;
    //a few editor vars that get passed in (for balancing)
    public int InitialCost;
    public int IncrementalCost;
    public int RechargeDelay;
    public int RechargeStrength;
    public int brokentime;
    public float brokenslow;
    public GameObject shield;
    //a few reccesive vars
    float timer;
    bool broken;
    float brokentimer;
    bool shielded;


    // FixedUpdate is called once per fixed frame
    void FixedUpdate ()
    {
        //does things if it is broken
        if (brokentimer > 0)
        {
            brokentimer--;
            Vars.vars.Player.ShieldSlow += (brokenslow / brokentime) * Time.deltaTime;
        }
        else
        {
            broken = false;
            Vars.vars.Player.ShieldSlow = 1;
        }
        //increment the recharge delay
        if (timer > 0)
            timer--; 

        //checks if the recharge delay is finished and the durability is below 100
        else if (durability < 100)
        {
            durability += RechargeStrength * Time.deltaTime;
        } 

        //makes sure it never goes over 100
        if (durability > 100)
            durability = 100;

        //the different actions to happen when the rmb is pressed, held, and released
        if (Input.GetMouseButtonDown(1) && !broken)
        {
            //when the rmb is first pressed
            durability -= InitialCost;
            shielded = true;
        }
        else if (Input.GetMouseButton(1) && !broken)
        {
            //when the rmb is held
            durability -= IncrementalCost * Time.deltaTime;
            shielded = true;
        }
        else if (shielded)
        {
            //when the rmb is released
            timer = RechargeDelay / Time.deltaTime;
            shielded = false;
        }
        if(shielded == true)
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            timer = 10;
        }
        else
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

        //checks if the shield is broken
        if (durability <= 0)
            Shieldbreak();

        //display the shield

        if (durability == 100)
            shield.SetActive(false);
        else
        {
            shield.SetActive(true);
            Vars.vars.ShieldDisplay.fillAmount = durability / 100;
        }

    }

    public void Shieldbreak()
    {
        //breaks itself
        broken = true;
        brokentimer = brokentime / Time.deltaTime;
        //does the things when it breaks
        Vars.vars.Player.ShieldSlow = 1 - brokenslow;
        Vars.vars.Player.clip = 0;
    }

    public void OnTriggerEnter2D (Collider2D collision)
    {
        //checks if it is an enemy bullet, if so, destroys it
        if (collision.gameObject.layer == 12)
        {
            Destroy(collision.gameObject);
        }
        else
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }
    }
}

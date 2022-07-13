using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Container : MonoBehaviour
{
    //bool for if the container holds a part or a mod
    public bool part;
    //the contained part/mod
    public Part Containsp;
    public Module Containsm;
    //a few refrences to the UI objects
    public Image display;
    public Text num;
    //a method assigned by different scripts that is called whenever something is changed
    public Action<Container> update;
	
	// Update is called once per frame
	public virtual void Update ()
    {
        //checks if the container contains anything 
        if (part && Containsp != null)
        {
            //updates the sprite ane enables it
            display.sprite = Containsp.sprite;
            display.enabled = true;
            //updates the number of parts, enables it, and changes it based on rarity
            num.text = Containsp.amount.ToString();
            num.color = Common.GetColor(Common.GetRarity(Containsp.type));
            num.enabled = true;
        }
        else if(!part && Containsm != null)
        {
            //updates the sprite ane enables it
            display.sprite = Containsm.sprite;
            display.enabled = true;
        }
        else
        {
            //disables all UI
            display.enabled = false;
            if(part)
                num.enabled = false;
        }
    }

    //activated when the UI container is clicked on
    public virtual void ClickEvent ()
    {
        //checks if the type of object held matches the type of container
        if (Vars.vars.MF.held != 2 && part)
        {
            //checks if the two parts can stack
            if (Vars.vars.MF.ContainsP != null && Containsp != null && Vars.vars.MF.ContainsP.type == Containsp.type)
            {
                //adds the two amounts together and deletes one
                Containsp.amount += Vars.vars.MF.ContainsP.amount;
                Vars.vars.MF.ContainsP = null;
            }
            else
            {
                //switches the parts using an intermediary part, this works for any case
                Part temp = Vars.vars.MF.ContainsP;
                Vars.vars.MF.ContainsP = Containsp;
                Containsp = temp;
            }
            //calling the update function if it is assigned
            if(update != null)
                update(this);
        }
        else if (Vars.vars.MF.held != 1 && !part)
        {
            //switches the parts using an intermediary mod, this works for any case
            Module temp = Vars.vars.MF.ContainsM;
            Vars.vars.MF.ContainsM = Containsm;
            Containsm = temp;
            //calling the update function
            if (update != null)
                update(this);
        }
    }
}

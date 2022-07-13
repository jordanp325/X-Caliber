using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Trash : Container
{
    // Update is called once per frame
    public override void Update()
    {
        //checks if the container contains anything 
        if (Containsp != null)
        {
            //updates the sprite ane enables it
            display.sprite = Containsp.sprite;
            display.enabled = true;
            //updates the number of parts, enables it, and changes it based on rarity
            num.text = Containsp.amount.ToString();
            num.color = Common.GetColor(Common.GetRarity(Containsp.type));
            num.enabled = true;
        }
        else
        {
            //disables all UI
            display.enabled = false;
            num.enabled = false;
        }
    }

    //activated when the UI container is clicked on
    public override void ClickEvent()
    {
        //checks if the type of object held matches the type of container
        if (Vars.vars.MF.held != 2)
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
                //checks if you're holding anything
                if (Vars.vars.MF.ContainsP != null) {
                    //overbuilds the part (because it's a trash slot)
                    Containsp = Vars.vars.MF.ContainsP;
                    Vars.vars.MF.ContainsP = null;
                }
                else
                {
                    //takes the part out (if it was an accident)
                    Vars.vars.MF.ContainsP = Containsp;
                    Containsp = null;
                }
            }
        }
    }
}

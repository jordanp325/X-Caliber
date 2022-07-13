using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickUp : PickUp
{
    //the new pickup method
    public override void pickUp()
    {
        //add the money to total
        Vars.money++;
    }
}

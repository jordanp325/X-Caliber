using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Enemy {
    //since its a dummy, i have enabled it to constantly stay at full health if this bool is active
    public bool WillRegen;

    public override void secondupdate()
    {
        //checks if the regen bool is on, if so, it resets his health
        if (WillRegen)
        {
            CurrentHealth = StartingHealth;
        }
    }
}
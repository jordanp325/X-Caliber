using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic02 : Basic {

    public override void start()
    {
        ShootSpeed = (int)(ShootSpeed * .5f);
        Drops = new List<int>() { 13, 17 };
    }
}

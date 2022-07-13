using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unique01 : Advanced01 {

    public override void start()
    {
        Drops = new List<int>() { 2 };
        Type = 2;
        Unstoppable = true;
    }
}

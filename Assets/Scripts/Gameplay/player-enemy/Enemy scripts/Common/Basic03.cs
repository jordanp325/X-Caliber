using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic03 : Basic {

    public override void start()
    {
        Inaccuracy *= .5f;
        Range *= 2;
        BulletSpeed *= 2;
        Drops = new List<int>() { 0, 1, 14};
    }
}

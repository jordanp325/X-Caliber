using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Advanced03 : Basic {

    bool gap;

    public override void start()
    {
        BulletDamage = .5f;
        Drops = new List<int>() { 10 };
        Type = 1;
    }

    public override void Fire(float angle)
    {
        for (int i = -180; i < 180; i += 45)
        {
            Shoot(i + angle + (22.5f * System.Convert.ToInt32(gap)));
        }
        gap = !gap;
    }
}

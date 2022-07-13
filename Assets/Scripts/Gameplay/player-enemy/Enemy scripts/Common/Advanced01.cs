using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Advanced01 : Basic {

    public override void start()
    {
        BulletDamage = .5f;
        Drops = new List<int>() { 2 };
        Type = 1;
    }

    public override void Fire(float angle)
    {
        float gaps = 60 / 5;
        for (int i = 1; i <= 5; i++)
        {
            //spawns a bullet for each bullet and gap
            Shoot(angle + (60 / 2) - (i * gaps));
        }
    }
}

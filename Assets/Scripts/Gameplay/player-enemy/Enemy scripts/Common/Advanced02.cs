using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Advanced02 : Basic {

    protected int BurstTimer = 0;
    protected int BurstLeft = 0;
    protected float Angle;

    public override void start()
    {
        BulletDamage = .5f;
        Drops = new List<int>() { 11 };
        Type = 1;
    }

    public override void fixedupdate2()
    {
        BurstTimer--;
        //checks if it needs to shoot another bullet in the burst
        if (BurstLeft > 0 && BurstTimer <= 0)
        {
            //lowers the number of burst left and fires the gun
            BurstLeft--;
            Shoot(Angle);
            //checks if the burst is over
            if (BurstLeft > 0)
            {
                //resets the burst timer if unfinished burst
                BurstTimer = 5;
            }
        }
    }

    public override void Fire(float angle)
    {
        Shoot(angle);
        Angle = angle;
        BurstLeft = 2;
        BurstTimer = 5;
    }

}

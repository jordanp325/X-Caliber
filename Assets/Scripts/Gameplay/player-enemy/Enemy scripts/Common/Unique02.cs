using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unique02 : Advanced00 {

    public override void start()
    {
        Drops = new List<int>() { 4, 16, 18, 21 };
        shield.transform.rotation = Quaternion.Euler(0, 0, 180 + -1 * Mathf.Rad2Deg * Mathf.Atan2(Vars.vars.Player.transform.position.x - gameObject.transform.position.x, Vars.vars.Player.transform.position.y - gameObject.transform.position.y));
        Type = 2;
    }

    public override void Fire(float angle)
    {
        Shoot(angle);
    }
}

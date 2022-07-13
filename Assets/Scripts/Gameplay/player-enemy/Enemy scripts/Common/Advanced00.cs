using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Advanced00 : Basic {

    public GameObject shield;
    //int x;
    //Vector2 shieldDirection;

    public override void start()
    {
        Range *= .8f;
        Drops = new List<int>() { 4, 16, 18, 21 };
        shield.transform.rotation = Quaternion.Euler(0, 0, 180 + -1 * Mathf.Rad2Deg * Mathf.Atan2(Vars.vars.Player.transform.position.x - gameObject.transform.position.x, Vars.vars.Player.transform.position.y - gameObject.transform.position.y));
        Type = 1;
    }

    public override void Fire(float angle) { }

    public override void fixedupdate2()
    {
        ////declares the new soon-to-be new angle of the shield
        //float newdirection;

        //float strength = 0.72f;
        //float angle = Vector2.Angle(shieldDirection, Vars.vars.Player.transform.position - gameObject.transform.position);

        //if (strength > angle)
        //{
        //    strength = angle;
        //}

        ////checks if it is to the left or right based on perpendicular 3d vectors, and then subtracts or adds it accordingly
        //if (Vector3.Cross(shieldDirection, Vars.vars.Player.transform.position - gameObject.transform.position).z > 0)
        //{
        //    newdirection = shield.transform.rotation.eulerAngles.z + strength;
        //}
        //else
        //{
        //    newdirection = shield.transform.rotation.eulerAngles.z - strength;
        //}
        ////now it sets the new angle as the rotation of the bullets sprite and the vector of movement
        //shield.transform.rotation = Quaternion.Euler(0, 0, newdirection);
        //shieldDirection = new Vector2(Mathf.Cos(Mathf.Deg2Rad * (newdirection - 90)), Mathf.Sin(Mathf.Deg2Rad * (newdirection - 90)));

        shield.transform.rotation = Quaternion.Euler(0, 0, 180 + -1 * Mathf.Rad2Deg * Mathf.Atan2(Vars.vars.Player.transform.position.x - gameObject.transform.position.x, Vars.vars.Player.transform.position.y - gameObject.transform.position.y));
    }
}

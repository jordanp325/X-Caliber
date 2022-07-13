using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unique00 : Advanced02 {

    int timer;
    Vector2 direction;

    public override void start()
    {
        Drops = new List<int>() { 11 };
        Type = 2;
        moveHorizontal = Vars.vars.Player.gameObject.transform.position.x - gameObject.transform.position.x;
        moveVertical = (Vars.vars.Player.gameObject.transform.position.y + Vars.vars.PlayerOffset) - gameObject.transform.position.y;
        float angle = Mathf.Rad2Deg * Mathf.Atan2(moveVertical, moveHorizontal);
        angle += (90 * (System.Convert.ToInt32(Random.Range(1, 3) == 1) * 2 - 1));
        direction = new Vector2(Mathf.Cos(angle), Mathf.Sign(angle));
    }

    public override void fixedupdate()
    {
        if (BurstLeft == 1 && BurstTimer == 1)
        {
            moveHorizontal = Vars.vars.Player.gameObject.transform.position.x - gameObject.transform.position.x;
            moveVertical = (Vars.vars.Player.gameObject.transform.position.y + Vars.vars.PlayerOffset) - gameObject.transform.position.y;
            float angle = Mathf.Rad2Deg * Mathf.Atan2(moveVertical, moveHorizontal);
                angle += (60 * (System.Convert.ToInt32(Random.Range(1, 3) == 1) * 2 - 1));
                direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            timer = 0;
        }
        fixedupdate2();
        if (BurstLeft == 0)
        {
            //calculates the slow
            float slow = 1;
            if (WaterTimer > 0)
            {
                slow = Common.FullEffect(4, ModAccess);
            }
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * Vars.vars.EnemyMoveSpeed[Type] * slow * 1.5f, direction.y * Vars.vars.EnemyMoveSpeed[Type] * slow * 1.5f);
            timer++;
            if (timer == 50)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                //calculates the difference in position
                moveHorizontal = Vars.vars.Player.gameObject.transform.position.x - gameObject.transform.position.x;
                moveVertical = (Vars.vars.Player.gameObject.transform.position.y + Vars.vars.PlayerOffset) - gameObject.transform.position.y;
                float angle = Mathf.Rad2Deg * Mathf.Atan2(moveVertical, moveHorizontal);
                Fire(angle);
            }
        }
    }
}

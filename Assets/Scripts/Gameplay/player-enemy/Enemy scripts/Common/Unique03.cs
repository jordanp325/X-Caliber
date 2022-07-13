using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unique03 : Advanced03 {

    public override void start()
    {
        Drops = new List<int>() { 10 };
        Type = 2;
        Unrangable = true;
        MovementModifier = .25f;
    }

    public override void OnDeath()
    {
        base.OnDeath();
        Fire(Mathf.Rad2Deg * Mathf.Atan2(Vars.vars.Player.gameObject.transform.position.y + Vars.vars.PlayerOffset - gameObject.transform.position.y, Vars.vars.Player.gameObject.transform.position.x - gameObject.transform.position.x));
    }

    public override void Shoot(float angle)
    {
        //calcualtes the full error of each shot and adding the error to the angle
        angle += Random.Range(-1 * Inaccuracy, Inaccuracy);
        //creating the bullet
        GameObject bullet = Instantiate(Vars.vars.EnemyBulletPrefab, gameObject.transform.position, Quaternion.Euler(0, 0, angle));
        //set the direction and damage
        bullet.GetComponent<EnemyBullet>().direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
        bullet.GetComponent<EnemyBullet>().damage = (int)(BulletDamage * (10 * (1 - (Common.FullEffect(28, ModAccess) * System.Convert.ToInt32(HollyTimer > 0)))));
        bullet.GetComponent<EnemyBullet>().speed = BulletSpeed;
        bullet.GetComponent<EnemyBullet>().durability = 1;
        bullet.transform.localScale *= BulletSize;

    }

}

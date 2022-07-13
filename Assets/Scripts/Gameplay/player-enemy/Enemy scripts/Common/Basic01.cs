using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic01 : Basic {

    public override void start()
    {
        ShootSpeed *= 2;
        BulletDamage *= 2;
        BulletSize = 3;
        Drops = new List<int>() { 9, 23};
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
        bullet.transform.localScale *= BulletSize;
        bullet.transform.position += new Vector3(bullet.GetComponent<EnemyBullet>().direction.x, bullet.GetComponent<EnemyBullet>().direction.y) * 0.5f;
    }
}

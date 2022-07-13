using UnityEngine;
using System.Collections.Generic;

public class Basic : Enemy {
    //to keep track of the enemy shooting speed
    private int ShootTimer = 0;
    //basic balancable variables
    protected int ShootSpeed = 50;
    protected float BulletSpeed = 5;
    protected float BulletSize = 2;
    protected float BulletDamage = 1;
    protected float Inaccuracy = 5;
    protected float Range = 10;
    protected bool Unstoppable = false;
    protected bool Unrangable = false;
    protected float moveHorizontal;
    protected float moveVertical;
    protected float distance;
    protected float MovementModifier = 1f;
    //variable for the items the enemy can possibly drop, and the probability of doing so
    protected List<int> Drops = new List<int>() { 0, 1, 9, 13, 14, 17, 23 };
    protected int Type = 0;

    public override void basestart()
    {
        StartingHealth = Vars.vars.EnemyHealth[Type];
    }

    public override void OnDeath()
    {
        if (Drops.Count != 0 && Random.Range(1, 101) <= Vars.vars.EnemyDropChance[Type])
        {
            //creates the pickup and sets its values
            GameObject Pickup = Instantiate(Vars.vars.PickUpPrefab);
            Pickup.GetComponent<PartPU>().Contained = new Part(Drops[Random.Range(0, Drops.Count)], (int)Random.Range(1, 4));
            Pickup.transform.parent = transform.parent;
            Pickup.transform.position = gameObject.transform.position;
        }
    }

    public override void fixedupdate()
    {
        //calculates the difference in position
        moveHorizontal = Vars.vars.Player.gameObject.transform.position.x - gameObject.transform.position.x;
        moveVertical = (Vars.vars.Player.gameObject.transform.position.y + Vars.vars.PlayerOffset) - gameObject.transform.position.y;
        distance = Mathf.Sqrt(Mathf.Pow(moveHorizontal, 2) + Mathf.Pow(moveVertical, 2));
        Movement();

        //another shell function
        fixedupdate2();

        //increments the timer down once per frame
        ShootTimer++;

        if (ShootTimer == ShootSpeed)
        {
            //reset the timer
            ShootTimer = 0;
            //translate the position to an angle and rotate the indicator to reciprocate this
            float angle = Mathf.Rad2Deg * Mathf.Atan2(moveVertical, moveHorizontal);
            if (Mathf.Sqrt(Mathf.Pow(moveHorizontal, 2) + Mathf.Pow(moveVertical, 2)) <= Range || Unrangable)
            {
                //fire the gun
                Fire(angle);
            }
        }
    }

    public virtual void Movement()
    {
        
        float movementHorizontal = 0;
        float movementVertical = 0;

        //calculates the direction for the enemy to move in
        float movementAngle = Mathf.Rad2Deg * Mathf.Atan2(moveVertical, moveHorizontal);
        movementHorizontal = Mathf.Cos(movementAngle * Mathf.Deg2Rad);
        movementVertical = Mathf.Sin(movementAngle * Mathf.Deg2Rad);

        //calculates the slow
        float slow = 1;
        if (WaterTimer > 0)
        {
            slow = Common.FullEffect(4, ModAccess);
        }

        //moves towards the player
        if (distance >= Range / 2 || Unstoppable)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(movementHorizontal * Vars.vars.EnemyMoveSpeed[Type] * slow * MovementModifier, movementVertical * Vars.vars.EnemyMoveSpeed[Type] * slow * MovementModifier);
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    public virtual void fixedupdate2() { }

    public virtual void Fire(float angle)
    {
        Shoot(angle);
    }

    public virtual void Shoot(float angle)
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
    }
}

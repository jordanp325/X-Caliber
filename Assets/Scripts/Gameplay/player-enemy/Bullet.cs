using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    //the direction, speed, and lifetime of the bullet
    public Vector2 direction;
    public float speed;
    public int LifeTime;
    //the bools that show if it does half damage or if was spawned by a cluster part and not by the player
    public float Damage;
    public bool Cluster;
    //the both durability counters were set when the bullet was spawned
    public int durability;
    public int Pierce;
    //the access int(the mod log variation it was spawned with)
    public int ModVersion;
    //bool variable for the ricochat part
    public bool Ricochet;
    //easier access to the homing variable
    private bool Homing;
    

	// Use this for initialization
	void Start () {
        //sets the bullet's lifetime based on its range (the range increases the length in time that it travels, therefore bullet velocity also makes the bullet travel further)
        LifeTime = (int)(((1 + Common.FullEffect(1)) * LifeTime) / (1 + Common.FullEffect(0, ModVersion)));
        //sets the homing variable
        Homing = Vars.GunModActiveParts[ModVersion][8] > 0;

    }

    void FixedUpdate()
    {
        //
        Ricochet = true;
        //checks if the part has homing
        if (Homing)
        {
            //gets the closest enemy
            GameObject Closest = Common.GetClosestEnemy(gameObject.transform.position);
            if (Closest != null)
            {
                //gets the strength based on the number of active parts
                float strength = Common.FullEffect(8, ModVersion) * Time.deltaTime;
                //declares the new soon-to-be new angle of the bullet
                float newdirection;
                //flattens the strength if it is going to overshoot
                float angle = Vector2.Angle(direction, Closest.transform.position - gameObject.transform.position);
                if (strength > angle)
                {
                    strength = angle;
                }
                //checks if it is to the left or right based on perpendicular 3d vectors, and then subtracts or adds it accordingly
                if (Vector3.Cross(direction, Closest.transform.position - gameObject.transform.position).z > 0)
                {
                    newdirection = gameObject.transform.rotation.eulerAngles.z + strength;
                }
                else
                {
                    newdirection = gameObject.transform.rotation.eulerAngles.z - strength;
                }
                //now it sets the new angle as the rotation of the bullets sprite and the vector of movement
                gameObject.transform.rotation = Quaternion.Euler(0, 0, newdirection);
                direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * newdirection), Mathf.Sin(Mathf.Deg2Rad * newdirection));
            }
        }
        ////the bullet moves
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * speed * (1 + Common.FullEffect(0, ModVersion)), direction.y * speed * (1 + Common.FullEffect(0, ModVersion)));

        //the lifetime is reduced
        LifeTime--;
        //checks if it has gone through all of its lifetime
        if (LifeTime <= 0)
        {
            //destroys the gameobject if it has used up all of its life
            Destroy(gameObject);
        }
    }

    public void Hit(GameObject Enemy)
    {
        //checks if lightning is active * the debuff chance
        if (Vars.GunModActiveParts[ModVersion][12] > 0 && Random.Range(1, 100) <= Vars.vars.DebuffChance)
        {
            //sets up the list of objects to avoid as well as the variables that keep track of the next and last target
            List<GameObject> avoid = new List<GameObject>();
            GameObject last = null;
            GameObject next = Enemy;
            //calls the lightning effect on the first enemy hit
            Enemy.GetComponent<Enemy>().LightningEffect();
            for (int i = (int)Common.FullEffect(12, ModVersion); i > 0; i--)
            {
                //shifts the enemies back one variable as well as gets the next enemy
                avoid.Add(next);
                last = next;
                next = Common.GetClosestEnemy(last.transform.position, avoid);
                //checks if there are any enemies left
                if (next == null)
                {
                    //breaks the loop if no enemies left to chain to
                    break;
                }
                else
                {
                    //calls the lightning effect on the enemy
                    next.GetComponent<Enemy>().LightningEffect();
                    //creates the visual effect for it
                    GameObject lightning = Instantiate(Vars.vars.LightningPrefab);
                    lightning.transform.position = (last.transform.position + next.transform.position) / 2;
                    //figures out the direction it is supposed to be facing based on perpendicular 3 dimentional vectors
                    if (Vector3.Cross(next.transform.position - lightning.transform.position, Vector2.right).z >= 0)
                    {
                        lightning.transform.rotation = Quaternion.Euler(0, 0, 90 - Vector2.Angle(next.transform.position - lightning.transform.position, Vector2.right));
                    }
                    else
                    {
                        lightning.transform.rotation = Quaternion.Euler(0, 0, 90 + Vector2.Angle(next.transform.position - lightning.transform.position, Vector2.right));
                    }
                    //sets the scale of the indicator
                    lightning.transform.localScale *= Vector2.Distance(last.transform.position, next.transform.position) * (100f / 452f);
                }
            }
        }
        //checks if explosion is active * the debuff chance
        if (Vars.GunModActiveParts[ModVersion][6] > 0 && Random.Range(1, 100) <= Vars.vars.DebuffChance)
        {
            //goes through each enemy and checks if it is in explosion range
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (Vector2.Distance(gameObject.transform.position, enemy.transform.position) <= Vars.vars.ExplosionSize )
                {
                    //finds the knockback vector and calls the explosion effect on each enemy
                    float mult = Vars.vars.ExplosionSize - Vector3.Distance(enemy.transform.position, gameObject.transform.position);
                    enemy.GetComponent<Enemy>().ExplosionEffect((enemy.transform.position - gameObject.transform.position) * mult);
                }
            }
            //creates the visual indicator, sets the size and position
            GameObject Explosion = Instantiate(Vars.vars.ExplosionPrefab);
            Explosion.transform.position = gameObject.transform.position;
            Explosion.transform.localScale = new Vector3(Vars.vars.ExplosionSize, Vars.vars.ExplosionSize, 1);
        }
        //checks if shockwave is active * the debuff chance
        if (Vars.GunModActiveParts[ModVersion][22] > 0 && true)
        {
            //goes through each enemy and checks if it is in explosion range
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (Vector2.Distance(gameObject.transform.position, enemy.transform.position) <= Common.FullEffect(22, ModVersion))
                {
                    //calls the shockwave effect
                    enemy.GetComponent<Enemy>().ShockwaveEffect();
                }
            }
            //creates the visual indicator, sets the size and position
            GameObject Shockwave = Instantiate(Vars.vars.ShockwavePrefab);
            Shockwave.transform.position = gameObject.transform.position;
            float size = Common.FullEffect(22, ModVersion);
            Shockwave.transform.localScale = new Vector3(size, size, 1);
            Shockwave.GetComponent<SpriteRenderer>().sortingOrder = -1;
        }
    }

    //gets called whenever a trigger enters the collider's area
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks if it collided with an enemy bullet and if there is stacks of pierce left 
        if (collision.gameObject.layer == 12 && Pierce > 0)
        {
            //increments pierce down
            Pierce--;
            //destroys the bullet
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.layer == 12)
        {
            //else ignores the collision
            Collider2D[] colliders = gameObject.GetComponents<Collider2D>();
            foreach(Collider2D collider in colliders)
            {
                Physics2D.IgnoreCollision(collision, collider);
            }
        }
    }
}

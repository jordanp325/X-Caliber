using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    //starting and current health
    protected int StartingHealth;
    protected float CurrentHealth;
    protected float BulletResistance = 1;
    //the debuff timers and indicators
    protected int FireTimer;
    protected int PoisonTimer;
    protected int WaterTimer;
    protected int IceTimer;
    protected int UnhollyTimer;
    protected int HollyTimer;
    protected int MidasTimer;
    protected int EarthTimer;
    //the mod log access int
    protected int ModAccess;
    //the frame counter basicly makes all debuffs + etc go off only once every second
    private int FrameCounter = 0;
    //a bool to keep track if the enemy is vulnerable or not
    protected bool Vulnerable = false;
    //a value to see how much longer an enemy will be knocked back
    protected int KnockedBack = 0;
    protected Vector2 KnockbackEffect;

    // Use this for initialization
    public void Start()
    {
        start();
        basestart();
        //sets the starting health
        CurrentHealth = StartingHealth;
    }

    public virtual void basestart() { }//nothing here as it is for overiding

    public virtual void start() { }//nothing here as it is for overiding

    public virtual void secondupdate() { }//nothing here as it is for overiding

    public virtual void fixedupdate() { }//nothing here as it is for overiding

    public virtual void OnDeath() { }//nothing here as it is for overiding

    // FixedUpdate is called once per fixed frame
    public virtual void FixedUpdate()
    {
        //applies knockback to the enemy
        if (KnockedBack != 0)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(KnockbackEffect);
            KnockedBack--;
        }
        //checks if ice is active
        if (IceTimer > 1)
        {
            //increments the ice timer
            IceTimer--;
        }
        else
        {
            //checks if the ice timer just went off
            if (IceTimer == 1)
            {
                IceTimer--;
                //clears the indicator if so
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
            fixedupdate();
        }

        //incrementing the frame timer
        FrameCounter++;
        //checking if it has been a second yet
        if (FrameCounter >= (int)(1 / Time.deltaTime))
        {
            //resets the frame counter
            FrameCounter = 0;
            //calls the update function for other sub-scripts
            secondupdate();
            //checks if the fire timer is still going
            if (FireTimer > 0)
            {
                //calls the fire effect
                FireEffect();
                //creates the visual indicator and moves it to the enemy's position
                GameObject Fire = Instantiate(Vars.vars.FirePrefab);
                Fire.transform.position = gameObject.transform.position;
                //lowers the fire timer
                FireTimer--;
            }
            //checks if the poison timer is still going
            if (PoisonTimer > 0)
            {
                //calls the poison effect
                PoisonEffect();
                //creates the visual indicator and moves it to the enemy's position
                GameObject Poison = Instantiate(Vars.vars.PoisonPrefab);
                Poison.transform.position = gameObject.transform.position;
                //lowers the poison timer
                PoisonTimer--;
            }
            //checks if the water timer is still going
            if (WaterTimer > 0)
            {
                //lowers the water timer
                WaterTimer--;
                if (WaterTimer == 0)
                {
                    //resets the color to white if the water has run dry
                    gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
            //checks if the Holly timer is still going
            if (HollyTimer > 0)
            {
                //lowers the Holly timer
                HollyTimer--;
            }
            //checks if the Holly timer is still going
            if (UnhollyTimer > 0)
            {
                //lowers the Holly timer
                UnhollyTimer--;
            }
            //checks if the Midas timer is still going
            if (MidasTimer > 0)
            {
                //lowers the Midas timer
                MidasTimer--;
            }
            //checks if the Earth timer is still going
            if (EarthTimer > 0)
            {
                //lowers the Earth timer
                EarthTimer--;
            }
        }
        //checks if it has died
        if (CurrentHealth <= 0)
        {
            //shows the indicator of death
            GameObject indicator = Instantiate(Vars.vars.DeathPrefab);
            indicator.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1);
            OnDeath();
            if (Random.Range(1, 3) == 1)
            {
                //drops a coin in 1/3 cases
                GameObject money = Instantiate(Vars.vars.MoneyPrefab);
                money.transform.position = gameObject.transform.position;
                money.transform.parent = transform.parent;
                //has a chance at activating the midas part
                if (MidasTimer > 0 && Random.Range(1, 100) <= Vars.vars.DebuffChance)
                {
                    //repeats the additional coin for the number of times times goes over 100%
                    float a = Common.FullEffect(26, ModAccess);
                    for (int i = 0; i < Mathf.RoundToInt(a + (1 - (a % 1))); i++)
                    {
                        //drops an additional coin
                        GameObject money2 = Instantiate(Vars.vars.MoneyPrefab);
                        money2.transform.position = gameObject.transform.position;
                        money2.transform.parent = transform.parent;
                    }
                }
            }
            //checks if the earthen debuff is still active * the debuff chance
            if (EarthTimer > 0 && Random.Range(1, 100) <= Vars.vars.DebuffChance)
            {
                //creates the earthen wall
                GameObject earth = Instantiate(Vars.vars.EarthPrefab);
                earth.transform.position = gameObject.transform.position;
                earth.GetComponent<Earth>().Durability = (int)Common.FullEffect(24, ModAccess);
            }
            //kills the enemy
            Destroy(gameObject);
        }
    }

    //this triggers when the collider of the object has a trigger object enter its borders(reffered to as "other")
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        //checks if it is a player bullet
        if (other.gameObject.layer == 10)
        {
            //gets the script for the bullet and the mod log number
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            ModAccess = bullet.ModVersion;
            //calls the hit script on the bullet for damages that interact with more than one enemy(like explosion or chain)
            bullet.Hit(gameObject);
            //calls the bullet shot method
            Shot(bullet.Damage);
            //temporary storage for bullet damage
            float Damage = bullet.Damage;
            //checks if it has any durability, if it doesnt, kills the bullet
            if (bullet.durability > 0)
            {
                //if it does have bullet durability, it lowers the counter for it and ignores any future collisions with this enemy
                Collider2D[] colliders = other.GetComponents<Collider2D>();
                foreach (Collider2D collider in colliders)
                {
                    Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collider);
                }
                //Physics2D.IgnoreCollision(other.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
                bullet.durability--;
                bullet.Damage *= (2f / 3f);
            }
            else
            {
                Destroy(other.gameObject);
            }
            //checks if the bullet has any knockback
            if (Vars.GunModActiveParts[ModAccess][21] > 0)
            {
                KnockedBack = 5;
                //calls the knockback method with the knockback vector
                KnockbackEffect = new Vector2(
                        bullet.direction.x * (Common.FullEffect(21, ModAccess)),
                        bullet.direction.y * (Common.FullEffect(21, ModAccess))
                        );
            }
            //checks if a fire part is active * the debuff chance
            if (Vars.GunModActiveParts[ModAccess][19] > 0 && Random.Range(1, 100) <= Vars.vars.DebuffChance)
            {
                //calls the fire function
                FireStart();
            }
            //checks if a poison part is active * the debuff chance
            if (Vars.GunModActiveParts[ModAccess][20] > 0 && Random.Range(1, 100) <= Vars.vars.DebuffChance)
            {
                //calls the poison function
                PoisonStart();
            }
            //checks if a water part is active * the debuff chance
            if (Vars.GunModActiveParts[ModAccess][4] > 0 && Random.Range(1, 100) <= Vars.vars.DebuffChance)
            {
                //calls the water function
                WaterEffect();
            }
            //checks if an ice part is active * the debuff chance
            if (Vars.GunModActiveParts[ModAccess][3] > 0 && Random.Range(1, 100) <= Vars.vars.DebuffChance)
            {
                //calls the ice function
                IceEffect();
            }
            //checks if an armor pierce part is active * the debuff chance
            if (Vars.GunModActiveParts[ModAccess][15] > 0)
            {
                //calls the armor pierce function
                APEffect(Damage);
            }
            //checks if an vampire part is active
            if (Vars.GunModActiveParts[ModAccess][25] > 0)
            {
                //calls the vampiric function
                VampiricEffect();
            }
            //checks if an assassin part is active
            if (Vars.GunModActiveParts[ModAccess][27] > 0)
            {
                //calls the assassin function
                AssassinEffect();
            }
            //checks if a Holly part is active
            if (Vars.GunModActiveParts[ModAccess][28] > 0 && Random.Range(1, 100) <= Vars.vars.DebuffChance)
            {
                //calls the Holly function
                HollyEffect();
            }
            //checks if an Unholly part is active
            if (Vars.GunModActiveParts[ModAccess][29] > 0 && Random.Range(1, 100) <= Vars.vars.DebuffChance)
            {
                //calls the Unholly function
                UnhollyEffect();
            }
            //checks if a Midas part is active
            if (Vars.GunModActiveParts[ModAccess][26] > 0)
            {
                //calls the Midas function
                MidasEffect();
            }
            //checks if an Earth part is active
            if (Vars.GunModActiveParts[ModAccess][24] > 0)
            {
                //calls the Earth function
                EarthEffect();
            }
            //checks if there is a bullet cluster part active and that the bullet isnt already from a bullet cluster
            if (Vars.GunModActiveParts[ModAccess][10] > 0 && CurrentHealth <= 0)
            {
                //finds the variables needed to contruct the explosion of bullets such as the difference in angle, the number of bullets, and where the rotation starts
                int cluster = (int)Common.FullEffect(10, ModAccess);
                float gap = 360 / cluster;
                float root = Random.Range(0, 360);
                //goes through and spawns all the bullets needed
                for (int i = 1; i <= cluster; i++)
                {
                    Common.SpawnBullet((gap * i) + root, gameObject.transform.position, 5f, false, gameObject, ModAccess);
                }
            }
        }
    }


    //these functions are where the debuffs go so that they can easily be added to with items and such

    //the method for a lightning strike
    public virtual void LightningEffect()
    {
        //loses health based on a set value
        CurrentHealth -= Vars.vars.LightningDamage * (1 + System.Convert.ToInt32(WaterTimer > 0));
    }

    //the method for an explosion
    public virtual void ExplosionEffect(Vector2 Knockback)
    {
        //loses health based on a value that increases with the number of parts
        CurrentHealth -= Common.FullEffect(6, ModAccess);
        //knocks back the enemy based on distance and direction
        gameObject.GetComponent<Rigidbody2D>().AddForce(Knockback);
    }

    //the method for taking poison damage
    public virtual void PoisonEffect()
    {
        //loses health based on a set value
        CurrentHealth -= Vars.vars.PoisonDebuffDamage;
    }

    //the method for poison
    public virtual void PoisonStart()
    {
        //sets the poison timer
        PoisonTimer = (int)Common.FullEffect(20, ModAccess);
    }

    //the method for taking fire damage
    public virtual void FireEffect()
    {
        //loses health based on a value that increases with the number of parts
        CurrentHealth -= Common.FullEffect(19, ModAccess);
    }

    //the method for fire
    public virtual void FireStart()
    {
        //sets the fire timer
        FireTimer = Vars.vars.FireDebuffTime;
        //calls the water effect if ice exists and removes the ice
        if (IceTimer > 0)
        {
            WaterEffect();
            IceTimer = 1;
        }
    }

    //the method for taking damage if hit by a bullet
    public virtual void Shot(float Damage)
    {
        //subtracts the damage done by the bullet
        CurrentHealth -= BulletResistance * (Damage + Common.FullEffect(9, ModAccess)) * (1 + (Common.FullEffect(29, ModAccess) * System.Convert.ToInt32(UnhollyTimer > 0)));
        //checks if it is vulnerable 
        if (Vulnerable)
        {
            //does bonus damage when vulnerable, resets vulnerable
            CurrentHealth -= 100;
            Vulnerable = false;
        }
    }

    //the method for the water debuff
    public virtual void WaterEffect()
    {
        //sets the water timer
        WaterTimer = Vars.vars.WaterDebuffTime;
        //sets the sprite color to blue
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, .5f, 1f);
        //puts out any fire
        FireTimer = 0;
    }

    //the method for the ice debuff
    public virtual void IceEffect()
    {
        //checks if fire is active
        if (FireTimer == 0)
        {
            //sets the Ice timer
            IceTimer = (int)(Common.FullEffect(3, ModAccess) * 50);
            //cuts the speed
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //create the visual indicator
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 1f);


            //IceDisplay = Instantiate(Vars.vars.IcePrefab);
            //IceDisplay.transform.position = gameObject.transform.position;
        }
        else
        {
            //calls the water effect if ice was going to activate
            WaterEffect();
        }
    }

    //the method for the armor pierce effect 
    public virtual void APEffect(float Damage)
    {
        //takes the additional damage
        if (Damage == 10f)
        {
            //does the percent health specified by the full effect
            CurrentHealth -= StartingHealth * Common.FullEffect(15, ModAccess);
        }
        else
        {
            //does 1/5th damage if fired from cluster, spread or burst
            CurrentHealth -= StartingHealth * Common.FullEffect(15, ModAccess) / 5f;
        }
    }

    //the method for the vampiric effect 
    public virtual void VampiricEffect()
    {
        //detects if the enemy has died recently
        if (CurrentHealth <= 0)
        {
            //activates the effect if the chance goes off
            if (Common.FullEffect(25, ModAccess) >= ((float)Random.Range(1,100) / 100f) && Vars.vars.Player.CurrentHealth < Vars.vars.Player.StartingHealth)
            {
                Vars.vars.Player.CurrentHealth++;
            }
        }
    }

    //the method for the assassin effect 
    public virtual void AssassinEffect()
    {
        //inflicts vulnerable if rng
        if (Common.FullEffect(27, ModAccess) >= ((float)Random.Range(1, 100) / 100f))
        {
            Vulnerable = true;
        }
    }

    //the method for the Holly effect 
    public virtual void HollyEffect()
    {
        //turns on the timer
        HollyTimer = 3;
        //checks if the two effects cancel out
        if (HollyTimer > 0 && UnhollyTimer > 0)
        {
            HollyTimer = 0;
            UnhollyTimer = 0;
        }
    }

    //the method for the Unholly effect 
    public virtual void UnhollyEffect()
    {
        //turns on the timer
        UnhollyTimer = 3;
        //checks if the two effects cancel out
        if (HollyTimer > 0 && UnhollyTimer > 0)
        {
            HollyTimer = 0;
            UnhollyTimer = 0;
        }
    }

    //the method for the Midas effect 
    public virtual void MidasEffect()
    {
        //turns on the timer
        MidasTimer = 3;
    }

    //the method for the Earth effect 
    public virtual void EarthEffect()
    {
        //turns on the timer
        EarthTimer = 3;
    }

    //the method for an explosion
    public virtual void ShockwaveEffect()
    {
        //loses health based on a value that increases with the number of parts
        CurrentHealth -= Vars.vars.ShockwaveDamage;
    }


}

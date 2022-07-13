using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    //movement vars
    public int speed;
    //UI refrences
    public GameObject aim;
    public GameObject canvas;
    //variables for the fire timer
    public int FireTime;
    private float FireTimer;
    //the inaccuracy variable
    public float Inaccuracy;
    //reload variables
    public int ReloadTime;
    private int ReloadTimer;
    private float ReloadModifier;
    public int ClipSize;
    public int clip;
    private int Fullreaload;
    //reload UI refrences
    public GameObject ReloadIndicator;
    public Image reloadfill;
    //burst variables
    public float BurstSpacing;
    private int BurstLeft;
    private float BurstTimer;
    //gameobject refrence for the lasersight
    public GameObject laser;
    //variables for keeping track of the player's health
    public int StartingHealth;
    public int CurrentHealth;
    public int HealthRegen;
    public int HealthRegenTimer;
    public bool dead = false;
    //variable for keeping track of damage
    float BulletDamage = 10;
    //refrence to the in-game sprite for the gun
    public SpriteRenderer GunInGame;
    //for the shield slow
    public float ShieldSlow;
    //for animation
    public int AnimationTime;
    int animationframe;
    int animationtimer;
    //for bullet display
    int lastclip;
    public GameObject bulletcontainer;
    GameObject bulletend;
    Image[] Bullets;
    public GameObject UIBulletPrefab;
    public GameObject TopPrefab;
    //for death
    public GameObject DeathScreen;
    public GameObject LoadingScreen;
    public Text LoadingText;
    private bool mousedown;
    private bool Rdown;


    // Use this for initialization
    void Start () {
        
        //sets all the variables to default
        ReloadTimer = 0;
        FireTimer = 0;
        lastclip = 0;
        clip = (int)(ClipSize + Common.FullEffect(13));
        CurrentHealth = StartingHealth;
        LoadingScreen.SetActive(false);
    }

    public IEnumerator Loading(AsyncOperation AO)
    {
        int Deadcounter2 = 0;
        while (AO.progress != 1f) {
            yield return new WaitForSeconds(1);
            Debug.Log(AO.progress);
            Deadcounter2++;
            if (Deadcounter2 == 4)
                Deadcounter2 = 0;
            string s = "Loading";
            for (int i = 0; i < Deadcounter2; i++)
            {
                s += ".";
            }
            LoadingText.text = s;
        }
    }

    private void Update()
    {
        if (Time.timeScale != 0 && Input.GetMouseButtonDown(0) && FireTimer < 0 && ReloadTimer <= 0)
            mousedown = true;
        if (Input.GetKeyUp(KeyCode.R) && ReloadTimer <= 0 && BurstLeft <= 0)
            Rdown = true;
    }

    //for physics interactions
    private void FixedUpdate()
    {
        //get the input from the arrow keys or wasd and add any movement modifiers
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float movementHorizontal = 0;
        float movementVertical = 0;

        if (Mathf.Abs(moveHorizontal) <= 0.1) moveHorizontal = 0;
        if (Mathf.Abs(moveVertical) <= 0.1) moveVertical = 0;

        if (!(moveVertical == 0 && moveHorizontal == 0)) {
            float movementAngle = Mathf.Rad2Deg * Mathf.Atan2(moveVertical, moveHorizontal);
            movementHorizontal = Mathf.Cos(movementAngle * Mathf.Deg2Rad);
            movementVertical = Mathf.Sin(movementAngle * Mathf.Deg2Rad);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(movementHorizontal * speed * ShieldSlow, movementVertical * speed * ShieldSlow);
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }

        
        //checks if there is little movement
        if (Mathf.Abs(moveHorizontal) < .1f && Mathf.Abs(moveVertical) < .1f) 
        {
            //resets the animation
            animationframe = 0;
            animationtimer = AnimationTime;
        }
        else if (animationtimer == AnimationTime)
        {
            //increments the animation timer
            animationtimer--;
            animationframe++;
            if (animationframe > 3)
            {
                animationframe = 0;
            }
        }
        else if(animationtimer > 0)
        {
            //increments the animation timer
            animationtimer--;
        }
        else
        {
            animationtimer = AnimationTime;
        }

        //get the mouse position
        Vector2 mousepos = new Vector2(Input.mousePosition.x - canvas.transform.localPosition.x, Input.mousePosition.y - canvas.transform.localPosition.y);
        //translate the position to an angle and rotate the indicator to reciprocate this
        float angle = Mathf.Rad2Deg * Mathf.Atan2(mousepos.y, mousepos.x);
        aim.transform.localEulerAngles = new Vector3(0, 0, angle);
        //for the gun display in game
        if (angle > 90 || angle < -90)
        {
            GunInGame.flipY = true;
        }
        else
        {
            GunInGame.flipY = false;
        }
        if (angle > 180)
            angle -= 360;

        //setting the sprite of the charecter based on the animation
        gameObject.GetComponent<SpriteRenderer>().sprite = Vars.vars.Playersprites[Vars.GunRarity][Common.GetSpriteIndex(angle)][animationframe];
        if (angle >= 0)
        {
            GunInGame.sortingOrder = 1;
        }
        else
        {
            GunInGame.sortingOrder = 3;
        }

        //regenerate the player's health [TEMPORARY]
        if(Vars.vars.crafting.DevMode)
        {
            HealthRegenTimer++;
            if (HealthRegenTimer == 10)
            {
                CurrentHealth += HealthRegen;
                HealthRegenTimer = 0;
            }
            if (CurrentHealth > StartingHealth)
            {
                CurrentHealth = 100;
            }
        }

        //display the health of the player
        Vars.vars.HealthDisplay.fillAmount = ((float)CurrentHealth) / ((float)StartingHealth);

        //for bullet UI display
        int j = (int)(ClipSize + Common.FullEffect(13));
        if (lastclip != j)
        {
            Destroy(bulletend);
            if (Bullets != null) {
                foreach (Image I in Bullets)
                {
                    Destroy(I.gameObject);
                }
            }
            Bullets = new Image[j];
            for (int i = 0; i < j; i++)
            {
                GameObject bullet = Instantiate(UIBulletPrefab);
                bullet.transform.SetParent(bulletcontainer.transform);
                bullet.transform.position = new Vector3(bulletcontainer.transform.position.x, 30f + (18 * i));
                Bullets[i] = bullet.GetComponent<Image>();
            }
            bulletend = Instantiate(TopPrefab);
            bulletend.transform.SetParent(bulletcontainer.transform);
            bulletend.transform.position = new Vector3(bulletcontainer.transform.position.x, 30f + (18 * j));
        }
        //for updating the bulet display
        int k = clip;
        for (int i = 0; i < j; i++)
        {
            if (k > 0)
                Bullets[i].sprite = Vars.vars.FullBullets[Vars.GunRarity];
            else
                Bullets[i].sprite = Vars.vars.EmptyBullets[Vars.GunRarity];
            k--;
        }

        //correct the number of bullets in your gun (if you have more than max)
        if(clip > (ClipSize + Common.FullEffect(13)))
        {
            clip = (int)(ClipSize + Common.FullEffect(13));
        }
        
        //checks if you want to reload manually
        if (Rdown && ReloadTimer <= 0 && BurstLeft <= 0)
        {
            Rdown = false;
            ReloadStart();
        }

        //lowers the burst timer
        BurstTimer--;
        if (FireTimer >= 0)
        {
            //lowers the fire timer if you arent in the middle of a burst
            FireTimer--;
        }
        //checks if you are able to fire (not bursting or reloading + the fire timer has expired)
        if (FireTimer < 0 && ReloadTimer <= 0)
        {

            //checks if the mousebutton is held down and the automatic part is on the gun
            if (Input.GetMouseButton(0) && Vars.GunModActiveParts[Vars.GunModNumber][18] > 0)
            {

                //resets the fire timer
                FireTimer += FireTime * (1 / (1 + Common.FullEffect(18))) * (1 + System.Convert.ToInt32(Vars.GunModActiveParts[Vars.GunModNumber][11] > 0 || Vars.GunModActiveParts[Vars.GunModNumber][2] > 0));
                if (clip > 0)
                {
                    //fires the gun and starts a burst if the gun has bulets in it
                    FireGun(angle);
                    BurstStart();
                    clip--;
                }
                else
                {
                    //otherwise, reloads
                    ReloadStart();
                }
            }
            else if (mousedown) //checks if the mousebutton is clicked
            {
                mousedown = false;
                //resets the fire timer
                FireTimer += FireTime * (1 / (1 + Common.FullEffect(18))) * (1 + System.Convert.ToInt32(Vars.GunModActiveParts[Vars.GunModNumber][11] > 0 || Vars.GunModActiveParts[Vars.GunModNumber][2] > 0));
                if (clip > 0)
                {
                    //fires the gun and starts a burst if the gun has bulets in it
                    FireGun(angle);
                    BurstStart();
                    clip--;
                }
                else
                {
                    //otherwise, reloads
                    ReloadStart();
                }
            }
        }

        //checks if it needs to shoot another bullet in the burst
        while (BurstLeft > 0 && BurstTimer <= 0)
        {
            //lowers the number of burst left and fires the gun
            BurstLeft--;
            FireGun(angle);
            //checks if the burst is over
            if (BurstLeft > 0)
            {
                //resets the burst timer if unfinished burst
                BurstTimer += (BurstSpacing * FireTime * (1 / (1 + Common.FullEffect(18))) / (Common.FullEffect(11) - 1));
            }
        }

        if (ReloadTimer >= 0)
        {
            //checks if its reloading and diplays it on the UI
            ReloadTimer--;
            reloadfill.fillAmount = ((float)Fullreaload - (float)ReloadTimer) / (float)Fullreaload;
        }

        if (ReloadTimer == 0)
        {
            //checks if its done reloading. if so, sets the clip size to full and turns of the indicator
            clip = (int)(ClipSize + Common.FullEffect(13));
            ReloadIndicator.SetActive(false);
        }
    }

    //method for starting the burst
    private void BurstStart()
    {
        if (Vars.GunModActiveParts[Vars.GunModNumber][11] > 0)
        {
            //sets the burst timer and number in the burst if the mod equipped has bursting timers
            BurstLeft = (int)Common.FullEffect(11);
            BurstTimer = (BurstSpacing * FireTime * (1 / (1 + Common.FullEffect(18))) / (Common.FullEffect(11) - 1));
        }
    }

    //method for firing the gun
    private void FireGun(float angle)
    {
        //sets the gap and number of bullets, along with damage (spreadshot)
        int bullets = (int)Common.FullEffect(2) + 1;
        float gaps = Vars.vars.SpreadAngle / (bullets + 1);
        float damage = BulletDamage;
        //cacluates bullet damage
        if (Vars.GunModActiveParts[Vars.GunModNumber][2] > 0 || Vars.GunModActiveParts[Vars.GunModNumber][11] > 0)
        {
            //FORMULAE BS
            int high;
            if (Vars.GunModActiveParts[Vars.GunModNumber][2] > Vars.GunModActiveParts[Vars.GunModNumber][11])
                high = Vars.GunModActiveParts[Vars.GunModNumber][2];
            else
                high = Vars.GunModActiveParts[Vars.GunModNumber][11];

            damage = (19 + high) / ((1 + Common.FullEffect(2)) * (1 + Common.FullEffect(11)));
        }
        for (int i = 1; i <= bullets; i++)
        {
            //spawns a bullet for each bullet and gap
            Common.SpawnBullet(angle + (Vars.vars.SpreadAngle / 2) - (i * gaps), gameObject.transform.position, damage, true, gameObject, Vars.GunModNumber);
        }
    }

    //method for starting a reload
    private void ReloadStart()
    {
        //sets the reload timer
        if (Vars.GunModActiveParts[Vars.GunModNumber][17] != 0) ReloadModifier = Common.FullEffect(17);
        else ReloadModifier = 1;
        ReloadTimer = (int)(ReloadTime * ReloadModifier);
        Fullreaload = ReloadTimer;
        reloadfill.fillAmount = 0;
        ReloadIndicator.SetActive(true);
        clip = 0;
    }

    //if an object collides with the player
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //checks if it was an enemy bullet
        if (collision.gameObject.layer == 12) {
            //subtracts the damage done by the bullet
            CurrentHealth -= collision.gameObject.GetComponent<EnemyBullet>().damage;
            //destroys the bullet
            Destroy(collision.gameObject);
        }
        if (CurrentHealth <= 0 && !Vars.vars.crafting.DevMode)
        {
            //game over
            dead = true;
            Time.timeScale = 0;
            LoadingScreen.SetActive(true);
            DeathScreen.SetActive(true);
        }
    }

    public void restart()
    {
        DeathScreen.SetActive(false);
        StartCoroutine(Vars.vars.Player.Loading(Common.Reset()));
    }
}

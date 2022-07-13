using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vars : MonoBehaviour {
    //for easy refrence
    public static Vars vars;

    //static vars for storage during runtime (see awake function for details)
    public static Part[] InventoryParts;
    public static Module[] InventoryMods;
    public static RoomManager CurrentRoom;
    public static int FloorNum;
    public static bool[,] Levelmap;
    public static Room[,] Level;
    static int gunmodnumber;
    public static int GunModNumber//the variable to access the current gun mod
    {
        get //default get statement
        {
            return gunmodnumber;
        }
        set //it will only loop through 10 numbers continuously
        {
            if (value > 9)
                gunmodnumber = 0;
            else
                gunmodnumber = value;
        }
    }
    public static int[][] GunModActiveParts;
    static int rarity;
    public static int GunRarity //variable for rarity of gun
    {
        get
        {
            return rarity;
        }
        set
        {
            //set the UI sprites based on gun rarity
            vars.Inventory.sprite = vars.InventoryUIs[value];
            vars.Crafting.sprite = vars.CraftingUIs[value];
            vars.Player.GunInGame.sprite = vars.Guns[value];
            vars.Shield.sprite = vars.Shields[value];
            vars.Reload.sprite = vars.ReloadUIs[value];
            rarity = value;
        }
    }
    public static int NumberOfParts;
    public static int money;

    //Instance Refrences
    [Space(10f)]
    [Header("Instance Refrences")]
    public Player Player;
    public Level level;
    public Camera camra;
    public SpriteRenderer Shield;
    public MouseFollow MF;
    public UIMain UI;
    public Image Inventory;
    public Image Reload;
    public Image Crafting;
    public Crafting crafting;
    public Image HealthDisplay;
    public Image ShieldDisplay;

    //UI Constants
    [Space(10f)]
    [Header("UI Constants")]
    public int MaxParts;
    public int MaxMods;

    //the prefab refrences
    [Space(10f)]
    [Header("The Prefabs")]
    public GameObject BulletPrefab;
    public GameObject EnemyBulletPrefab;
    public GameObject LightningPrefab;
    public GameObject ExplosionPrefab;
    public GameObject FirePrefab;
    public GameObject PoisonPrefab;
    public GameObject PickUpPrefab;
    public GameObject DeathPrefab;
    public GameObject MoneyPrefab;
    public GameObject EarthPrefab;
    public GameObject ShockwavePrefab;
    public GameObject FloorPrefab;
    public GameObject WallPrefab;
    public GameObject BackWallPrefab;
    public GameObject RoomManagerPrefab;


    //the sprites for parts and mods
    [Space(10f)]
    [Header("The Sprites")]
    public Sprite fez;
    public Sprite Black;
    public Sprite[] Mods;
    public Sprite[] Parts;
    public Sprite[] DroppedParts;
    public Sprite[] Bullets;
    public Sprite[] Guns;
    public Sprite[] Shields;
    public Sprite[] FullBullets;
    public Sprite[] EmptyBullets;
    public Sprite[] ReloadUIs;
    public Sprite[] CraftingUIs;
    public Sprite[] InventoryUIs;
    public Sprite[] TooltipUIs;
    public Sprite[] ComparisonUIs;
    public Sprite[][][] Playersprites;
    [Space(5f)]
    public Sprite[] CommonPlayer;
    public Sprite[] UncommonPlayer;
    public Sprite[] RarePlayer;
    public Sprite[] EpicPlayer;
    public Sprite[] LegendaryPlayer;

    //The Enemy vars
    [Space(10f)]
    [Header("The Enemy Balancing")]
    public int[] EnemyDropChance;
    public int[] EnemyMoveSpeed;
    public int[] EnemyHealth;

    //The player vars
    [Space(10f)]
    [Header("The Player Balancing")]
    public float[] StartingStrength;
    public float[] IncrementalStrength;
    [Space(5f)]
    public int PickUpDistance;
    public int Inaccuracy;
    public int SpreadAngle;
    public int FireDebuffTime;
    public int PoisonDebuffDamage;
    public int LightningDamage;
    public int WaterDebuffTime;
    public int ExplosionSize;
    public int DebuffChance;
    public int ShockwaveDamage;
    public float PlayerOffset;

    //etc vars
    [Space(10f)]
    [Header("Etc Balancing")]
    public int Roomsize;
    public int Levelsize;
    public bool spawn;


    // a start-up function for early assignment
    private void Awake()
    {
        //for refrence to this script
        vars = this;

        //game run
        Time.timeScale = 1;

        //to make enemies aim at the player's hitbox, not their sprite
        PlayerOffset = Vars.vars.Player.gameObject.GetComponent<BoxCollider2D>().offset.y * Vars.vars.Player.gameObject.transform.localScale.y;

        //to make things pixel perfect
        camra.orthographicSize = ((float)Screen.height) / (48f * 2f);

        //instantiating the inventory vars
        InventoryMods = new Module[MaxMods];
        InventoryParts = new Part[MaxParts];

        //setting the number of parts based on the array length given
        NumberOfParts = IncrementalStrength.Length;

        //instantiating the log of gun mods and its accessor
        GunModActiveParts = new int[10][] { new int[NumberOfParts], new int[NumberOfParts], new int[NumberOfParts], new int[NumberOfParts], new int[NumberOfParts], new int[NumberOfParts], new int[NumberOfParts], new int[NumberOfParts], new int[NumberOfParts], new int[NumberOfParts] };
        GunModNumber = 0;

        //organize whatever the frick the player sprites are
        Playersprites = new Sprite[5][][];
        Sprite[][] Player = new Sprite[5][] {CommonPlayer, UncommonPlayer, RarePlayer, EpicPlayer, LegendaryPlayer };
        for (int i = 0; i < 5; i++)
        {
            Sprite[][] rarity = new Sprite[6][];
            for (int j = 0; j < 6; j++)
            {
                rarity[j] = new Sprite[4] { Player[i][(j * 4)], Player[i][(j * 4) + 1], Player[i][(j * 4) + 2], Player[i][(j * 4) + 3], };
            }
            Playersprites[i] = rarity;
        }
    }

    //for rearranging a 1 dimentional array into a 2 dimentional one
    private T[,] arrange<T>(T[] Array, int height, int width)
    {
        T[,] array = new T[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                array[i, j] = Array[j + (i * (width))];
            }
        }
        return array;
    }
}

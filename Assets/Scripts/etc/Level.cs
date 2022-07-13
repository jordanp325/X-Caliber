using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
    //the variable that stores all themes
    public static Theme[] themes;

    //the basics
    [Space(10f)]
    [Header("Floor number")]
    public int FloorNum;

    //the enemy prefab refrences
    [Space(10f)]
    [Header("The Room Layouts")]
    public TextAsset layouts;
    public static Vector2[][] Layouts;


    //the enemy prefab refrences
    [Space(10f)]
    [Header("The Enemies")]
    public GameObject[] BasicEnemies;
    public GameObject[] AdvancedEnemies;
    public GameObject[] UniqueEnemies;
    public GameObject[] MiniBosses;
    public GameObject[] Bosses;
    public GameObject SecretBoss;



    //the floor sprite refrences
    [Space(10f)]
    [Header("The Room themes")]
    public Sprite[] Doors;
    public Sprite LeftSideDoor;
    public Sprite RightSideDoor;
    public Sprite[] walls;
    public Sprite[,] Walls;
    [Space(5f)]
    [Header("T0")]
    public Sprite[] Floor0;
    public Sprite[] floorwalls0;
    public Sprite[,] FloorWalls0;
    public Sprite SideWall0;
    [Space(5f)]
    [Header("T1")]
    public Sprite[] Floor1;
    public Sprite[] floorwalls1;
    public Sprite[,] FloorWalls1;
    public Sprite SideWall1;
    [Space(5f)]
    [Header("T2")]
    public Sprite[] Floor2;
    public Sprite[] floorwalls2;
    public Sprite[,] FloorWalls2;
    public Sprite SideWall2;
    [Space(5f)]
    [Header("T3")]
    public Sprite[] Floor3;
    public Sprite[] floorwalls3;
    public Sprite[,] FloorWalls3;
    public Sprite SideWall3;
    [Space(5f)]
    [Header("T4")]
    public Sprite[] Floor4;
    public Sprite[] floorwalls4;
    public Sprite[,] FloorWalls4;
    public Sprite SideWall4;
    [Space(5f)]
    [Header("T5")]
    public Sprite[] Floor5;
    public Sprite[] floorwalls5;
    public Sprite[,] FloorWalls5;
    public Sprite SideWall5;
    [Space(5f)]
    [Header("T6")]
    public Sprite[] Floor6;
    public Sprite[] floorwalls6;
    public Sprite[,] FloorWalls6;
    public Sprite SideWall6;
    [Space(5f)]
    [Header("T7")]
    public Sprite[] Floor7;
    public Sprite[] floorwalls7;
    public Sprite[,] FloorWalls7;
    public Sprite SideWall7;
    [Space(5f)]
    [Header("T8")]
    public Sprite[] Floor8;
    public Sprite[] floorwalls8;
    public Sprite[,] FloorWalls8;
    public Sprite SideWall8;
    [Space(5f)]
    [Header("T9")]
    public Sprite[] Floor9;
    public Sprite[] floorwalls9;
    public Sprite[,] FloorWalls9;
    public Sprite SideWall9;


    // Use this for initialization
    void Start () {
        //to organize the sprites
        Walls = arrange(walls, 3, 4);

        FloorWalls0 = arrange(floorwalls0, 3, 4);
        FloorWalls1 = arrange(floorwalls1, 3, 4);
        //FloorWalls2 = arrange(floorwalls2, 3, 4);
        //FloorWalls3 = arrange(floorwalls3, 3, 4);
        //FloorWalls4 = arrange(floorwalls4, 3, 4);
        //FloorWalls5 = arrange(floorwalls5, 3, 4);
        //FloorWalls6 = arrange(floorwalls6, 3, 4);
        //FloorWalls7 = arrange(floorwalls7, 3, 4);
        //FloorWalls8 = arrange(floorwalls8, 3, 4);
        //FloorWalls9 = arrange(floorwalls9, 3, 4);

        themes = new Theme[10]
        {
            new Theme(Floor0, FloorWalls0, SideWall0),
            new Theme(Floor1, FloorWalls1, SideWall1),
            new Theme(Floor2, FloorWalls2, SideWall2),
            new Theme(Floor3, FloorWalls3, SideWall3),
            new Theme(Floor4, FloorWalls4, SideWall4),
            new Theme(Floor5, FloorWalls5, SideWall5),
            new Theme(Floor6, FloorWalls6, SideWall6),
            new Theme(Floor7, FloorWalls7, SideWall7),
            new Theme(Floor8, FloorWalls8, SideWall8),
            new Theme(Floor9, FloorWalls9, SideWall9)
        };

        //gets the rooms from the file
        Vector2[][] layout = ReadAllFromFile();
        //Vector2[][] layout = null;
        Layouts = layout ?? new Vector2[1][] { new Vector2[16] { new Vector2(3, 3), new Vector2(4, 3), new Vector2(3, 4), new Vector2(4, 4), new Vector2(Vars.vars.Roomsize - 5, 3), new Vector2(Vars.vars.Roomsize - 5, 4), new Vector2(Vars.vars.Roomsize - 4, 3), new Vector2(Vars.vars.Roomsize - 4, 4), new Vector2(3, Vars.vars.Roomsize - 5), new Vector2(4, Vars.vars.Roomsize - 5), new Vector2(3, Vars.vars.Roomsize - 4), new Vector2(4, Vars.vars.Roomsize - 4), new Vector2(Vars.vars.Roomsize - 5, Vars.vars.Roomsize - 5), new Vector2(Vars.vars.Roomsize - 5, Vars.vars.Roomsize - 4), new Vector2(Vars.vars.Roomsize - 4, Vars.vars.Roomsize - 5), new Vector2(Vars.vars.Roomsize - 4, Vars.vars.Roomsize - 4) } };

        //create the level
        Vars.Level = Common.LevelGen();
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

    public Vector2[][] ReadAllFromFile()
    {
        string[] file = layouts.text.Split('\r');
        try
        {
            Vector2[][] rooms = new Vector2[file.Length][];
            for (int i = 0; i < file.Length; i++)
            {
                string[] room = file[i].Split(' ');
                int size = int.Parse(room[0]);
                if (size == Vars.vars.Roomsize) {
                    List<Vector2> walls = new List<Vector2>();
                    for (int j = 1; j < room.Length; j++)
                    {
                        string[] vector = room[j].Split(',');
                        walls.Add(new Vector2(int.Parse(vector[0]), int.Parse(vector[1])));
                    }
                    rooms[i] = walls.ToArray();
                }
            }
            return rooms;
        }
        catch
        {
            Debug.Log("Room Layout File is corrupt");
            return null;
        }
    }
}

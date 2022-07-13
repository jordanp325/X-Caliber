using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Theme {
    
    public Sprite[] Floor;
    public Sprite[,] FloorWalls;
    public Sprite SideWall;
    public Theme(Sprite[] floor, Sprite[,] floorwalls, Sprite sidewall)
    {
        Floor = floor;
        FloorWalls = floorwalls;
        SideWall = sidewall;
    }
}

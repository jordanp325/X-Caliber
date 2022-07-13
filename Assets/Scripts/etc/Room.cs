using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room{

	public int size { get; private set; }
    public Vector2 position { get; private set; }
    public bool[,] tiles { get; private set; }
    public RoomManager manager { get; private set; }
    public int IndexX { get; private set; }
    public int IndexY { get; private set; }

    public Vector2 BotLeft
    {
        get
        {
            return new Vector2(position.x - (size / 2), position.y - (size / 2));
        }
    }
    public Vector2 TopRight
    {
        get
        {
            return new Vector2(position.x + (size / 2), position.y + (size / 2));
        }
    }

    public Room(Vector2[] walls, Vector2 pos, int IndX, int IndY)
    {
        IndexX = IndX;
        IndexY = IndY;
        position = pos;
        tiles = Common.ToRoom(walls, IndX, IndY);
        size = Vars.vars.Roomsize;
        GameObject m = MonoBehaviour.Instantiate(Vars.vars.RoomManagerPrefab);
        m.transform.position = pos;
        manager = m.GetComponent<RoomManager>();
        manager.self = this;
    }

    public Room(RoomManager room)
    {
        manager = room;
        IndexX = room.self.IndexX;
        IndexY = room.self.IndexY;
        position = room.self.position;
        tiles = room.self.tiles;
        size = room.self.size;
    }

    public Vector2 LocationFromIndex(int x, int y)
    {
        return new Vector2(x + BotLeft.x, y + BotLeft.y);
    }

    public Vector2 RandomLocation()
    {
        Vector2 index = new Vector2(0, 0);
        while (tiles[(int)index.x, (int)index.y])
        {
            index = new Vector2(Random.Range(1, size), Random.Range(1, size));
        }
        if (LocationFromIndex((int)index.x, (int)index.y).y > BotLeft.y + 1 && LocationFromIndex((int)index.x, (int)index.y).x > BotLeft.x + 1 && LocationFromIndex((int)index.x, (int)index.y).y < TopRight.y - 1 && LocationFromIndex((int)index.x, (int)index.y).x < TopRight.x - 1)
            return LocationFromIndex((int)index.x, (int)index.y);
        else
            return RandomLocation();
    }
}

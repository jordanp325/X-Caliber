using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {
    public Room self;
    public bool NotActivated;
    public bool Cleared = true;
    public List<GameObject> Doors;

    public void Start()
    {
        //checks if the player is not in its bounds
        if (!(Vars.vars.Player.gameObject.transform.position.x > self.BotLeft.x &&
              Vars.vars.Player.gameObject.transform.position.x < self.TopRight.x &&
              Vars.vars.Player.gameObject.transform.position.y > self.BotLeft.y &&
              Vars.vars.Player.gameObject.transform.position.y < self.TopRight.y
            ))
        {
            NotActivated = true;
            gameObject.SetActive(false);
            Cleared = false;
        }
        else
        {
            Vars.CurrentRoom = this;
        }
    }

    //used to change the room that you are in
    public void OnEnable()
    {
        Vars.CurrentRoom = this;
        if (NotActivated && Vars.vars.spawn)
        {
            NotActivated = false;

            if (Vars.vars.Player.transform.position.x < self.BotLeft.x + 1)
            {
                Vars.vars.Player.transform.position = new Vector2(self.LocationFromIndex(1, 0).x, Vars.vars.Player.transform.position.y);
            }
            else if (Vars.vars.Player.transform.position.x > self.TopRight.x - 1)
            {
                Vars.vars.Player.transform.position = new Vector2(self.LocationFromIndex(self.size - 1, 0).x, Vars.vars.Player.transform.position.y);
            }
            else if (Vars.vars.Player.transform.position.y > self.TopRight.y - 1)
            {
                Vars.vars.Player.transform.position = new Vector2(Vars.vars.Player.transform.position.x, self.LocationFromIndex(0, self.size - 1).y);
            }
            else if (Vars.vars.Player.transform.position.y < self.BotLeft.y + 1)
            {
                Vars.vars.Player.transform.position = new Vector2(Vars.vars.Player.transform.position.x, self.LocationFromIndex(0, 1).y);
            }

            //randomly generate enemies here
            int spawns = Common.EnemiesToSpawn(Vars.vars.level.FloorNum);
            int uniques = Mathf.RoundToInt(((spawns + 1) / 2) - .1f);
            for (int i = 0; i < uniques; i++)
            {
                if (Random.Range(1, 101) <= Common.UniqueRarity(Vars.vars.level.FloorNum))
                {
                    GameObject enemy = Instantiate(Vars.vars.level.UniqueEnemies[Random.Range(0, Vars.vars.level.UniqueEnemies.Length)]);
                    enemy.transform.parent = transform;
                    enemy.transform.position = self.RandomLocation();
                    while (Vector2.Distance(enemy.transform.position, Vars.vars.Player.transform.position) <= 5) {
                        enemy.transform.position = self.RandomLocation();
                    }
                    spawns -= 2;
                }
                else
                    break;
            }
            if (spawns != 0)
            {
                int S = spawns + Random.Range(0, 2);
                for (int i = 0; i < S; i++)
                {
                    int j = Random.Range(0, Vars.vars.level.BasicEnemies.Length);
                    GameObject enemy = Instantiate(Vars.vars.level.BasicEnemies[j]);
                    if (j == 0 && Random.Range(1, 1001) == 1)
                        enemy.GetComponent<SpriteRenderer>().sprite = Vars.vars.fez;
                    enemy.transform.parent = transform;
                    enemy.transform.position = self.RandomLocation();
                    while (Vector2.Distance(enemy.transform.position, Vars.vars.Player.transform.position) <= 5)
                    {
                        enemy.transform.position = self.RandomLocation();
                    }
                }
                S = spawns + Random.Range(0, 2);
                for (int i = 0; i < S; i++)
                {
                    GameObject enemy = Instantiate(Vars.vars.level.AdvancedEnemies[Random.Range(0, Vars.vars.level.AdvancedEnemies.Length)]);
                    enemy.transform.parent = transform;
                    enemy.transform.position = self.RandomLocation();
                    while (Vector2.Distance(enemy.transform.position, Vars.vars.Player.transform.position) <= 5)
                    {
                        enemy.transform.position = self.RandomLocation();
                    }
                }
            }
            Doors = new List<GameObject>();
            if (!self.tiles[self.size / 2, self.size - 1])
            {
                GameObject D1 = Instantiate(Vars.vars.WallPrefab);
                D1.GetComponent<SpriteRenderer>().sprite = Vars.vars.level.Doors[2];
                D1.transform.parent = transform;
                D1.transform.position = self.LocationFromIndex(self.size / 2, self.size - 1) + Vector2.up;
                Doors.Add(D1);
                GameObject D2 = Instantiate(Vars.vars.WallPrefab);
                D2.GetComponent<SpriteRenderer>().sprite = Vars.vars.level.Doors[2];
                D2.transform.parent = transform;
                D2.transform.position = self.LocationFromIndex((self.size / 2) - 1, self.size - 1) + Vector2.up;
                Doors.Add(D2);
                GameObject W1 = Instantiate(Vars.vars.BackWallPrefab);
                W1.GetComponent<SpriteRenderer>().sprite = Vars.vars.level.LeftSideDoor;
                W1.transform.parent = transform;
                W1.transform.position = self.LocationFromIndex(self.size / 2 - 1, self.size - 2) + Vector2.up;
                Doors.Add(W1);
                GameObject W2 = Instantiate(Vars.vars.BackWallPrefab);
                W2.GetComponent<SpriteRenderer>().sprite = Vars.vars.level.RightSideDoor;
                W2.transform.parent = transform;
                W2.transform.position = self.LocationFromIndex((self.size / 2), self.size - 2) + Vector2.up;
                Doors.Add(W2);
            }
            if (!self.tiles[self.size - 1, self.size / 2])
            {
                GameObject D1 = Instantiate(Vars.vars.WallPrefab);
                D1.GetComponent<SpriteRenderer>().sprite = Vars.vars.level.Doors[3];
                D1.transform.parent = transform;
                D1.transform.position = self.LocationFromIndex(self.size - 1, self.size / 2) + Vector2.up;
                Doors.Add(D1);
                GameObject D2 = Instantiate(Vars.vars.WallPrefab);
                D2.GetComponent<SpriteRenderer>().sprite = Vars.vars.level.Doors[3];
                D2.transform.parent = transform;
                D2.transform.position = self.LocationFromIndex(self.size - 1, (self.size / 2) - 1) + Vector2.up;
                Doors.Add(D2);
            }
            if (!self.tiles[self.size / 2, 0])
            {
                GameObject D1 = Instantiate(Vars.vars.WallPrefab);
                D1.GetComponent<SpriteRenderer>().sprite = Vars.vars.level.Doors[0];
                D1.transform.parent = transform;
                D1.transform.position = self.LocationFromIndex(self.size / 2, 0) + Vector2.up;
                Doors.Add(D1);
                GameObject D2 = Instantiate(Vars.vars.WallPrefab);
                D2.GetComponent<SpriteRenderer>().sprite = Vars.vars.level.Doors[0];
                D2.transform.parent = transform;
                D2.transform.position = self.LocationFromIndex((self.size / 2) - 1, 0) + Vector2.up;
                Doors.Add(D2);
                GameObject W1 = Instantiate(Vars.vars.WallPrefab);
                W1.GetComponent<SpriteRenderer>().sprite = Vars.vars.Black;
                W1.transform.parent = transform;
                W1.transform.position = self.LocationFromIndex(self.size / 2, 0);
                Doors.Add(W1);
                GameObject W2 = Instantiate(Vars.vars.WallPrefab);
                W2.GetComponent<SpriteRenderer>().sprite = Vars.vars.Black;
                W2.transform.parent = transform;
                W2.transform.position = self.LocationFromIndex((self.size / 2) - 1, 0);
                Doors.Add(W2);
            }
            if (!self.tiles[0, self.size / 2])
            {
                GameObject D1 = Instantiate(Vars.vars.WallPrefab);
                D1.GetComponent<SpriteRenderer>().sprite = Vars.vars.level.Doors[1];
                D1.transform.parent = transform;
                D1.transform.position = self.LocationFromIndex(0, self.size / 2) + Vector2.up;
                Doors.Add(D1);
                GameObject D2 = Instantiate(Vars.vars.WallPrefab);
                D2.GetComponent<SpriteRenderer>().sprite = Vars.vars.level.Doors[1];
                D2.transform.parent = transform;
                D2.transform.position = self.LocationFromIndex(0, (self.size / 2) - 1) + Vector2.up;
                Doors.Add(D2);
            }
        }
    }

    // Update is called once per frame
    public void FixedUpdate ()
    {
        //check to see if the player has left yet
        if (Vars.vars.Player.gameObject.transform.position.x < self.BotLeft.x)
        {
            Vars.Level[self.IndexX - 1, self.IndexY].manager.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        else if (Vars.vars.Player.gameObject.transform.position.x > self.TopRight.x)
        {
            Vars.Level[self.IndexX + 1, self.IndexY].manager.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        else if (Vars.vars.Player.gameObject.transform.position.y > self.TopRight.y)
        {
            Vars.Level[self.IndexX, self.IndexY + 1].manager.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        else if (Vars.vars.Player.gameObject.transform.position.y < self.BotLeft.y)
        {
            Vars.Level[self.IndexX, self.IndexY - 1].manager.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        if (!Cleared) {
            //check to see if the player has killed all enemies
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            bool cleared = true;
            foreach (GameObject g in enemies)
            {
                if (g.transform.parent == transform)
                {
                    cleared = false;
                }
            }
            if (cleared)
            {
                foreach (GameObject G in Doors)
                {
                    Destroy(G);
                }
                Cleared = true;
            }
        }
    }
}

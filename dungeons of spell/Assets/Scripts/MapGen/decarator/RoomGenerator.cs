using UnityEngine;
using System.Collections;

/*
The purpose of this script is to spawn the rooms. Typically the rooms are designed by the room filler classes.
This is then passed to this class in the form of a 2d array.
This class uses the 2d array to do the actual instantiating of the objects.
 */

public class RoomGenerator
{

    // The actual pos of the room
    Vector2 pos;
    // The grid that holds the dasta for the room
    string[,] grid;
    // The size of the the tiles
    const int size = 2;

    Room room;
    public RoomGenerator(Vector2 position, string[,] mapGrid, Room rm, bool createFloor = false, float flwrChance = 2)
    {
        flowerChance = flwrChance;
        grid = mapGrid;
        pos = position;
        room = rm;
        if(createFloor)
        {
            CreateFloor();
        }
        Draw();

    }

    void CreateFloor()
    {
        for (int x = 0; x < grid.GetLength(1); x++)
        {
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                if(grid[y,x] == "O" || grid[y, x] == "M" || grid[y, x] == "MS" || grid[y, x] == "S" || grid[y, x] == "ST" || grid[y, x] == "T")
                {
                    string delim = System.IO.Path.DirectorySeparatorChar.ToString();
                    string path = "Walls" + delim + "Side1" + delim + "Path";
                    GameObject spawnedObject = GameObject.Instantiate(Resources.Load(path) as GameObject) as GameObject;
                    room.mapObjs.Add(spawnedObject);
                    spawnedObject.transform.position = pos + new Vector2(y * size, x * size);
                    spawnedObject.transform.parent = room.transform;
                }
            }
        }
    }

    // This is just here for testing
    void Start()
    {
        pos = Vector2.zero;
        grid = new string[20, 20]
        {
            {"X","X","X","X","X","X","X","X","X","X","X","X","X","X","X","X","X","X","X","X"},
            {"X","x","O","O","O","X","O","O","O","O","O","O","O","O","O","O","O","O","O","X"},
            {"X","x","O","O","O","X","O","O","O","O","O","O","O","O","O","D","O","O","D","X"},
            {"X","x","O","O","O","X","O","O","O","O","O","O","O","O","O","O","O","O","O","X"},
            {"X","x","O","O","O","O","O","O","O","O","O","O","O","O","O","O","O","O","O","X"},
            {"X","x","O","O","O","O","O","O","O","O","O","O","O","O","O","O","O","O","O","X"},
            {"X","x","O","O","O","X","O","O","O","O","O","O","O","O","O","O","O","O","O","X"},
            {"X","X","X","X","X","X","O","O","O","X","X","X","X","X","X","X","X","X","X","X"},
            {"X","O","O","O","X","O","O","O","O","O","O","X","O","O","O","O","O","O","O","X"},
            {"X","O","O","O","X","O","O","O","O","O","O","O","O","O","O","O","O","O","O","X"},
            {"X","O","O","O","X","O","O","O","O","O","O","O","O","O","O","O","O","O","O","X"},
            {"X","O","O","O","O","O","O","O","O","O","O","O","O","O","O","O","O","O","O","X"},
            {"X","O","O","O","O","O","O","O","O","O","O","X","X","O","O","O","O","O","O","X"},
            {"X","O","O","O","O","O","O","O","O","O","O","X","X","O","O","O","O","O","O","X"},
            {"X","O","O","O","X","O","O","O","O","O","O","O","O","O","O","X","X","O","O","X"},
            {"X","O","O","O","X","O","O","O","O","O","O","O","O","O","O","X","X","O","O","X"},
            {"X","O","O","O","X","O","O","O","O","O","O","X","O","O","O","O","O","O","D","X"},
            {"X","O","D","O","X","O","O","O","O","O","O","X","O","O","O","O","O","O","O","X"},
            {"X","O","O","O","X","O","O","O","O","O","O","X","O","O","O","O","O","O","O","X"},
            {"X","X","X","X","X","X","X","X","X","X","X","X","X","X","X","X","X","X","X","X"}
        };
        Draw();
    }

    public float flowerChance = 2;
    void Draw()
    {
        for (int x = 0; x < grid.GetLength(1); x++)
        {
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                CheckForLake(x, y);
                //CheckForBigTree(x, y);
                GameObject spawnedObject = GetGameObject(grid[y, x], x, y);
                if (spawnedObject)
                {
                    spawnedObject.transform.position = pos + new Vector2(y * size, x * size);
                    spawnedObject.transform.parent = room.transform;
                }
            }
        }
    }

    void CheckForLake(int yStart, int xStart)
    {

        if (grid[xStart, yStart] != "M" || xStart == grid.GetLength(1) - 1 || yStart == grid.GetLength(0) - 1)
        {
            return;
        }

        int x = xStart + 1;
        int y = yStart + 1;
        int count = 0;
        grid[xStart, yStart] = "R";

        while (x != grid.GetLength(0) - 1 && y != grid.GetLength(1) - 1 && grid[x, y] == "M")
        {
            count++;
            grid[x, y] = "R";

            for (int i = 0; i < count; i++)
            {
                //Debug.Log(x + " B " + y + " " + grid.GetLength(0) +  " " + grid.GetLength(1));

                //grid[x - i, y] = "O";
                //grid[x, y - i] = "O";
            }

            x++;
            y++;
            count += 1;
        }
        x--;
        y--;

        if (count == 0)
        {
            return;
        }
        Vector2 center = new Vector2(((pos.x + x * size) + (pos.x + xStart * size)) / 2, ((pos.y + y * size) + (pos.y + yStart * size)) / 2);
        float radius = (((float)(x - xStart) / 2) + .5f) * size;
        LakeDrawer drawer = new LakeDrawer(radius, center);
        for (int xx = 0; xx < grid.GetLength(0); xx++)
        {
            for (int yy = 0; yy < grid.GetLength(1); yy++)
            {
                if(grid[xx,yy] == "M")
                {
                    grid[xx, yy] = "R";
                }
            }
        }
    }

    void CheckForBigTree(int x, int y)
    {
        if (x == grid.GetLength(1) - 1 || y == grid.GetLength(0) - 1)
        {
            return;
        }
        if ((grid[y, x] == "X" && grid[y + 1, x + 1] == "X" && grid[y + 1, x] == "X" && grid[y, x + 1] == "X"))
        {
            grid[y + 1, x + 1] = "O";
            grid[y + 1, x] = "O";
            grid[y, x + 1] = "O";
            grid[y, x] = "B";
        }
    }
    /*
    O = Blank Space
    I = Ignore kind of the same as a blank space but i designed this poorly so now we are stuck with 'I'
    X = Blocker
    D = Destructable
    M = Movement Blocker
    S = Shop
    R = Door
    C = Chest
    F = Flowers
    MS = Mushroom
    ST = store ownder
    T = tomb stone
    Portal = Portal
    ==================
    B = BigTree
    */
    GameObject GetGameObject(string s, int x, int y)
    {
        string path = "";
        string delim = System.IO.Path.DirectorySeparatorChar.ToString();
        switch (s)
        {   
            case "O":
                if (Random.Range(0, 100) < flowerChance)
                {
                    path = "RoomStuff" + delim + "Foliage";
                    break;
                }
                return null;
            case "ST":
                path = "Enemy" + delim + "book1";
                break;
            case "R":
                return null;
            case "I":
                return null;
            case "X":
                if (x != 0 && (grid[y,x-1] == "O" || grid[y, x - 1] == "R" || grid[y, x - 1] == "MS"))
                {
                    path = "Walls" + delim + "Side1" + delim + 1 + delim + "pathWall"; //DetermineBushSize(x, y);
                    break;
                }
                path = "Walls" + delim + "Side1" + delim + 1 + delim + "path"; //DetermineBushSize(x, y);
                break;
            case "D":
                path = GetFence(x, y);
                break;
            case "M":
                return null;
            case "S":
                path = "RoomStuff" + delim + "Shop"; ;
                break;
            case "B":
                path = "BigTree";
                break;
            case "C":
                path = "RoomStuff" + delim + "Chest";
                break;
            case "F":
                path = "RoomStuff" + delim + "Foliage";
                break;
            case "MS":
                path = "RoomStuff" + delim + "Mushroom";
                break;
            case "T":
                path = "RoomStuff" + delim + "Tombstone";
                break;
            default:
                Debug.LogError("Room generator recieved an invalid character " + s);
                break;
        }
        GameObject pathObj = GameObject.Instantiate(Resources.Load(path) as GameObject) as GameObject;
        if(pathObj == null)
        {
            Debug.LogError(path);
        }
        if (rotate)
        {
            pathObj.transform.eulerAngles = new Vector3(0, 0, 90);
            rotate = false;
        }
        return pathObj;
    }

    bool rotate = false;

    string GetFence(int x, int y)
    {

        int xDir = 0;
        int yDir = 1;
        int xIt = x;
        int yIt = y;
        int count = 0;
        while (count < 2 && xIt < grid.GetLength(1) && yIt < grid.GetLength(0) && grid[yIt, xIt] == "D")
        {
            count++;


            if (count > 1)
            {
                grid[yIt, xIt] = "I";
            }

            xIt += xDir;
            yIt += yDir;
        }

        string delim = System.IO.Path.DirectorySeparatorChar.ToString();
        string path = "Walls" + delim + "Side1" + delim + count + delim + "Fence";
        if (count == 1)
        {
            xDir = 1;
            yDir = 0;
            xIt = x;
            yIt = y;
            count = 0;
            while (count < 2 && xIt < grid.GetLength(1) && yIt < grid.GetLength(0) && grid[yIt, xIt] == "D")
            {
                count++;


                if (count > 1)
                {
                    grid[yIt, xIt] = "I";
                }

                xIt += xDir;
                yIt += yDir;
            }
            if(count > 1)
            {
                path = "Walls" + delim + "Side1" + delim + count + delim + "VertFence";
            }
        }

        return path;
    }


    string DetermineBushSize(int x, int y)
        {
        int xDir = 0;
        int yDir = 1;
        int xIt = x;
        int yIt = y;
        int count = 0;
        while (count < 4 && xIt < grid.GetLength(1) && yIt < grid.GetLength(0) && grid[yIt, xIt] == "X")
        {
            count++;


            if (count > 1)
            {
                grid[yIt, xIt] = "I";
            }
            if (Random.Range(0, 100) > 70)
            {
                break;
            }

            xIt += xDir;
            yIt += yDir;
        }

        if (count == 1)
        {
            xDir = 1;
            yDir = 0;
            xIt = x;
            yIt = y;
            count = 0;
            rotate = true;
            while (count < 4 && xIt < grid.GetLength(1) && yIt < grid.GetLength(0) && grid[yIt, xIt] == "X")
            {
                count++;


                if (count > 1)
                {
                    grid[yIt, xIt] = "I";
                }

                if (Random.Range(0, 100) > 70)
                {
                    break;
                }
                xIt += xDir;
                yIt += yDir;
            }
        }
        if (count > 1 && xDir == 1)
        {
            rotate = true;
        }
        string delim = System.IO.Path.DirectorySeparatorChar.ToString();
        string path = "Walls" + delim + "Side1" + delim + count + delim + "Path";
        return path;
    }
}


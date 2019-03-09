using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RiverRoomFiller : CelluarRoomFiller
{

    float randomRiverFillChance = 40;
    public RiverRoomFiller(Vector2 pos, int x, int y, Room r, List<Vector2> ds) : base(pos, x, y, r, ds)
    {
        randomFillChance = 65;
    }

    int mushroomChance = 6;
    private void CeullarMiddle()
    {

        randomRiverFillChance = randomRiverFillChance * ((float)grid.GetLength(0) * grid.GetLength(1) / 350);
        Debug.Log(grid.GetLength(0) + " " + grid.GetLength(1) + " " + grid.GetLength(0) * grid.GetLength(1) + " " + randomRiverFillChance);
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j] == "O")
                {
                    if (grid[i, j] == "O" && IsNearWall(3, i, j))
                    {
                        grid[i, j] = "Border";
                    }
                    else if (Random.Range(0, 100) > randomRiverFillChance)
                    {
                        grid[i, j] = "OO";
                    }
                    else
                    {
                        grid[i, j] = "XX";
                    }
                }
            }
        }

        for (int i = 0; i < 2; i++)
        {
            CellularA();
        }

        for (int i = 0; i < 1; i++)
        {
            Smooth();
        }

        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                if (grid[x, y] == "Border")
                {
                    grid[x, y] = "O";
                }
                else if (grid[x, y] == "OO")
                {
                    grid[x, y] = "O";
                    int wallCount = GetSurroundingWallCount(x, y);

                    if (Random.Range(0, 100) < mushroomChance && wallCount >= 4 && wallCount < 8)
                    {
                        grid[x, y] = "MS";
                    }

                }
                else if (grid[x, y] == "XX")
                {
                    grid[x, y] = "X";
                }
            }
        }

    }

    int GetSurroundingCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < grid.GetLength(0) && neighbourY >= 0 && neighbourY < grid.GetLength(1))
                {
                    if (!(neighbourX == gridX && neighbourY == gridY))
                    {
                        if (grid[neighbourX, neighbourY] == "OO" || grid[neighbourX, neighbourY] == "X")
                        {
                            wallCount++;
                        }
                    }
                }
                else
                {
                    wallCount++;
                }
            }
        }
        return wallCount;
    }

    void Smooth()
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                if (grid[x, y] == "OO" || grid[x, y] == "XX")
                {
                    int neighbourWallTiles = GetSurroundingCount(x, y);

                    if (neighbourWallTiles > 4)
                        grid[x, y] = "OO";
                    else if (neighbourWallTiles < 4)
                        grid[x, y] = "XX";
                }
            }
        }
    }



    List<bool> Values(int xVal, int yVal)
    {
        List<bool> values = new List<bool>();
        values.Add(grid[xVal-1, yVal] == "OO" || grid[xVal-1, yVal] == "X");
        values.Add(grid[xVal, yVal] == "OO" || grid[xVal, yVal] == "X");
        values.Add(grid[xVal+1, yVal] == "OO" || grid[xVal+1, yVal] == "X");
        return values;
    }

    bool InRange(int xVal, int yVal)
    {
        if (xVal >= 0 && xVal < grid.GetLength(0) && yVal >= 0 && yVal < grid.GetLength(1))
        {
            return true;
        }
        return false;
    }
    void CellularA()
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                if (grid[x, y] == "OO" || grid[x, y] == "XX")
                {
                    List<bool> values = Values(x,y);
                    if (values[0] == true && values[1] == true && values[2] == true)
                    {
                        grid[x, y] = "XX";
                    }
                    else if (values[0] == true && values[1] == true && values[2] == false)
                    {
                        grid[x, y] = "OO";
                    }
                    else if (values[0] == true && values[1] == false && values[2] == true)
                    {
                        grid[x, y] = "OO";
                    }
                    else if (values[0] == true && values[1] == false && values[2] == false)
                    {
                        grid[x, y] = "XX";
                    }
                    else if (values[0] == false && values[1] == true && values[2] == true)
                    {
                        grid[x, y] = "XX";
                    }
                    else if (values[0] == false && values[1] == true && values[2] == false)
                    {
                        grid[x, y] = "XX";
                    }
                    else if (values[0] == false && values[1] == false && values[2] == true)
                    {
                        grid[x, y] = "OO";
                    }
                    else if (values[0] == false && values[1] == false && values[2] == false)
                    {
                        grid[x, y] = "OO";
                    }
                }
            }
        }
    }

    bool IsNearWall(int border, int gridX, int gridY)
    {
        if (border == 0)
        {
            return false;
        }
        else
        {
            for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
            {
                for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
                {
                    // If in range
                    if (neighbourX >= 0 && neighbourX < grid.GetLength(0) && neighbourY >= 0 && neighbourY < grid.GetLength(1))
                    {
                        if (!(neighbourX == gridX && neighbourY == gridY))
                        {
                            if (grid[neighbourX, neighbourY] == "X")
                            {
                                return true;
                            }
                            if (IsNearWall(border - 1, neighbourX, neighbourY))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }

    int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < grid.GetLength(0) && neighbourY >= 0 && neighbourY < grid.GetLength(1))
                {
                    if (!(neighbourX == gridX && neighbourY == gridY))
                    {
                        if (grid[neighbourX, neighbourY] == "XX" || grid[neighbourX, neighbourY] == "X")
                        {
                            wallCount++;
                        }
                    }
                }
                else
                {
                    wallCount++;
                }
            }
        }
        return wallCount;
    }

    public override Room FillRoom()
    {
        Ceullar();
        CeullarMiddle();
        RoomGenerator generator = new RoomGenerator(transform.position, grid, room, true);

        MakeDoors(false);

        FillEnemy();
        return room;
    }
}

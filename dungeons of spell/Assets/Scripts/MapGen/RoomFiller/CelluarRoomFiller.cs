using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CelluarRoomFiller : StartRoomFiller
{
    protected int medianXIndex = 0;
    protected int medianYIndex = 0;
    protected int width = 0;
    protected int height = 0;
    protected int blankTiles = 0;
    public CelluarRoomFiller(Vector2 pos, int x, int y) : base(pos,x,y) {

    }

    int randomFillChance = 65;
    bool VerifyRoom()
    {

        int count = 0;
        IntPair index = new IntPair(-1, -1);
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j] == "O")
                {
                    count++;
                    if(index.x == -1)
                    {
                        index.x = i;
                        index.y = j;
                    }
                }
            }
        }
        HashSet<IntPair> visted = new HashSet<IntPair>();
        Queue<IntPair> q = new Queue<IntPair>();
        q.Enqueue(index);
        int bfsCount = 0;
        while (q.Count > 0 && bfsCount <= count)
        {
            IntPair current = q.Dequeue();

            if (visted.Contains(current))
            {
                continue;
            }

            visted.Add(current);
            List<IntPair> pairs = new List<IntPair>()
            {
                new IntPair (current.x, current.y + 1),
                new IntPair (current.x, current.y - 1),
                new IntPair (current.x + 1, current.y),
                new IntPair (current.x - 1, current.y)
            };

            foreach (IntPair next in pairs)
            {
                if (!visted.Contains(next))
                {
                    if (IsInRange(next))
                    {
                        if (grid[next.x, next.y] == "O" || grid[next.x, next.y] == "R")
                        {
                            q.Enqueue(next);
                        }
                    }
                }
            }
            bfsCount++;
        }
        
        if (count == bfsCount)
        {
            return true;
        }
        return false;
    }

    public override Room FillRoom()
    {
        Ceullar();
        RoomGenerator generator = new RoomGenerator(transform.position, grid, room, true);

        MakeDoors(false);

        FillEnemy();
        return room;
    }

    protected void Ceullar()
    {
        bool roomVerified = false;
        while(!roomVerified)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (i == 0 || j == 0 || i == grid.GetLength(0) - 1 || j == grid.GetLength(1) - 1)
                    {
                        grid[i, j] = "X";
                    }
                    else
                    {
                        if (Random.Range(0, 100) > randomFillChance)
                        {
                            if (grid[i, j] != "R")
                                grid[i, j] = "X";
                        }
                        else
                        {
                            grid[i, j] = "O";
                        }
                    }
                }
            }
            for (int i = 0; i < 5; i++)
            {
                SmoothMap();
            }
            CreatePathFindingNodes();
            foreach (Vector2 pos in doors)
            {
                int x = (int)(pos.x - transform.position.x) / tileSize;
                int y = (int)(pos.y - transform.position.y) / tileSize;
                grid[x, y] = ("O");
                DrunkenWalkFromPosToCenter(x, y);
            }

            roomVerified = VerifyRoom();
            randomFillChance += 10;
        }


        ClearExtraWalls();
        GameObject.Destroy(room.transform.GetChild(0).gameObject);
        
    }

    void SmoothMap()
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                int neighbourWallTiles = GetSurroundingWallCount(x, y);

                if (neighbourWallTiles > 4)
                    grid[x, y] = "X";
                else if (neighbourWallTiles < 4)
                    grid[x, y] = "O";   

            }
        }
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
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        if (grid[neighbourX, neighbourY] != "O")
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

    void DrunkenWalkFromPosToCenter(int x, int y)
    {
        if (x != 0 && y != 0 && x != grid.GetLength(0) - 1 && y != grid.GetLength(1) - 1)
        {
            grid[x, y] = ("O");
        }
        
        int xDist = DistanceFromCenter(x, true);
        int yDist = DistanceFromCenter(y, false);
        if(Mathf.Abs(xDist) + Mathf.Abs(yDist) < 4)
        {
            return;
        }
        int xDir = 0;
        int yDir = 0;
        if(Mathf.Abs(xDist) * Random.Range(0,5) > Mathf.Abs(yDist) * Random.Range(0,5))
        {
            if(Mathf.Abs(xDist) < 4)
            {
                xDir = Random.Range(0, 100) > 50 ? -1 : 1;
            }
            else
            {
                xDir = xDist > 0 ? 1 : -1;
            }
        }
        else
        {
            if (Mathf.Abs(yDist) < 4)
            {
                yDir = Random.Range(0, 100) > 50 ? -1 : 1;
            }
            else
            {
                yDir = yDist > 0 ? 1 : -1;
            }
        }
        x += xDir;
        y += yDir;
        
        DrunkenWalkFromPosToCenter(x, y);
    }

    int DistanceFromCenter(int pos, bool xAxis)
    {
        return (xAxis) ? grid.GetLength(0)/2 - pos : grid.GetLength(1)/2 - pos;
    }

    void CreatePathFindingNodes()
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                if(grid[x,y] == "O" && x%3 == 0 && y% 3 == 0)
                {
                    room.pathFindingNodes.Add(new PathFindingNode(new Vector2(transform.position.x + x * tileSize, transform.position.y + y * tileSize)));
                }
            }
        }
    }

    override protected bool IsInRange(IntPair pair)
    {
        if (pair.x > sizeX - 1 || pair.x < 0)
        {
            return false;
        }
        if (pair.y > sizeY - 1 || pair.y < 0)
        {
            return false;
        }
        return true;
    }

    void ClearExtraWalls()
    {
        List<int> xList = new List<int>();
        List<int> yList = new List<int>();
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                int neighbourWallTiles = GetSurroundingWallCount(x, y);

                if (neighbourWallTiles > 7)
                    grid[x, y] = "I";
                if (grid[x, y] == "O" )
                {
                    blankTiles++;
                    xList.Add(x);
                    yList.Add(y);
                }

            }
        }
        xList.Sort();
        yList.Sort();
        medianXIndex = xList[xList.Count / 2];
        medianYIndex = yList[yList.Count / 2];
        width = xList[xList.Count - 1] - xList[0];
        height = yList[yList.Count - 1] - yList[0];
    }
}

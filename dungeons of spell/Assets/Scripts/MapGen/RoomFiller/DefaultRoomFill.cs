using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DefaultRoomFill : StartRoomFiller
{
    
    public DefaultRoomFill(Vector2 pos, int x, int y, Room r, List<Vector2> ds) : base(pos,x,y,r,ds) {

    }

    public override Room FillRoom()
    {
        GenerateMaze();
        RoomGenerator generator = new RoomGenerator(transform.position, grid, room);

        MakeDoors(false);

        FillEnemy();
        return room;
    }

    const int distance = 4;


    bool IsAWall(IntPair pair)
    {
        if (IsInRange(pair) && (grid[pair.x, pair.y] == "X" || grid[pair.x, pair.y] == "R"))
            return true;
        return false;
    }

    bool IsDeadEnd(IntPair pair)
    {
        IntPair up = new IntPair(pair.x, pair.y + distance);
        IntPair down = new IntPair(pair.x, pair.y - distance);
        IntPair left = new IntPair(pair.x - distance, pair.y);
        IntPair right = new IntPair(pair.x + distance, pair.y);
        bool d = (IsAWall(up) || IsAWall(down) || IsAWall(left) || IsAWall(right));

        return (!d);
    }

    public void GenerateMaze()
    {
        // Ad pathfinding nodes at the corners
        room.pathFindingNodes.Add(new PathFindingNode(new Vector2(transform.position.x + 2 * tileSize, transform.position.y + 2 * tileSize)));
        room.pathFindingNodes.Add(new PathFindingNode(new Vector2(transform.position.x + (sizeX - 3) * tileSize, transform.position.y + 2 * tileSize)));
        room.pathFindingNodes.Add(new PathFindingNode(new Vector2(transform.position.x + 2 * tileSize, transform.position.y + (sizeY - 3) * tileSize)));
        room.pathFindingNodes.Add(new PathFindingNode(new Vector2(transform.position.x + (sizeX - 3) * tileSize, transform.position.y + (sizeY - 3) * tileSize)));

		room.pathFindingNodes.Add(new PathFindingNode(new Vector2(transform.position.x + ((sizeX) * tileSize)/2, transform.position.y + 2 * tileSize)));
		room.pathFindingNodes.Add(new PathFindingNode(new Vector2(transform.position.x + 2 * tileSize, transform.position.y + ((sizeY) * tileSize)/2)));
		//room.pathFindingNodes.Add(new PathFindingNode(new Vector2(transform.position.x + ((sizeX - 3) * tileSize)/2, transform.position.y + (sizeY - 3) * tileSize)));
		//room.pathFindingNodes.Add(new PathFindingNode(new Vector2(transform.position.x + (sizeX - 3) * tileSize, transform.position.y + ((sizeY - 3) * tileSize)/2)));
        for (int i = 2; i < grid.GetLength(0) - 2; i++)
        {
            for (int j = 2; j < grid.GetLength(1) - 2; j++)
            {
                if(grid[i, j] == "O")
                    grid[i, j] = "X";
            }
        }

        Stack stack = new Stack();
        IntPair start = new IntPair(sizeX / 2, sizeY / 2);
        if (start.x % 2 == 0)
        {
            start.x = start.x - 1;
        }
        if (start.y % 2 == 0)
        {
            start.y = start.y - 1;
        }
        Random.Range(0, 4);
        stack.Push(start);
        while (stack.Count > 0)
        {
            IntPair currentTile = (IntPair)stack.Peek();
            
            if(grid[currentTile.x, currentTile.y] != "O")
            {
                grid[currentTile.x, currentTile.y] = "O";
                room.pathFindingNodes.Add(new PathFindingNode(new Vector2(transform.position.x + currentTile.x * tileSize, transform.position.y + currentTile.y * tileSize)));
            }
            
            if (IsDeadEnd(currentTile))
            {
                stack.Pop();
                continue;
            }
            IntPair newTile = new IntPair(currentTile.x, currentTile.y);
            int direction = 0;
            while (true)
            {
                newTile.x = currentTile.x;
                newTile.y = currentTile.y;
                // Get a direction
                // 0 = left, 1 = up, 2 = right, distance = down
                direction = Random.Range(0, 4);

                if (direction == 0)
                    newTile.x = currentTile.x - distance;
                else if (direction == 1)
                    newTile.y = currentTile.y + distance;
                else if (direction == 2)
                    newTile.x = currentTile.x + distance;
                else if (direction == 3)
                    newTile.y = currentTile.y - distance;

                if (IsAWall(newTile))
                {
                    break;
                }
            }

            if (direction == 0)
            {
                for (int i = 1; i < distance; i++)
                {
                    grid[currentTile.x - i, currentTile.y] = "O";
                }
            }
            else if (direction == 1)
            {
                for (int i = 1; i < distance; i++)
                {
                    grid[currentTile.x, currentTile.y + i] = "O";
                }
            }
            else if (direction == 2)
            {
                for (int i = 1; i < distance; i++)
                {
                    grid[currentTile.x + i, currentTile.y] = "O";
                }
            }
            else if (direction == 3)
            {
                for (int i = 1; i < distance; i++)
                {
                    grid[currentTile.x, currentTile.y - i] = "O";
                }
            }
            stack.Push(newTile);

        }

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j] == "O")
                {
                    IntPair up = new IntPair(i,j + 1);
                    if(IsInRange(up) && IsAWall(up))
                    {
                        if(!(up.x == 0 || up.x == grid.GetLength(0) - 1))
                        {
                            if (!(up.y == 0 || up.y == grid.GetLength(1) - 1))
                            {
                                grid[up.x, up.y] = "R";
                            }
                        }  
                    }

                    IntPair down = new IntPair(i, j - 1);
                    if (IsInRange(down) && IsAWall(down))
                    {
                        if (!(down.x == 0 || down.x == grid.GetLength(0) - 1))
                        {
                            if (!(down.y == 0 || down.y == grid.GetLength(1) - 1))
                            {
                                grid[down.x, down.y] = "R";
                            }
                        }
                    }
                    IntPair left = new IntPair(i - 1, j);
                    if (IsInRange(left) && IsAWall(left))
                    {
                        if (!(left.x == 0 || left.x == grid.GetLength(0) - 1))
                        {
                            if (!(left.y == 0 || left.y == grid.GetLength(1) - 1))
                            {
                                grid[left.x, left.y] = "R";
                            }
                        }
                    }
                    IntPair right = new IntPair(i + 1, j);
                    if (IsInRange(right) && IsAWall(right))
                    {
                        if (!(right.x == 0 || right.x == grid.GetLength(0) - 1))
                        {
                            if (!(right.y == 0 || right.y == grid.GetLength(1) - 1))
                            {
                                grid[right.x, right.y] = "R";
                            }
                        }
                    }
                }
            }
        }
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {

                if (grid[i, j] == "X")
                {
                    MakeWall(i, j);
                }
                if (grid[i, j] == "O" && (i == 0 || j == 0 || j == grid.GetLength(1) - 1 || i == grid.GetLength(0) - 1))
                {
                    grid[i, j] = "X";
                }

            }
        }
    }

    void MakeWall(int i, int j)
    {
        if (i == 0 || j == 0 || i == sizeX - 1 || j == sizeY - 1)
        {
            grid[i, j] = "X";
            return;
        }
        if (i == 1 || j == 1 || i == sizeX - 2 || j == sizeY - 2)
        {
            grid[i, j] = "O";
            return;
        }

        const int holeChance = 10;
        const int destructableChance = 10;
        const int chestChance = 1;
        int randomNumber = Random.Range(0, 100);
        if(randomNumber <= chestChance)
        {
            grid[i, j] = "C";
        }
        else if(randomNumber < holeChance + destructableChance && randomNumber > holeChance)
        {
            if (grid[i - 1, j] == "X")
            {
                grid[i - 1, j] = "D";
                grid[i, j] = "D";
                return;
            }
            if (grid[i, j - 1] == "X")
            {
                grid[i, j - 1] = "D";
                grid[i, j] = "D";
                return;
            }
            if (grid[i, j + 1] == "X")
            {
                grid[i, j + 1] = "D";
                grid[i, j] = "D";
                return;
            }
            if (grid[i + 1, j] == "X")
            {
                grid[i + 1, j] = "D";
                grid[i, j] = "D";
                return;
            }
            grid[i, j] = "X";

        }
        else if (randomNumber <= holeChance)
        {
            room.pathFindingNodes.Add(new PathFindingNode(new Vector2(transform.position.x + i * tileSize, transform.position.y + j * tileSize)));
            grid[i, j] = "O";
            grid[i + 1, j] = "O";
            grid[i, j + 1] = "O";
            grid[i - 1, j] = "O";
            grid[i, j - 1] = "O";
        }
        else
        {
            grid[i, j] = "X";
        }

    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GraveYardRoom : CelluarRoomFiller
{

    float stoneChance = 40;
    public GraveYardRoom(Vector2 pos, int x, int y, Room r, List<Vector2> ds) : base(pos,x,y,r,ds) {

    }

    float  ManhatDistance(int x1, int y1, int x2, int y2)
    {
        return (Mathf.Abs(x1 - x2) + Mathf.Abs(y1 - y2));
    }

    private void MakeTombstones()
    {
        
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                float distance = (1 - ((ManhatDistance(medianXIndex, medianYIndex, i, j) / (float)(medianXIndex + medianYIndex))));
                float thisChance = stoneChance * Mathf.Pow(distance,10);
                if (grid[i, j] == "O" && Random.Range(0, 100) < thisChance && GetSurroundingTomb(i, j) == 0)
                {

                    grid[i, j] = "T";
                }
            }
        }
    }

    int GetSurroundingTomb(int gridX, int gridY)
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

                        if (grid[neighbourX, neighbourY] == "T")
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
        MakeTombstones();
        RoomGenerator generator = new RoomGenerator(transform.position, grid, room, true);

        MakeDoors(false);

        FillEnemy();
        return room;
    }
}

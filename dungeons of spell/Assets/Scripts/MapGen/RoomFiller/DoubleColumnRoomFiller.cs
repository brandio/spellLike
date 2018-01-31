using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DoubleColumnRoomFiller : CelluarRoomFiller
{

    public DoubleColumnRoomFiller(Vector2 pos, int x, int y, Room r, List<Vector2> ds) : base(pos,x,y,r,ds) {

    }

    public override Room FillRoom()
    {
        Ceullar();
        MakeColumns();
        RoomGenerator generator = new RoomGenerator(transform.position, grid, room, true);

        MakeDoors(false);

        FillEnemy();
        return room;
    }

    public void MakeColumns()
    {
        if (height > width)
        {
            for (int i = 0; i < height / 4; i++)
            {
                if(width > 23)
                {
                    grid[medianXIndex - 3, medianYIndex - height / 8 + i] = "X";
                    grid[medianXIndex - 2, medianYIndex - height / 8 + i] = "X";

                    grid[medianXIndex + 1, medianYIndex - height / 8 + i] = "X";
                    grid[medianXIndex + 2, medianYIndex - height / 8 + i] = "X";
                }
                else
                {
                    grid[medianXIndex - 2, medianYIndex - height / 8 + i] = "X";
                    grid[medianXIndex + 1, medianYIndex - height / 8 + i] = "X";
                }

            }
        }
        else
        {
            for (int i = 0; i < width / 4; i++)
            {
                if (height > 23)
                {
                    grid[medianXIndex - width / 8 + i, medianYIndex - 3] = "X";
                    grid[medianXIndex - width / 8 + i, medianYIndex - 2] = "X";

                    grid[medianXIndex - width / 8 + i, medianYIndex + 1] = "X";
                    grid[medianXIndex - width / 8 + i, medianYIndex + 2] = "X";
                }
                else
                {
                    grid[medianXIndex - width / 8 + i, medianYIndex - 2] = "X";
                    grid[medianXIndex - width / 8 + i, medianYIndex + 1] = "X";
                }

            }
        }

    }
}

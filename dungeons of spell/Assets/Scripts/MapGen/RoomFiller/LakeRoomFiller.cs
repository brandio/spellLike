using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class LakeRoomFiller : CelluarRoomFiller {
    public LakeRoomFiller(Vector2 pos, int x, int y, Room r, List<Vector2> ds) : base(pos,x,y,r,ds) {

    }

    public override Room FillRoom()
    {
        Ceullar();
        float area = width * height;
        int lakeRadius = ((int)Mathf.Floor(Mathf.Sqrt(area))) / 4;
        MakeLake(lakeRadius);
        RoomGenerator generator = new RoomGenerator(transform.position, grid, room, true);

        MakeDoors(false);

        FillEnemy();
        return room;
    }

    public void MakeLake(int lakeRadius)
    {
        if(lakeRadius <= 2)
        {
            return;
        }
        List<IntPair> intPairs = new List<IntPair>();

        for (int x = medianXIndex - lakeRadius; x < medianXIndex + lakeRadius; x++)
        {
            for (int y = medianYIndex - lakeRadius; y < medianYIndex + lakeRadius; y++)
            {
                if(grid[x,y] == "X")
                {
                    MakeLake(lakeRadius - 2);
                    return;
                }
                else
                {
                    intPairs.Add(new IntPair(x, y));
                }
                
            }
        }
        foreach(IntPair pair in intPairs)
        {
            grid[pair.x, pair.y] = "M";
        }
    }
}


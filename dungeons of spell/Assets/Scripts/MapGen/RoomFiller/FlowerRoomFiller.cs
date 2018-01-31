using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FlowerRoomFiller : CelluarRoomFiller
{
    const float Shroom_Per_Blank_Tile = .03f;
    public FlowerRoomFiller(Vector2 pos, int x, int y, Room r, List<Vector2> ds) : base(pos, x, y,r,ds)
    {

    }

    public override Room FillRoom()
    {
        Ceullar();
        RoomGenerator generator = new RoomGenerator(transform.position, grid, room, true,25);

        MakeDoors(false);

        FillEnemy();
        return room;
    }

}

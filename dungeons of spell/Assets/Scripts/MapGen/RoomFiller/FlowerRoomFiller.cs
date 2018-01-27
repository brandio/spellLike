using UnityEngine;
using System.Collections;

public class FlowerRoomFiller : CelluarRoomFiller
{
    const float Shroom_Per_Blank_Tile = .03f;
    public FlowerRoomFiller(Vector2 pos, int x, int y) : base(pos, x, y)
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

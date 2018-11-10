using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BossOneFiller : CelluarRoomFiller
{
    public BossOneFiller(Vector2 pos, int x, int y, Room r, List<Vector2> ds) : base(pos,x,y,r,ds) 
    {

    }

    public override Room FillRoom()
    {
        room.bossRoom = true;
        room.center = new Vector3(room.transform.position.x + tileSize * (sizeX/2), room.transform.position.y + tileSize * (sizeY / 2),0);
        randomFillChance = 93;
        Ceullar();
        RoomGenerator generator = new RoomGenerator(transform.position, grid, room, true);
        MakeDoors(false);
        FillEnemy(EnemyFillerFactory.EnemyType.Boss);
        return room;
    }
}

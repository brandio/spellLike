using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PortalRoomFiller : StartRoomFiller
{
    public PortalRoomFiller destinationRoom;
    public Portal mPortal;
    const float chestChance = 3;
    public PortalRoomFiller(Vector2 pos, int x, int y, Room r, List<Vector2> ds) : base(pos, x, y, r, ds)
    {

    }

    public override Room FillRoom()
    {
        MakeBorder();
        MakePortal();
        RoomGenerator generator = new RoomGenerator(transform.position, grid, room);

        MakeDoors(false);

        return room;
    }

    public void MakePortal()
    {
        int xMiddle = sizeX / 2;
        int yMiddle = sizeY / 2;
        GameObject portal = GameObject.Instantiate(Resources.Load("RoomStuff" + System.IO.Path.DirectorySeparatorChar.ToString() + "Portal") as GameObject);
        portal.transform.parent = transform;
        portal.transform.localPosition = new Vector3(xMiddle * tileSize, yMiddle * tileSize, 0);
        Portal portalComp = portal.GetComponent<Portal>();
        portalComp.homeRoom = room;
        mPortal = portalComp;
        if(destinationRoom.mPortal != null)
        {
            destinationRoom.mPortal.destinationPortal = mPortal;
            mPortal.destinationPortal = destinationRoom.mPortal;
        }
    }
}

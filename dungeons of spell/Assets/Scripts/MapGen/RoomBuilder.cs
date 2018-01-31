using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomBuilder  {

    public List<Vector2> doors;
    public Transform transform;
    public int depth;
    public Room room;
    public int sizeX;
    public int sizeY;
    public Vector2 position;
    IRoomFiller roomFiller;
    public bool hasRoomFiller;
    public RoomBuilder(int x, int y, Vector2 pos, int d)
    {
        GameObject newRoom = new GameObject();
        doors = new List<Vector2>();
        room = newRoom.AddComponent<Room>() as Room;
        transform = room.transform;
        transform.position = pos;
        sizeX = x;
        sizeY = y;
        depth = d;
        position = pos;
        hasRoomFiller = false;
        if(depth==0)
        {
            hasRoomFiller = true;
            roomFiller = new StartRoomFiller(pos, sizeX, sizeY, room, doors);
        }
    }

    public void AddDoor(Vector2 pos)
    {
        doors.Add(pos);
        if(hasRoomFiller)
        {
            roomFiller.AddDoor(pos);
        }
    }

    public void Clear()
    {
        doors = new List<Vector2>();
        GameObject.Destroy(room.gameObject);
        room.gameObject.SetActive(false);
    }

    public void AddRoomFiller(IRoomFiller filler)
    {
        if(hasRoomFiller)
        {
            Debug.LogError("Already have a room type");
        }
        hasRoomFiller = true;
        roomFiller = filler;
    }
    public Room RoomFiller()
    {
        //IRoomFiller filler;
        //if (depth == 0)
        //{
        //    filler = new StartRoomFiller(position, sizeX, sizeY,room,doors );
        //}
        //else
        //{
        //    filler = new MiddleBlockRoom(position, sizeX, sizeY, room, doors);
        //}
        return roomFiller.FillRoom();
    }

    public GameObject MakeFloor()
    {
        // Create the floor
        GameObject floor = GameObject.Instantiate(Resources.Load("Floor") as GameObject) as GameObject;
        floor.transform.parent = transform;
        floor.transform.localScale = new Vector3(sizeX * 2, sizeY * 2);
        floor.transform.localPosition = new Vector3(sizeX - 1, sizeY - 1);
        room.floor = floor;
        return floor;
    }
}

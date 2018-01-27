using UnityEngine;
using System.Collections;

public class TestRoomFiller : StartRoomFiller
{
    const int size = 20;
    string[,] myGrid;
    public TestRoomFiller(Vector2 pos, int x, int y) : base(pos,x,y) {

        myGrid = new string[size, size]
        {
            {"X","X","X","X","X","X","X","X","X","X","X","X","X","X","X","X","X","X","X","X"},
            {"X","O","O","O","O","X","O","O","O","O","O","O","O","O","O","O","O","O","O","X"},
            {"X","O","O","O","O","X","O","O","O","O","O","O","O","O","O","O","O","O","O","X"},
            {"X","O","O","O","O","X","O","O","O","O","O","O","O","O","O","O","O","O","O","X"},
            {"X","O","O","O","O","X","O","O","O","O","O","O","O","O","O","O","O","O","O","X"},
            {"X","O","O","O","O","O","O","O","O","O","O","O","O","O","O","O","O","O","O","X"},
            {"X","O","O","O","O","O","O","O","O","O","O","O","O","O","O","O","O","O","O","X"},
            {"X","O","X","X","X","O","O","O","X","X","O","O","O","O","O","O","O","O","O","X"},
            {"X","O","X","M","M","O","O","M","M","X","X","X","O","O","O","O","O","O","O","X"},
            {"X","O","O","M","M","X","X","M","M","O","X","M","M","M","M","M","O","O","O","X"},
            {"X","O","O","O","X","X","X","X","O","O","O","M","M","M","M","M","O","O","O","X"},
            {"X","O","O","O","O","O","O","O","O","X","X","M","M","M","M","M","O","O","O","X"},
            {"X","O","O","O","O","X","O","O","O","X","X","M","M","M","M","M","O","O","O","X"},
            {"X","O","O","O","O","X","O","O","O","O","O","M","M","M","M","M","X","O","O","X"},
            {"X","O","O","O","O","X","O","O","O","O","O","O","O","O","O","X","X","O","O","X"},
            {"X","O","O","O","O","X","O","O","O","O","O","O","O","O","O","O","O","O","O","X"},
            {"X","O","O","O","O","X","O","O","O","O","O","O","O","O","O","O","O","O","O","X"},
            {"X","O","O","O","O","X","O","O","O","O","O","O","O","O","O","O","O","O","O","X"},
            {"X","O","O","O","O","X","O","O","O","O","O","O","O","O","O","O","O","O","O","X"},
            {"X","X","X","X","X","X","X","X","X","X","X","X","X","X","X","X","X","X","X","X"}
        };
    }

    public override Room FillRoom()
    {
        AddDoor(new Vector2(1, 1));
        MakeDoors(false);
        for(int x = 4; x < size * 2; x = x + 5)
        {
            for (int y = 4; y < size * 2; y = y + 5)
            {
                room.pathFindingNodes.Add(new PathFindingNode(new Vector2(x,y)));
            }
        }
        //LakeDrawer lakeDraw = new LakeDrawer(2,new Vector2(20, 10));
        RoomGenerator roomGen = new RoomGenerator(transform.position, myGrid, room);
        FillEnemy();
        return room;
    }


    // Update is called once per frame
    void Update () {
	
	}
}

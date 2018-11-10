using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class StoreRoomFiller : CelluarRoomFiller {

	public StoreRoomFiller(Vector2 pos, int x, int y, Room r, List<Vector2> ds) : base(pos,x,y,r,ds) {}

	public override Room FillRoom()
	{
        randomFillChance = 90;
        Ceullar();
        MakeStoreSpotsAndDestructables();
        RoomGenerator generator = new RoomGenerator(transform.position, grid, room, true);
        MakeDoors(false);
        return room;
    }
	

	public void MakeStoreSpotsAndDestructables()
	{
		for (int i = 3; i < grid.GetLength(0) - 3; i++)
		{
			for (int j = 3; j < grid.GetLength(1) - 3; j++)
			{
                if(j == (sizeY / 2) + 2 && i == (sizeX / 2) + 1)
                {
                    grid[i, j] = "ST";
                }
				if(j == sizeY/2 && i % 3 == 0)
				{
					grid[i,j] = "S";
				}
				//else if(Random.Range (0,100) < 5)
				//{
				//	grid[i,j] = "D";
				//}
			}
		}
	}
}

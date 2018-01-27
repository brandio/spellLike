using UnityEngine;
using System.Collections;

public class StoreRoomFiller : StartRoomFiller {

	public StoreRoomFiller(Vector2 pos, int x, int y) : base(pos,x,y) {}

	public override Room FillRoom()
	{
		MakeBorder ();
		MakeStoreSpotsAndDestructables ();
		MakeDoors(false);
		RoomGenerator generator = new RoomGenerator(transform.position, grid, room);
		return room;
	}
	

	public void MakeStoreSpotsAndDestructables()
	{
		for (int i = 3; i < grid.GetLength(0) - 3; i++)
		{
			for (int j = 3; j < grid.GetLength(1) - 3; j++)
			{
				if(j == sizeY/2 && i % 3 == 0)
				{
					grid[i,j] = "S";
				}
				else if(Random.Range (0,100) < 5)
				{
					grid[i,j] = "D";
				}
			}
		}
	}
}

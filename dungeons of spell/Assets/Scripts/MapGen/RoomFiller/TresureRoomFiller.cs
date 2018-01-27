using UnityEngine;
using System.Collections;

public class TresureRoomFiller : StartRoomFiller
{
    const float chestChance = 3;
    public TresureRoomFiller(Vector2 pos, int x, int y) : base(pos,x,y) {

    }

    public override Room FillRoom()
    {
        MakeBorder();
        MakeChest();
        RoomGenerator generator = new RoomGenerator(transform.position, grid, room);

        MakeDoors(false);

        return room;
    }

    public void MakeChest()
    {
        int xMiddle = sizeX / 2;
        int yMiddle = sizeY / 2;
        grid[xMiddle, yMiddle] = "C";
        for(int x = 0; x < grid.GetLength(0); x++)
        {
            for(int y = 0; y < grid.GetLength(1); y++)
            {
                int distance = Mathf.Abs(xMiddle - x) + Mathf.Abs(yMiddle - y);
                distance *= distance;
                if (Random.Range(0,20) > distance)
                {
                    int amt = Random.Range(1, 3);
                    for (int i = 0; i < amt; i++)
                    {
                        GameObject coin = OrbPool.instance.CheckOutOrb();
                        coin.transform.position = new Vector2(x * tileSize + transform.position.x + Random.Range(-1.1f, 1.1f), y * tileSize + transform.position.y + Random.Range(-1.1f, 1.1f));
                    }
                }
            }
        }
    }
}

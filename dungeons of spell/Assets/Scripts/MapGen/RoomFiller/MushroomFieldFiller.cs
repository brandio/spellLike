using UnityEngine;
using System.Collections;

public class MushroomFieldFiller : CelluarRoomFiller
{
    const float Shroom_Per_Blank_Tile = .04f;
    public MushroomFieldFiller(Vector2 pos, int x, int y) : base(pos, x, y)
    {

    }

    public override Room FillRoom()
    {
        Ceullar();
        MakeShrooms();
        RoomGenerator generator = new RoomGenerator(transform.position, grid, room, true);

        MakeDoors(false);
        
        FillEnemy();
        return room;
    }

    public void MakeShrooms()
    {
        bool full = false;
        for(int i = 0; i < blankTiles * Shroom_Per_Blank_Tile; i++)
        {
            if(full)
            {
                break;
            }
            bool mushroomDown = false;
            int ii = 0;
            while (!mushroomDown && ii < 120)
            {
                ii++;
                int x = Random.Range(0, sizeX);
                int y = Random.Range(0, sizeY);
                if(grid[x,y] == "O")
                {

                    grid[x, y] = "MS";
                    mushroomDown = true;
                }
            }
            if(ii > 120)
            {
                full = true;
            }
        }
    }
}

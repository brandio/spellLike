using System;
using UnityEngine;

public class DepthBasedRoomFactory : IRoomFillerFactory
{
	public IRoomFiller MakeRoomFiller(int sizeX, int sizeY, Vector2 pos, int depth)
    {
        
        IRoomFiller filler = null;
		if (depth == 0)
		{
			return new StartRoomFiller(pos, sizeX, sizeY);
		}
        else if (sizeX < 16 && sizeY < 16)
        {
            return new TresureRoomFiller(pos, sizeX, sizeY);
        }
		else if (false)//sizeX > 15 && sizeY < 15)
        {
            return new StoreRoomFiller(pos, sizeX, sizeY);
        }
        else if (sizeX < 20 && sizeY < 20)
        {
            int number = UnityEngine.Random.Range(0, 100);
            if(number < 33)
            {
                return new MushroomFieldFiller(pos, sizeX, sizeY);
            }
            else if(number < 66)
            {
                return new FlowerRoomFiller(pos, sizeX, sizeY);
            }
        	return new CelluarRoomFiller(pos, sizeX, sizeY);
        }
        else if (sizeX < 23 && sizeY < 23)
        {
            int number = UnityEngine.Random.Range(0, 100);
            if (number < 45)
            {
                return new MushroomFieldFiller(pos, sizeX, sizeY);
            }
            else if (number < 66)
            {
                return new FlowerRoomFiller(pos, sizeX, sizeY);
            }
            return new DoubleColumnRoomFiller(pos, sizeX, sizeY);
        }
        else
        {
            int number = UnityEngine.Random.Range(0, 100);
            if (number < 15)
            {
                return new MushroomFieldFiller(pos, sizeX, sizeY);
            }
            else if (number < 24)
            {
                return new FlowerRoomFiller(pos, sizeX, sizeY);
            }

            if (UnityEngine.Random.Range(0,100) < 40)
            {
                return new LakeRoomFiller(pos, sizeX, sizeY);
            }
            else
            {
                return new DoubleColumnRoomFiller(pos, sizeX, sizeY);
            }
            
        }
		return filler;
	}
}

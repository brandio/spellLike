using System;
using UnityEngine;
public interface IRoomFillerFactory
{
    IRoomFiller MakeRoomFiller(int sizeX, int sizeY, Vector2 pos, int depth);
}

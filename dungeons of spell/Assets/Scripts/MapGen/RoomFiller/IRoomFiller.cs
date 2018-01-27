using System;
using UnityEngine;
public interface IRoomFiller
{
	GameObject MakeFloor();

	Room FillRoom();

	void AddDoor(Vector2 pos);

	void Clear();
}

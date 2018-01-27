using UnityEngine;
using System.Collections;

public class RoomCord {
	public float tileSize = 2;

	public int xPos;
	public int yPos;

	public RoomCord(int x, int y, float tileS)
	{
		tileSize = tileS;
		xPos = x;
		yPos = y;
	}

	public RoomCord(int x, int y)
	{
		xPos = x;
		yPos = y;
	}

	public void SpawnObject(GameObject objectToSpawn, Transform t)
	{
		GameObject gameObject = GameObject.Instantiate (objectToSpawn, Vector2.zero, Quaternion.identity) as GameObject;
		gameObject.transform.parent = t;
		gameObject.transform.localPosition = GetLocalPos ();

	}

	public RoomCord(Vector2 localPos)
	{
		xPos = (int)(localPos.x / tileSize);
		yPos = (int)(localPos.y / tileSize);
	}

	public Vector2 GetLocalPos()
	{
		float offset = ((float)tileSize - 4) / (float)2;
		return new Vector2(xPos * tileSize, yPos * tileSize) + new Vector2(offset,offset); 
	}

	public static RoomCord operator+(RoomCord a, RoomCord b)
	{
		if (a.tileSize != b.tileSize) {
			Debug.LogError("Adding Room cords with different tile sizes");
		}
		return new RoomCord(a.xPos + b.xPos, a.yPos + b.yPos); 
	}
	public static RoomCord operator*(int a, RoomCord b)
	{
		return new RoomCord(a * b.xPos, a * b.yPos); 
	}
	public static bool operator==(RoomCord a, RoomCord b)
	{
		if (a.tileSize != b.tileSize) {
			Debug.Log ("Mismatch tile size");
			return false;
		}
		return(a.xPos == b.xPos && a.yPos == b.yPos);
	}
	public static bool operator!=(RoomCord a, RoomCord b)
	{

		return(!(a == b));
	}
}

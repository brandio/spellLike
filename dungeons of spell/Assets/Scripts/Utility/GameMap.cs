using UnityEngine;
using System.Collections;

public class GameMap : MonoBehaviour 
{
    public DungeonRoomMaker dungeon;
    public Camera cam;
    void SetBounds()
    {
        float xDiff = ((dungeon.topRight.x - dungeon.bottomLeft.x) / 2) * .5625f;
        float yDiff = (dungeon.topRight.y - dungeon.bottomLeft.y) / 2;
        Debug.Log("Dungeon Info");
        Debug.Log(dungeon.bottomLeft);
        Debug.Log(dungeon.topRight);
        Debug.Log(xDiff);
        Debug.Log(yDiff);
        cam.orthographicSize = Mathf.Max(xDiff,yDiff);
        this.transform.position = new Vector3((dungeon.topRight.x + dungeon.bottomLeft.x)/2, (dungeon.topRight.y + dungeon.bottomLeft.y) / 2, this.transform.position.z);
    }
    void Start()
    {
        dungeon.DungeonDone += SetBounds;
    }
	
	void Update () {
	
	}
}

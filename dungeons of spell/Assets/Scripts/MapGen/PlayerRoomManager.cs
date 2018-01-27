using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerRoomManager : MonoBehaviour {
	public static PlayerRoomManager instance;
    public List<Room> Rooms;
    public DungeonRoomMaker dungeonRoomMaker;

    const float maxStoresPerRoom = .1f;
    const float maxTreasurePerRoom = .1f;

    int maxDepth;
    Movement playerMov;
    List<IRoomChangeListener> roomChangeListeners = new List<IRoomChangeListener>();
    Room currentRoom;
    
    public void Register(IRoomChangeListener l)
	{
		roomChangeListeners.Add(l);
	}

	public void ChangeRoom(Room newRoom)
	{
        if(currentRoom == newRoom)
        {
            return;
        }
		currentRoom = newRoom;
        if(playerMov == null)
        {
            playerMov = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        }
        playerMov.room = newRoom;
        foreach (Room r in Rooms)
        {
            r.DeActivate();
        }

        newRoom.Activate(playerMov.transform);

		foreach(IRoomChangeListener listener in roomChangeListeners)
		{
			listener.RoomChanged(newRoom);
		}
        
	}
	
    public IRoomFiller MakeRoom(int sizeX, int sizeY, Vector2 pos, int depth)
    {
        DepthBasedRoomFactory fillerFactory = new DepthBasedRoomFactory();
        return fillerFactory.MakeRoomFiller(sizeX, sizeY, pos, depth);
    }

	void Start () {
		if (instance != null) {
			Debug.LogError("There can only be one PlayerRoomManager!");
			return;
		}
		instance = this;
	}
	

}

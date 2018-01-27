using UnityEngine;
using System.Collections;

public class RoomEnterance : MonoBehaviour {
	Room room;
	public bool locked;
    public void SetRoom(Room r)
    {
        room = r;
        //room.RoomCleared += roomClearedEventHandler;
        //room.RoomEntered += roomEnteredEventHandler;
    }

    void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			PlayerRoomManager.instance.ChangeRoom(room);

		}
	}
}

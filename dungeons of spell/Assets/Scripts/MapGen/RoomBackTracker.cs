using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RoomBackTracker : MonoBehaviour, IRoomChangeListener
{
    Stack<Room> roomStack = new Stack<Room>();
    void IRoomChangeListener.RoomChanged(Room newRoom)
    {
        roomStack.Push(newRoom);
    }
    Room lastRoom = null;
    void Start()
    {
        PlayerRoomManager.instance.Register(this);
    }
	// Update is called once per frame
	void Update () 
    {
	    if(Input.GetKeyUp(KeyCode.Space) && roomStack.Count > 1 && Controls.GetInstance().active)
        {
            if(PlayerRoomManager.instance.currentRoom.IsCleared())
            {
                roomStack.Pop();
                Room room = roomStack.Pop();
                lastRoom = PlayerRoomManager.instance.currentRoom;
                if (room.pathFindingNodes.Count > 0)
                {
                    this.transform.position = room.pathFindingNodes[UnityEngine.Random.Range(0, room.pathFindingNodes.Count)].position;
                }
                else
                {
                    this.transform.position = room.center;
                }
                PlayerRoomManager.instance.ChangeRoom(room);
            }
        }

        if(Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.LeftControl))
        {
            if(lastRoom != null)
            {
                Room room = lastRoom;
                lastRoom = null;
                if (room.pathFindingNodes.Count > 0)
                {
                    this.transform.position = room.pathFindingNodes[UnityEngine.Random.Range(0, room.pathFindingNodes.Count)].position;
                }
                else
                {
                    this.transform.position = room.center;
                }
                PlayerRoomManager.instance.ChangeRoom(room);
            }
        }
	}
}

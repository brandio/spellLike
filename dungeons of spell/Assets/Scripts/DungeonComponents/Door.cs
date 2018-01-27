using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour, IPasswordActive {
    public SpriteRenderer doorSprite;
    public BoxCollider2D doorCollide;
	bool parentSet = false;
	public bool locked = false;
	public void Lock(Room r)
    {
        doorSprite.enabled = true;
        doorCollide.enabled = true;
		locked = true;
    }

	public void ActivatePassword()
	{
		UnLock (null);
	}

    public void UnLock(Room r)
    {
        doorSprite.enabled = false;
        doorCollide.enabled = false;
    }

	void Update()
	{
		if (transform.hasChanged && transform.parent != null && !parentSet) {
			parentSet = true;
			Room room = transform.parent.gameObject.GetComponent<Room> ();
			room.RoomCleared += UnLock;
			room.RoomEntered += Lock;
		}
	}
}

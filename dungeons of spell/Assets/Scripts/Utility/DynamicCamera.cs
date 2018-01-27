using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DynamicCamera : MonoBehaviour, IRoomChangeListener {
    Transform player;

    public List<Transform> trackedObjects;
	public float moveSpeed = 10;
	public float zoomSpeed = 2;

    // The average max distance we expect between all objects
    public float averageDist;

    // Max amount we will zoom out
	public float maxDistance;
	public float minDistance;

    // Maximum dist between two points
	float maxDist;

	float orthRatio;
	float sizeRatio;

	Camera myCamera;
	Vector3 position;

	public void RoomChanged(Room room)
	{
        foreach (GameObject obj in room.enemies)
        {
            trackedObjects.Add(obj.transform);
        }
    }

    // Use this for initialization
    void Start ()
    {
		PlayerRoomManager.instance.Register (this);
		position = transform.position;
		myCamera = gameObject.GetComponent<Camera> ();
		trackedObjects = new List<Transform> ();
		GetObjects ();
		GetMaxDistance ();
	}
    
	void GetObjects()
	{
        player = (GameObject.Find ("Player").transform);
	}

    public float MaxDistFromPlayer;

	void GetMaxDistance()
	{
        float myMaxDist = 0;
		for (int i = 0; i < trackedObjects.Count; i++) 
		{
            float playerDist = Vector2.Distance(player.position, trackedObjects[i].position);
            if (playerDist >= MaxDistFromPlayer)
            {
                continue;
            }
            if(playerDist > myMaxDist)
            {
                myMaxDist = playerDist;
            }
			for (int j = 0; j < trackedObjects.Count; j++) 
			{
				float dist = Vector2.Distance(trackedObjects[i].position,trackedObjects[j].position);
				if(dist > myMaxDist)
				{
                    myMaxDist = dist;
				}
			}
		}
        maxDist = myMaxDist;
	}

	public float playerWeight;

	void CalculatePosition()
	{
        List<Transform> toRemove = new List<Transform>();
		Vector3 center = Vector3.zero;
        float count = 0;
		foreach (Transform trackedObject in trackedObjects) {
            if (trackedObject.gameObject.activeSelf == false)
            {
                toRemove.Add(trackedObject);
                continue;
            }
            if(Vector2.Distance(player.position, trackedObject.position) > MaxDistFromPlayer)
            {
                continue;
            }
            count++;
			center += trackedObject.position;
		}
		for(int i = 0; i < playerWeight * count; i++)
		{
            center += player.position;
		}
        if(count > 0)
        {
            position = center / (count + playerWeight * count);
        }
        else
        {
            position = player.position;
        }
		
        foreach(Transform t in toRemove)
        {
            trackedObjects.Remove(t);
        }

		transform.position = Vector3.Lerp (transform.position, position, moveSpeed * Time.deltaTime);
		transform.position = new Vector3 (transform.position.x, transform.position.y, -10);
    }

    void CalculateZoom()
    {
        GetMaxDistance();
        float dist = maxDist / averageDist * 100;
        if(dist < minDistance)
        {
            dist = minDistance;

        }
        if(dist > maxDistance)
        {
            dist = maxDistance;
        }
        myCamera.orthographicSize = Mathf.Lerp(myCamera.orthographicSize,dist,zoomSpeed * Time.deltaTime);
    }

	void Update () {
		CalculatePosition ();
        CalculateZoom();
    }
}

using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CircleCollider2D))]
public class InRangeActive : MonoBehaviour {

	protected bool playerInside = false;
	protected GameObject player;
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			player = other.gameObject;
			playerInside = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			playerInside = false;
		}
	}

}

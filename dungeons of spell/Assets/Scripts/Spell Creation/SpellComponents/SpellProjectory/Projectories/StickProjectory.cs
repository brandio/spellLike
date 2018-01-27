using UnityEngine;
using System.Collections;

public class StickProjectory : IProjectory {
	string toolTip;
	public string GetToolTip()
	{
		return toolTip;
	}
	
	public void OnBackGroundCollision(GameObject projectile, MovementCheck movementCheck)
	{
		Debug.Log ("STICKY!!!");
		projectile.transform.parent.GetComponent<ProjectileMovement>().rotateNow = false;
		projectile.GetComponent<ProjectileCollision> ().collideWithBack = false;
		projectile.transform.parent.GetComponent<ProjectileMovement> ().tempSpeed = 0;
	}
}

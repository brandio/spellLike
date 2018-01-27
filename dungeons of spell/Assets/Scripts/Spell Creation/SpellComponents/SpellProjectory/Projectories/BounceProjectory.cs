using UnityEngine;
using System.Collections;

public class BounceProjectory : IProjectory {
	string toolTip;
	public string GetToolTip()
	{
		return toolTip;
	}
	
	public void OnBackGroundCollision(GameObject projectile, MovementCheck movementCheck)
	{
		projectile.transform.parent.GetComponent<ProjectileMovement>().rotateNow = false;
		Vector2 dir = Vector2.Reflect(projectile.transform.parent.position, movementCheck.hit2d.normal);
		projectile.transform.parent.eulerAngles = new Vector3(0,0,Mathf.Atan2(dir.x,dir.y) * Mathf.Rad2Deg);
	}
}

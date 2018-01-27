using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BasicProjectory  : IProjectory{

	public void OnBackGroundCollision(GameObject projectile, MovementCheck movementCheck)
	{
		projectile.SetActive (false);
	}
}

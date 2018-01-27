using UnityEngine;
using System.Collections;

public class GhostProjectory : IProjectory {
	string toolTip;
	public string GetToolTip()
	{
		return toolTip;
	}
	
	public void OnBackGroundCollision(GameObject projectile, MovementCheck movementCheck)
	{

	}
}

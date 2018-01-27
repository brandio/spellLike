using UnityEngine;
using System.Collections;

public interface IProjectory {
	void OnBackGroundCollision(GameObject projectile, MovementCheck movementCheck);
}

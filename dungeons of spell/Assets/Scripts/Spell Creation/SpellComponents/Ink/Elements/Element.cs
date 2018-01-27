using UnityEngine;
using System.Collections;

abstract public class Element {
	 protected int priority;

	public int GetPriority()
	{
		return priority;
	}

	virtual public float GetSpeedAdd()
	{
		return 1;
	}


	virtual public float GetMana()
	{
		return 1;
	}

	virtual public void PriorityWin(GameObject loser)
	{
	    loser.SetActive (false);
	}
	
	virtual public float Damage(GameObject hit, float mod)
	{
		return 1 * mod;
	}

	abstract public  Element NewOfSameType();

	abstract public Color GetColor();

}

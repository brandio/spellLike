using UnityEngine;
using System.Collections;

public class EarthElement : Element {

	public EarthElement()
	{
		priority = 3;
	}

	override public float GetSpeedAdd()
	{
		return 1.25f;
	}

	override public Element NewOfSameType()
	{
		return new EarthElement();
	}

	int count = 2;
	
	override public void PriorityWin(GameObject loser)
	{

		if (loser.activeSelf) {
			loser.SetActive (false);
			count = count - 1;
			if(count == 0)
			{
				priority = priority - 2;
			}
		}

	}
	
	override public Color GetColor()
	{
		return new Color32(152,90,49,255);
	}
}

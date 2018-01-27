using UnityEngine;
using System.Collections;

public class WindElement : Element {

	public WindElement()
	{
		priority = 2;
	}

	override public float GetSpeedAdd()
	{
		return .5f;
	}

	override public Element NewOfSameType()
	{
		return new WindElement();
	}
	
	override public Color GetColor()
	{
		return new Color32(177,219,236,255);
	}
}

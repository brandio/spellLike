using UnityEngine;
using System.Collections;

public class WaterElement : Element {

	public WaterElement()
	{
		priority = 2;
	}

	override public Element NewOfSameType()
	{
		return new WaterElement();
	}
	
	override public Color GetColor()
	{
		return new Color32(0,139,255,255);
	}
}

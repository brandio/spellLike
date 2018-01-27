using UnityEngine;
using System.Collections;

public class LightElement : Element {

	public LightElement()
	{
		priority = 2;
	}

	override public Element NewOfSameType()
	{
		return new LightElement();
	}
	
	override public Color GetColor()
	{
		return new Color32(245,255,222,255);
	}
}

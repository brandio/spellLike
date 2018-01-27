using UnityEngine;
using System.Collections;

public class DarkElement : Element {
	
	public DarkElement()
	{
		priority = 1;
	}

	override public float Damage(GameObject hit, float mod)
	{
		return 1.5f * mod;
	}

	override public Element NewOfSameType()
	{
		return new DarkElement();
	}
	
	override public Color GetColor()
	{
		return new Color32(49,14,65,255);
	}
}

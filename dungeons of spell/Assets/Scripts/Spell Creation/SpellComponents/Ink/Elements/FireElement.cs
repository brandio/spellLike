using UnityEngine;
using System.Collections;

public class FireElement : Element {

	public FireElement()
	{
		priority = 2;
	}

	override public float Damage(GameObject hit, float mod)
	{
		hit.GetComponent<Health> ().TakeDamageOverTime (.25f * mod, 3, .5f);
		return .5f * mod;

	}


	override public Element NewOfSameType()
	{
		return new FireElement();
	}
	
	override public Color GetColor()
	{
		return new Color32(255,142,0,255);
	}
}

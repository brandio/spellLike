using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PoisonGasElement : Element {
	
	public PoisonGasElement()
	{
		priority = 5;
	}

	override public Element NewOfSameType()
	{
		return new PoisonGasElement();
	}
	
	override public Color GetColor()
	{
		return new Color32(170,204,200,200);
	}

	override public void PriorityWin(GameObject loser)
	{	
	}

}

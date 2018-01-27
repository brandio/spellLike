using UnityEngine;
using System.Collections;

public class ElementUtility : MonoBehaviour {

	public enum Element_Type{Fire,Water,Wind,Earth,Light,Dark,PoisonGas};

	public static Element GetElement(ElementUtility.Element_Type type)
	{
		switch (type) 
		{
		case Element_Type.Fire:
			return new FireElement();
		case Element_Type.Water:
			return new WaterElement();
		case Element_Type.Earth:
			return new EarthElement();
		case Element_Type.Wind:
			return new WindElement();
		case Element_Type.Dark:
			return new DarkElement();
		case Element_Type.Light:
			return new LightElement();
		case Element_Type.PoisonGas:
			return new PoisonGasElement();
			
		}
		return null;
	}

	public static Color ElementTypeToColor(ElementUtility.Element_Type type)
	{
		switch (type) 
		{
		case Element_Type.Fire:
			return new Color32(255,142,0,255);
		case Element_Type.Water:
			return new Color32(0,139,255,255);
		case Element_Type.Earth:
			return new Color32(152,90,49,255);
		case Element_Type.Wind:
			return new Color32(177,219,236,255);
		case Element_Type.Dark:
			return new Color32(49,14,65,255);
		case Element_Type.Light:
			return new Color32(245,255,222,255);
		case Element_Type.PoisonGas:
			return new Color32(170,204,200,200);

		}
		return Color.white;
	}


}

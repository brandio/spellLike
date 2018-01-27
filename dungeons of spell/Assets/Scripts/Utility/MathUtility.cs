using UnityEngine;
using System.Collections;

public class MathUtility : MonoBehaviour {
	public static int roundUp(int numToRound, int multiple)
	{
		if (multiple == 0)
			return numToRound;
		
		int remainder = numToRound % multiple;
		if (remainder == 0)
			return numToRound;
		
		return numToRound + multiple - remainder;
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

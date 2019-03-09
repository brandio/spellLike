using UnityEngine;
using System.Collections;

public class Wait : SpellEvent {
	float amt;

    float min = -1;
    float max = -1;
	public Wait(float a, SpellCreationSegment segment)
	{
		seg = segment;
		amt = a;
	}

    public Wait(float a, float b, SpellCreationSegment segment)
    {
        seg = segment;
        min = a;
        max = b;
    }
    public override void AdjustForSpeed(SpellCreationSegment seg)
    {
        amt = amt / seg.GetSpeed();
        return;
        //rotationAmt = rotationAmt / seg.GetTurnSpeed();
    }

    public override float CallAndWait(ProjectileController projControl)
	{
        return min > 0 ? Random.Range(min,max) : amt;
	}

	public override string ToText()
	{
		return "Wait " + amt;
	}
}

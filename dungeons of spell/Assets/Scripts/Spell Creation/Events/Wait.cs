using UnityEngine;
using System.Collections;

public class Wait : SpellEvent {
	float amt;

	public Wait(float a, SpellCreationSegment segment)
	{
		seg = segment;
		amt = a;
	}

    public override void AdjustForSpeed(SpellCreationSegment seg)
    {
        amt = amt / seg.GetSpeed();
        return;
        //rotationAmt = rotationAmt / seg.GetTurnSpeed();
    }

    public override float CallAndWait(ProjectileController projControl)
	{
		return amt;
	}

	public override string ToText()
	{
		return "Wait " + amt;
	}
}

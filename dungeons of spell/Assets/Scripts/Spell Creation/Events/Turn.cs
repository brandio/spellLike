using UnityEngine;
using System.Collections;

public class Turn : SpellEvent {
	float rotationAmt;

	public Turn(float rotation, SpellCreationSegment segment)
	{
		seg = segment;
		rotationAmt = rotation;
	}

    public override void AdjustForSpeed(SpellCreationSegment seg)
    {
        return;
        //rotationAmt = rotationAmt / seg.GetTurnSpeed();
    }


    public override float CallAndWait(ProjectileController projControl)
	{
		projControl.turn (rotationAmt);
		return Mathf.Abs(rotationAmt/(seg.GetTurnSpeed())) ;
	}

	public override string ToText()
	{
		return "Rotate " + rotationAmt;
	}
}

using UnityEngine;
using System.Collections;

public class SpellMove : SpellEvent
{

    float moveAmt;

    public SpellMove(float dist, SpellCreationSegment segment)
    {
        moveAmt = dist;
        seg = segment;
    }

    public override void AdjustForSpeed(SpellCreationSegment seg)
    {
        return;
        
    }

    public override float CallAndWait(ProjectileController projControl)
    {
        projControl.transform.position = projControl.transform.position + projControl.transform.right * moveAmt;
        return 0;
    }

    public override string ToText()
    {
        return "Move " + moveAmt;
    }
}

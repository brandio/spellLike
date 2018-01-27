using UnityEngine;
using System.Collections;

public class SpeedChange : SpellEvent
{
    float mSpeed;
    public SpeedChange(float speed, SpellCreationSegment segment)
    {
        mSpeed = speed;
        seg = segment;
    }

    public override void AdjustForSpeed(SpellCreationSegment seg)
    {
        return;
    }

    public override float CallAndWait(ProjectileController projControl)
    {
        projControl.pm.tempSpeed = mSpeed;
        return 0;
    }

    public override string ToText()
    {
        return "Speed Change " + mSpeed;
    }
}

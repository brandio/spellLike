using UnityEngine;
using System.Collections;

public class SpellFaceEvent : SpellEvent
{
    Vector3 _dir;
    public SpellFaceEvent(Vector3 dir, SpellCreationSegment segment)
    {
        seg = segment;
        _dir = dir;
    }

    public override void AdjustForSpeed(SpellCreationSegment seg)
    {
        return;
        //rotationAmt = rotationAmt / seg.GetTurnSpeed();
    }


    public override float CallAndWait(ProjectileController projControl)
    {
        projControl.transform.eulerAngles = _dir;
        return 0;
    }

    public override string ToText()
    {
        return "face " + _dir;
    }
}

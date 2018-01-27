using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlowS : ISpellPattern {

    public List<SpellCreationSegment> CreateSpellSegs(ISpellGrid grid, float damageMod)
    {
        SpellCreationSegment seg = new SpellCreationSegment();
        seg.SetStartingRotation(0);
        seg.SetTurnSpeed(50);
        seg.SetSpeedMod(.6f);
        seg.AddEvent(new Turn(20, seg));
        seg.AddEvent(new Turn(-30, seg));
        seg.AddEvent(new Turn(20, seg));
        seg.AddEvent(new Turn(-20, seg));
        //seg.AddEvent(new Wait(1, seg));
        seg.AddEvent(new SpellEnd());


        List<SpellCreationSegment> segs = new List<SpellCreationSegment>();
        segs.Add(seg);
        return segs;
    }
}

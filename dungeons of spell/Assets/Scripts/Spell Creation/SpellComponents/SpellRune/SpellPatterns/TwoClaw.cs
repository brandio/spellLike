using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TwoClaw : ISpellPattern {
    public List<SpellCreationSegment> CreateSpellSegs(ISpellGrid grid, float damageMod)
    {
        SpellCreationSegment seg = new SpellCreationSegment();
        seg.SetStartingRotation(-10);
        seg.SetTurnSpeed(200);

        seg.AddEvent(new Wait(1, seg));
        seg.AddEvent(new SpellEnd());

        SpellCreationSegment seg1 = new SpellCreationSegment();
        seg1.SetStartingRotation(10);
        seg1.SetTurnSpeed(200);

        seg1.AddEvent(new Wait(1, seg));
        seg1.AddEvent(new SpellEnd());

        List<SpellCreationSegment> segs = new List<SpellCreationSegment>();
        segs.Add(seg);
        segs.Add(seg1);
        return segs;
    }
}

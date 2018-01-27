using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FrontBack : ISpellPattern {

    public List<SpellCreationSegment> CreateSpellSegs(ISpellGrid grid, float damageMod)
    {
        SpellCreationSegment seg = new SpellCreationSegment();
        seg.SetStartingRotation(0);
        seg.SetTurnSpeed(0);

        seg.AddEvent(new Wait(17, seg));
        seg.AddEvent(new SpellEnd());

        SpellCreationSegment seg1 = new SpellCreationSegment();
        seg1.SetStartingRotation(180);
        seg1.SetTurnSpeed(0);

        seg1.AddEvent(new Wait(17, seg));
        seg1.AddEvent(new SpellEnd());
        List<SpellCreationSegment> segs = new List<SpellCreationSegment>();
        segs.Add(seg);
        segs.Add(seg1);
        return segs;
    }
}

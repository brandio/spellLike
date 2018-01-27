using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ShortCone : ISpellPattern
{

    public List<SpellCreationSegment> CreateSpellSegs(ISpellGrid grid, float damageMod)
    {
        SpellCreationSegment seg = new SpellCreationSegment();
        seg.AddEvent(new Wait(.15f, seg));
        seg.AddEvent(new SpellEnd());

        SpellCreationSegment seg1 = new SpellCreationSegment();
        seg1.AddEvent(new Wait(.15f, seg));
        seg1.AddEvent(new SpellEnd());
        seg1.SetStartingRotation(-12);

        SpellCreationSegment seg2 = new SpellCreationSegment();
        seg2.AddEvent(new Wait(.15f, seg));
        seg2.AddEvent(new SpellEnd());
        seg2.SetStartingRotation(12);

        List<SpellCreationSegment> segs = new List<SpellCreationSegment>();
        segs.Add(seg);
        segs.Add(seg1);
        segs.Add(seg2);

        return segs;
    }
}

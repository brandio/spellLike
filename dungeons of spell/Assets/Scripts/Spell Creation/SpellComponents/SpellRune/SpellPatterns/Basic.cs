using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Basic : ISpellPattern
{

    public List<SpellCreationSegment> CreateSpellSegs(ISpellGrid grid, float damageMod)
    {
        SpellCreationSegment seg = new SpellCreationSegment();
        seg.AddEvent(new SpeedChange(.3f, seg));
        seg.AddEvent(new Wait(8, seg));
        seg.AddEvent(new SpellEnd());
        List<SpellCreationSegment> segs = new List<SpellCreationSegment>();
        segs.Add(seg);
        return segs;
    }
}

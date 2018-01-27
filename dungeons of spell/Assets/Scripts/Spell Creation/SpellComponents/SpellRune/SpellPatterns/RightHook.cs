using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RightHook : ISpellPattern
{

    public List<SpellCreationSegment> CreateSpellSegs(ISpellGrid grid, float damageMod)
    {
        SpellCreationSegment seg = new SpellCreationSegment();
        seg.SetStartingRotation(0);
        seg.SetTurnSpeed(200);

        seg.AddEvent(new Wait(8f, seg));
        seg.AddEvent(new Turn(-160, seg));
        seg.AddEvent(new Wait(9f, seg));
        seg.AddEvent(new SpellEnd());
        List<SpellCreationSegment> segs = new List<SpellCreationSegment>();
        segs.Add(seg);
        return segs;
    }
}

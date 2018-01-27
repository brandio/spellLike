using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Boomerang : ISpellPattern
{
    public List<SpellCreationSegment> CreateSpellSegs(ISpellGrid grid, float damageMod)
    {
        SpellCreationSegment seg = new SpellCreationSegment();
        seg.SetStartingRotation(0);
        seg.SetTurnSpeed(400);

        seg.AddEvent(new Wait(10, seg));
        seg.AddEvent(new Turn(200, seg));
        seg.AddEvent(new Wait(10, seg));
        seg.AddEvent(new SpellEnd());
        List<SpellCreationSegment> segs = new List<SpellCreationSegment>();
        segs.Add(seg);
        return segs;
    }
}

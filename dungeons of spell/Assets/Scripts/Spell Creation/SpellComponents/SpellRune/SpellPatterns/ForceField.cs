using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ForceField : ISpellPattern {

    public List<SpellCreationSegment> CreateSpellSegs(ISpellGrid grid, float damageMod)
    {
        List<SpellCreationSegment> segs = new List<SpellCreationSegment>();
        for (int i = 0; i < 4; i++)
        {
            SpellCreationSegment seg = new SpellCreationSegment();
            seg.SetStartingRotation(90 * i);
            seg.SetSpeedMod(0.1f);
            seg.AddEvent(new SpellMove(2, seg));
            seg.AddEvent(new Wait(2, seg));
            seg.AddEvent(new SpellEnd());
            segs.Add(seg);
        }   
        return segs;
    }
}

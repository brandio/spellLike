using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AlphaStar : ISpellPattern {
    public List<SpellCreationSegment> CreateSpellSegs(ISpellGrid grid, float damageMod)
    {
        List<SpellCreationSegment> segs = new List<SpellCreationSegment>();
        for (int i = 0; i < 7; i++)
        {
            SpellCreationSegment seg = new SpellCreationSegment();

            float sign = -1;
            if (51 * i > 180)
            {
                sign = 1;
            }

            seg.SetStartingRotation(51 * i);
            seg.AddEvent(new Turn(51 * i * sign, seg));
            seg.AddEvent(new Wait(20, seg));
            seg.AddEvent(new SpellEnd());
            seg.SetTurnSpeed(300);
            segs.Add(seg);
        }
        return segs;
    }
}

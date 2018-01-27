using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BlowFishPattern : ISpellPattern {

    public List<SpellCreationSegment> CreateSpellSegs(ISpellGrid grid, float damageMod)
	{
		List<SpellCreationSegment> segs = new List<SpellCreationSegment> ();
		for (int i = 0; i < 8; i++) {
			SpellCreationSegment seg = new SpellCreationSegment ();
            seg.SetSpeedMod(.5f);
			seg.SetStartingRotation(i * 45);
			seg.AddEvent(new Wait(7,seg));
			seg.AddEvent (new SpellEnd ());
			segs.Add (seg);
		}
		return segs;
	}
}

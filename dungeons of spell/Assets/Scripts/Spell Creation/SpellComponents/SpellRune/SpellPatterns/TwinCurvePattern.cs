using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TwinCurvePattern : ISpellPattern {

	public List<SpellCreationSegment> CreateSpellSegs(ISpellGrid grid, float damageMod)
	{
		SpellCreationSegment seg = new SpellCreationSegment ();
		seg.SetStartingRotation (-15);
		seg.SetTurnSpeed (100);

        seg.AddEvent(new Turn(40, seg));
        seg.AddEvent(new Wait(.5f, seg));
        seg.AddEvent(new Turn(-40, seg));
        seg.AddEvent(new Wait(.5f, seg));
        seg.AddEvent(new Turn(40, seg));
        seg.AddEvent(new Wait(.5f, seg));
        seg.AddEvent(new Turn(-40, seg));
        seg.AddEvent(new Wait(.5f, seg));
        seg.AddEvent(new Turn(40, seg));
        seg.AddEvent(new Wait(.5f, seg));

        seg.AddEvent (new SpellEnd ());

		SpellCreationSegment seg1 = new SpellCreationSegment ();
		seg1.SetStartingRotation (15);
		seg1.SetTurnSpeed (100);

		seg1.AddEvent (new Turn (-40,seg));
        seg1.AddEvent(new Wait(.5f, seg));
        seg1.AddEvent(new Turn(40, seg));
        seg1.AddEvent(new Wait(.5f, seg));
        seg1.AddEvent(new Turn(-40, seg));
        seg1.AddEvent(new Wait(.5f, seg));
        seg1.AddEvent(new Turn(40, seg));
        seg1.AddEvent(new Wait(.5f, seg));
        seg1.AddEvent(new Turn(-40, seg));
        seg1.AddEvent(new Wait(.5f, seg));
       

		seg1.AddEvent (new SpellEnd ());

		List<SpellCreationSegment> segs = new List<SpellCreationSegment> ();
		segs.Add (seg);
		segs.Add (seg1);
		return segs;
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PorcupinePattern : ISpellPattern {

	public List<SpellCreationSegment> CreateSpellSegs(ISpellGrid grid, float damageMod)
	{
		List<SpellCreationSegment> segs = new List<SpellCreationSegment> ();
		for (int i = 0; i < 8; i++) {
			SpellCreationSegment seg = new SpellCreationSegment ();
			seg.SetStartingRotation(i * 360/8);
            seg.SetTurnSpeed(100);

            //seg.AddEvent(new SpellMove(1, seg));
            seg.AddEvent(new Wait(2, seg));
            seg.AddEvent(new SpeedChange(.01f,seg));
            seg.AddEvent(new Turn(90, seg));
            seg.AddEvent(new Wait(5,seg));
            seg.AddEvent(new SpeedChange(1, seg));
            seg.AddEvent(new Wait(20, seg));
            seg.AddEvent (new SpellEnd ());
			segs.Add (seg);
		}
		return segs;
	}
}

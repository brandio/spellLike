using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ShotgunPattern1 : ISpellPattern {

	public List<SpellCreationSegment> CreateSpellSegs(ISpellGrid grid, float damageMod)
	{
		List<SpellCreationSegment> segs = new List<SpellCreationSegment>();
		for (int i = 0; i < 4; i++)
		{
			SpellCreationSegment seg = new SpellCreationSegment();
			seg.SetStartingRotation((5 * i) - Random.Range(10,40));
			seg.AddEvent(new SpellMove(Random.Range(0 , 2), seg));
			seg.AddEvent(new Wait(12, seg));
			seg.AddEvent(new SpellEnd());
			segs.Add(seg);
		}   
		return segs;
	}
}

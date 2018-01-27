using UnityEngine;
using System.Collections;

public class SpellEnd : SpellEvent {

	public SpellEnd()
	{

	}

    public override void AdjustForSpeed(SpellCreationSegment seg)
    {
        return;

    }

    public override float CallAndWait(ProjectileController projControl)
	{
		projControl.returnToSpell ();
		return 0;
	}

	public override string ToText()
	{
		return "Spell end";
	}
}

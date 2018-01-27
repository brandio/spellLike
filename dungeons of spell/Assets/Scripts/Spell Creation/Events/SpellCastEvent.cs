using UnityEngine;
using System.Collections;

public class SpellCastEvent : SpellEvent {
	public SpellBook spell;

	public SpellCastEvent(SpellBook s)
	{
		spell = s;
	}

    public override void AdjustForSpeed(SpellCreationSegment seg)
    {
        return;
    }

    public override float CallAndWait(ProjectileController projControl)
	{
		Debug.Log ("castSpell " + spell + " " +projControl);
		Debug.Log (projControl + " controler");
		spell.Cast (projControl.transform, false);
		return 0;
	}

	public override string ToText()
	{
		return "Spell segment cast";
	}
}

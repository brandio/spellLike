using UnityEngine;
using System.Collections;

public class SpellCastEvent : SpellEvent {
	public SpellBook spell;
    Vector2 _minOff;
    Vector2 _maxOff;
	public SpellCastEvent(SpellBook s)
	{
		spell = s;
	}
    public SpellCastEvent(SpellBook s, Vector2 minOff, Vector2 maxOff)
    {
        spell = s;
        _minOff = minOff;
        _maxOff = maxOff;
    }


    public override void AdjustForSpeed(SpellCreationSegment seg)
    {
        return;
    }

    public override float CallAndWait(ProjectileController projControl)
	{
		Debug.Log ("castSpell " + spell + " " +projControl);
		Debug.Log (projControl + " controler");
		spell.Cast (projControl.transform, false, new Vector3(Random.Range(_minOff.x, _maxOff.x), Random.Range(_minOff.y, _maxOff.y), 0));
		return 0;
	}

	public override string ToText()
	{
		return "Spell segment cast";
	}
}

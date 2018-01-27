using UnityEngine;
using System.Collections;

public abstract class SpellEvent  {

	public SpellCreationSegment seg;

	abstract public float CallAndWait(ProjectileController projControl);
	abstract public string ToText();
    abstract public void AdjustForSpeed(SpellCreationSegment seg);

}

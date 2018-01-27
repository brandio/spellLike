using UnityEngine;
using System.Collections;

public class SpellPage : SpellComponent, ISpellPage {
    public float damage = 0;
    public float cooldown = 1;
    public float chaos = 1;

    public float GetCoolDown()
	{
		return cooldown;
	}

	public float GetDamage()
	{
		return damage;
	}

	public float GetChaos()
	{
		return chaos;
	}
}

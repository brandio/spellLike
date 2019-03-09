using UnityEngine;
using System.Collections;
using System;

public class SpellPage : SpellComponent, ISpellPage {
    public float damage = 1;
    public float cooldown = 1;
    public float chaos = 1;
    public float reloadTime = 1;
    public float charges = 1;
    public float moveSpeedMod = 1;

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

    public float GetMoveSpeed()
    {
        return moveSpeedMod;
    }

    public float GetCharges()
    {
        return charges;
    }

    public float GetReloadTime()
    {
        return reloadTime;
    }
}

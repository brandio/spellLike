using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellCreationSegment  {

	// Projectory
	float speed;
	float speedMod;
	float turnSpeed;
	float startingRotation;

	// Spell pixels
	List<SpellPixelFinal> spellPixels = new List<SpellPixelFinal> ();
    bool final = false;

	// Events
	public List<SpellEvent> events = new List<SpellEvent>();

    //
    public bool makePixels = true;

    void AdjustEventsForSpeed()
    {
        foreach (SpellEvent spellEvent in events)
        {
            spellEvent.AdjustForSpeed(this);
        }
    }
    
	public string EventsToText()
	{
		string output = "";
		foreach (SpellEvent sEvent in events) {
			output = output + sEvent.ToText();
			output += "\n";
		}
		return output;
	}

	public SpellCreationSegment()
	{
		//turnSpeed = 124;
		speedMod = 1;
	}

	public void calculateValues(ISpellGrid grid,float damage)
	{
		final = true;
		FinalizePixels (grid, damage);
        DetermineSpeed();
        AdjustEventsForSpeed();
    }

	public void setSpeed(float s)
	{
		speed = s;
	}

    public float GetTotalDamage(float runeDamage, float pageDamage, ISpellGrid grid)
    {
        float totalDmg = 0;
        // Add up the cost of each pixel
        foreach (KeyValuePair<Ink, int> entry in grid.GetInkToNumberOfActiveInksOfThatType())
        {
            totalDmg += (entry.Key.GetDamageMod() + runeDamage + pageDamage) * entry.Value/3;
        }

        // Add up the cost of other spells this segment is casting
        foreach (SpellEvent sEvent in events)
        {
            if (sEvent is SpellCastEvent)
            {
                SpellCastEvent sce = (SpellCastEvent)sEvent;
                //totalDmg += sce.spell.cost;
            }
        }
        return totalDmg;
    }

    public float GetCost(float runeCost, float pageCost, ISpellGrid grid)
	{
        float totalCost = 0;
        // Add up the cost of each pixel

        foreach (KeyValuePair<Ink,int> entry in grid.GetInkToNumberOfActiveInksOfThatType())
        {
            totalCost += (entry.Key.GetCost() + pageCost ) * runeCost * entry.Value;
        }

        // Add up the cost of other spells this segment is casting
		foreach (SpellEvent sEvent in events) {
			if (sEvent is SpellCastEvent)
			{
				SpellCastEvent sce = (SpellCastEvent)sEvent;
                totalCost += sce.spell.cost;
			}
		}
        return totalCost;
	}
    
    public float GetSpeedMod()
    {
        return speedMod;
    }

	void DetermineSpeed()
	{
        const float AVERAGE_SPEED = 290;
        const float AVERAGE_SIZE = 1;
        if (spellPixels.Count == 0)
        {
            speed = AVERAGE_SPEED;
            return;
        }
        float total = 0;
		foreach (SpellPixelFinal pixel in spellPixels) {
			total = total + pixel.m_ink.GetMoveSpeed();
		}
		float countRatio = AVERAGE_SIZE / total;
		countRatio = Mathf.Log(countRatio + 1.2f,100);
		speed = AVERAGE_SPEED * countRatio *speedMod;
	}

    void FinalizePixels(ISpellGrid grid, float damage)
	{
        if (!makePixels)
            return;
        ISpellPixel[,] pixels = grid.GetGrid();
		for (int x = 0; x < pixels.GetLength(0); x ++) {
			for(int y = 0; y < pixels.GetLength(1); y++)
			{
				if(pixels[x,y].IsActive())
				{
                    SpellPixelFinal finalPix = new SpellPixelFinal(pixels[x, y].GetInk(), new Vector2(y - pixels.GetLength(1) / 2, x - pixels.GetLength(0) / 2));
                    finalPix.damage = finalPix.m_ink.GetDamageMod() * damage;
                    spellPixels.Add (finalPix);
				}
			}
		}
	}

	public void SetSpeedMod(float sm)
	{

		speedMod = sm;
	}

	public float GetSpeed()
	{
		return speed;
	}

	public List<SpellPixelFinal> GetPixels()
	{
		return spellPixels;
	}

	public float GetStartingRotation()
	{
		return startingRotation;
	}

	public void SetStartingRotation(float rotation)
	{
		startingRotation = rotation;
	}

	public void SetTurnSpeed(float ts)
	{
		turnSpeed = ts;
	}

	public float GetTurnSpeed()
	{
		return turnSpeed;
	}

	public List<SpellEvent> GetEvents()
	{
		return events;
	}

	public void RemoveEvent()
	{
		if (events.Count == 0)
			return;

		events.RemoveAt (events.Count - 1);
	}

	public void AddEvent(SpellEvent spellEvent)
	{
		events.Add (spellEvent);
	}

}

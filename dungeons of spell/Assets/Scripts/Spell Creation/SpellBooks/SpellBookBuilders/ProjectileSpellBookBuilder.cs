using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * This class is responsible for holding all information pertaining to a spell before it is final
 */
public class ProjectileSpellBookBuilder : SpellBookBuilder{

	// Information about spell abilities
	public enum EventTrigger{None,Timed,Remote,Proximity};
	public EventTrigger trigger;
	public SpellEvent spellEvent;

    // This determines if the spell belongs to the player or not
    public spellSource source;

    public enum spellSource {player, enemy, neutral};
    // The language - determines collision properties
    public ILanguage lang;

    // The spell rune - used to make the segments
    private ISpellRune rune;
    public ISpellRune GetRune()
    {
        return rune;
    }
    public void SetRune(ISpellRune r)
    {
        rune = r;
        mSegments = rune.GetPattern(grid);
    }

    // The spell grid - determine shape of the projectile
    public ISpellGrid grid;

	public ProjectileSpellBookBuilder(spellSource spellSource)
	{
		mSegments = new List<SpellCreationSegment> ();
        source = spellSource;
	}

    float GetDamage()
    {
		// This is the damage before the ink is conisdered in each indiviudal spell pixel
        float damage = 1 * rune.GetDamageMod() * page.GetDamage();
        return damage;
    }

    public float CalculateDPS()
    {
        if (page == null)
            return 0;
        return 1 / page.GetCoolDown() * CalculateTotalDamage();
    }

    public float CalculateTotalDamage()
    {
        float totalDmg = 0;
        // Add up the cost of all the segs
        List<SpellCreationSegment> spellSegs = GetSpellSegs();
        if (page == null || rune == null)
        {
            return 0;
        }
        foreach (SpellCreationSegment seg in spellSegs)
        {
            totalDmg += seg.GetTotalDamage(page.GetDamage(), rune.GetDamageMod(), grid);
        }
        return totalDmg;
    }

    public float CalculateCost()
    {

        float totalCost = 0;
        // Add up the cost of all the segs
        List<SpellCreationSegment> spellSegs = GetSpellSegs();
        if(page == null || rune == null)
        {
            return 0;
        }
        foreach (SpellCreationSegment seg in spellSegs)
        {
            totalCost += seg.GetCost(page.GetCost(), rune.GetCost(), grid);
        }
        return totalCost;
    }

    List<SpellCreationSegment> mSegments;

    public List<SpellCreationSegment> GetSpellSegs()
	{
		return mSegments;
	}

    public string[] GetLayers()
    {
        List<string> layers = new List<string>();
        layers.AddRange(lang.GetLayers());
        if (layers.Contains("Player"))
        {
            switch(source)
            {
                case spellSource.player:
                    layers.Remove("Player");
                    layers.Add("Enemy");
                    layers.Add("ProjectileEnemy");
                    layers.Add("ProjectileNeutral");
                    break;
                case spellSource.enemy:
                    layers.Add("ProjectilePlayer");
                    layers.Add("ProjectileNeutral");
                    break;
                case spellSource.neutral:
                    layers.Add("Enemy");
                    layers.Add("ProjectileEnemy");
                    layers.Add("ProjectilePlayer");
                    break;

            }
        }
        return layers.ToArray();
    }

    public ProjectileSpellBook MakeSpellBook()
    {

        List<SpellCreationSegment> spellSegs = GetSpellSegs();
        foreach (SpellCreationSegment seg in spellSegs)
        {
            if(source != ProjectileSpellBookBuilder.spellSource.player)
            {
                seg.SetSpeedMod(seg.GetSpeedMod() * .6f);
            }

            ISpellPixel[,] pixels = grid.GetGrid();
            seg.calculateValues(grid, GetDamage());
        }

        CalculateCost();
        ProjectileSpellBook spellBook = new ProjectileSpellBook();
        spellBook.Init(this);
        return spellBook;
    }
}

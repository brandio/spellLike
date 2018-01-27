using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SpellRune : SpellComponent, ISpellRune {
    public string pattern;
    public float damageMod;
    public bool thrown = false;

    public bool IsThrown()
    {
        return thrown;
    }

    public float GetDamageMod()
    {
        return damageMod;
    }

    public List<SpellCreationSegment> GetPattern(ISpellGrid grid)
    {
        System.Runtime.Remoting.ObjectHandle handle = System.Activator.CreateInstance(null, pattern);
        ISpellPattern spellPattern = handle.Unwrap() as ISpellPattern;
        return spellPattern.CreateSpellSegs(grid, damageMod);
    }
}

using UnityEngine;
using System.Collections;

public abstract class SpellBookBuilder {
    public GameObject caster;
	public ISpellPage page;
    public string name = "My Spell Book";
}

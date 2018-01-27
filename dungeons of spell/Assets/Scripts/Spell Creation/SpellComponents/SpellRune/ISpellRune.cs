using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ISpellRune : ISpellComponent  {
    List<SpellCreationSegment> GetPattern(ISpellGrid grid);
    float GetDamageMod();
    bool IsThrown();
}

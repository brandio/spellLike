using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ISpellPattern  {
    List<SpellCreationSegment> CreateSpellSegs(ISpellGrid grid, float damageMod);

}

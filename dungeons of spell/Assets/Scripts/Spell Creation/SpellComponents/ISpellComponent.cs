using UnityEngine;
using System.Collections;

public interface ISpellComponent  {
	string GetToolTip ();
    bool IsInUse ();
    string GetTitle();
    float GetCost();
    SpellComponent.SubSpellComponentType GetSubType();
}

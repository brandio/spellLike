using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RuneStats : UiStats
{
    public override void UpdateStats(SpellComponent.SubSpellComponentType type, string components)
    {
        if(builder.GetRune() != null)
        {
            UpdateBaseStats(builder.GetRune());
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LangStats : UiStats {
    public override void UpdateStats(SpellComponent.SubSpellComponentType type, string components)
    {
        if (builder.lang != null)
        {
            UpdateBaseStats(builder.lang);
        }
    }
}

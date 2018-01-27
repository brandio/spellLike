using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PaperStats : UiStats
{
    public Text chaos;
    public Text coolDown;
    public override void UpdateStats(SpellComponent.SubSpellComponentType type, string components)
    {
        if (builder.page != null)
        {
            chaos.text = "" + builder.page.GetChaos();
            coolDown.text = "" + builder.page.GetCoolDown();
            UpdateBaseStats(builder.page);
        }
    }
}

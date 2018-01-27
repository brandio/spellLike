using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class UiStats : MonoBehaviour {
    public Text toolTip;
    public Text cost;
    public Text title;
    public ProjectileSpellBookBuilder builder;

    public void UpdateBaseStats(ISpellComponent component)
    {
        toolTip.text = "" + component.GetToolTip();
        cost.text = "" + component.GetCost();
        title.text = "" + component.GetTitle();
    }

    public abstract void UpdateStats(SpellComponent.SubSpellComponentType type, string components);


}

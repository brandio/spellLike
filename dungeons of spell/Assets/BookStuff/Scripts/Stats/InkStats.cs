using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InkStats : UiStats
{
    public override void UpdateStats(SpellComponent.SubSpellComponentType type, string components)
    {
        ComponentLoader.UnLoadedSpellComponent unloadedComponent;
        unloadedComponent.name = components;
        unloadedComponent.type = type;
        ISpellComponent component = ComponentLoader.GetInstance().LoadComponent(unloadedComponent);
        UpdateBaseStats(component);
    }
}

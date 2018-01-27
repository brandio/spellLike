using UnityEngine;
using System.Collections;

public class SpellComponent : ISpellComponent {
    public enum SubSpellComponentType { Rune, Ink, Paper, Cover, language }

    public string title;
    public string toolTip;
    public SubSpellComponentType subType;
    public float cost;
    public bool isActive = false;
    
    public SubSpellComponentType GetSubType()
    {
        return subType;
    }

    public string GetTitle()
    {
        return title;
    }

    public float GetCost()
    {
        return cost;
    }

    public string GetToolTip()
    {
        return toolTip;
    }

    public bool IsInUse()
    {
        return isActive;
    }
}

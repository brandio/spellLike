using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ComponentLoader {
    public struct UnLoadedSpellComponent
    {
        public UnLoadedSpellComponent(string n, SpellComponent.SubSpellComponentType t)
        {
            name = n;
            type = t;
        }
        public string name;
        public SpellComponent.SubSpellComponentType type;
    }

    static ComponentLoader instance;
    public ComponentLoader()
    {

    }
    string path;

    public ISpellComponent LoadComponent(UnLoadedSpellComponent spellUnLoaded)
    {
        string localPath = Path.Combine(path, spellUnLoaded.type.ToString());
        localPath = Path.Combine(localPath, spellUnLoaded.name + ".json");
        string json = File.ReadAllText(localPath);
        switch(spellUnLoaded.type)
        {
            case SpellComponent.SubSpellComponentType.Paper:
                SpellPage page = JsonUtility.FromJson<SpellPage>(json);
                return page;
            case SpellComponent.SubSpellComponentType.Rune:
                SpellRune rune = JsonUtility.FromJson<SpellRune>(json);
                return rune;
            case SpellComponent.SubSpellComponentType.language:
                Language lang = JsonUtility.FromJson<Language>(json);
                return lang;
            case SpellComponent.SubSpellComponentType.Ink:
                Ink ink = JsonUtility.FromJson<Ink>(json);
                return ink;
            default:
                return null;
        }
    }

    public UnLoadedSpellComponent[] StartingSpellComponents; 
    public UnLoadedSpellComponent[] UnlockableSpellComponents;

    public static ComponentLoader GetInstance()
    {
        if (instance != null)
        {
            return instance;
        }
        instance = new ComponentLoader();
        instance.path = Path.Combine("Assets", "Resources");
        instance.path = Path.Combine(instance.path, "SpellInfo");

        instance.StartingSpellComponents = new UnLoadedSpellComponent[] { new UnLoadedSpellComponent("Paper", SpellComponent.SubSpellComponentType.Paper),
            new UnLoadedSpellComponent("Bamboo", SpellComponent.SubSpellComponentType.Paper),
             new UnLoadedSpellComponent("Raging Badger", SpellComponent.SubSpellComponentType.Rune),
             new UnLoadedSpellComponent("Bomb", SpellComponent.SubSpellComponentType.Rune),
             new UnLoadedSpellComponent("BullFrog", SpellComponent.SubSpellComponentType.Rune),
			new UnLoadedSpellComponent("TwoClaw", SpellComponent.SubSpellComponentType.Rune),
			new UnLoadedSpellComponent("English", SpellComponent.SubSpellComponentType.language),
             new UnLoadedSpellComponent("SootExtract", SpellComponent.SubSpellComponentType.Ink),
             new UnLoadedSpellComponent("Gold", SpellComponent.SubSpellComponentType.Ink),
             new UnLoadedSpellComponent("Insect", SpellComponent.SubSpellComponentType.Ink),
			 new UnLoadedSpellComponent("VampireBlood", SpellComponent.SubSpellComponentType.Ink),
             new UnLoadedSpellComponent("Earth", SpellComponent.SubSpellComponentType.Ink)};

        instance.UnlockableSpellComponents = new UnLoadedSpellComponent[] { new UnLoadedSpellComponent("Silk", SpellComponent.SubSpellComponentType.Paper),
             new UnLoadedSpellComponent("Papyrus", SpellComponent.SubSpellComponentType.Paper),
             new UnLoadedSpellComponent("Linen", SpellComponent.SubSpellComponentType.Paper),
             new UnLoadedSpellComponent("Silk", SpellComponent.SubSpellComponentType.Paper),
             new UnLoadedSpellComponent("Cotton", SpellComponent.SubSpellComponentType.Paper),
             new UnLoadedSpellComponent("Boomerang", SpellComponent.SubSpellComponentType.Rune),
             new UnLoadedSpellComponent("LeftHook", SpellComponent.SubSpellComponentType.Rune),
             new UnLoadedSpellComponent("RightHook", SpellComponent.SubSpellComponentType.Rune),
             new UnLoadedSpellComponent("FrontBack", SpellComponent.SubSpellComponentType.Rune),
             new UnLoadedSpellComponent("TwinBat", SpellComponent.SubSpellComponentType.Rune),
             new UnLoadedSpellComponent("Tortoise", SpellComponent.SubSpellComponentType.Rune),
             new UnLoadedSpellComponent("AlphaStar", SpellComponent.SubSpellComponentType.Rune),
             new UnLoadedSpellComponent("Slipping Snake", SpellComponent.SubSpellComponentType.Rune),
             new UnLoadedSpellComponent("Basic", SpellComponent.SubSpellComponentType.Rune),
             new UnLoadedSpellComponent("Spore Burst", SpellComponent.SubSpellComponentType.Rune),
             new UnLoadedSpellComponent("Ghastlish", SpellComponent.SubSpellComponentType.language),
             new UnLoadedSpellComponent("GlurpleTongue", SpellComponent.SubSpellComponentType.language),
             new UnLoadedSpellComponent("Gnomish", SpellComponent.SubSpellComponentType.language),
             new UnLoadedSpellComponent("Hentch", SpellComponent.SubSpellComponentType.language),
             new UnLoadedSpellComponent("Vampiric", SpellComponent.SubSpellComponentType.language),
             new UnLoadedSpellComponent("Shadowskript", SpellComponent.SubSpellComponentType.language),
             new UnLoadedSpellComponent("FireWeedSap", SpellComponent.SubSpellComponentType.Ink),
             new UnLoadedSpellComponent("Slit-Faced Serum", SpellComponent.SubSpellComponentType.Ink),
             new UnLoadedSpellComponent("Spore Oil", SpellComponent.SubSpellComponentType.Ink),
             new UnLoadedSpellComponent("Moon Lace", SpellComponent.SubSpellComponentType.Ink)};

        SpellRune rune = new SpellRune();
        rune.toolTip = "Cast from mouse position shooting violent violents in all directions!";
        rune.title = "Bomb";
        rune.pattern = "BlowFishPattern";
        rune.damageMod = 1;
        rune.cost = 3;
        rune.thrown = true;
        rune.subType = SpellComponent.SubSpellComponentType.Rune;
        string jsonString = JsonUtility.ToJson(rune);
        File.WriteAllText("Assets/Resources/Bomb.json", jsonString);

        return instance;

        /*
       Ink ink = new Ink();
       ink.cost = 1;
       ink.toolTip = "tip";
       ink.title = "title";
       ink.color = Color.red;
       ink.priority = 1;
       ink.moveSpeed = 1;
       ink.damageMod = 1;
       ink.onHitEffectName = "fire";

       string jsonString = JsonUtility.ToJson(ink);
       File.WriteAllText("Assets/Resources/pageink23.json", jsonString);

       string second = File.ReadAllText("Assets/Resources/SpellInfo/Ink/FireWeedSap.json");
       Debug.Log(second);
       Ink i = JsonUtility.FromJson<Ink>(second);
       string myString = JsonUtility.ToJson(second);
       //Debug.Log(page2.GetToolTip());
       Debug.Log(myString);
       */

    }

    void Start()
    {


    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerInventory : MonoBehaviour {
    public List<string> runes = new List<string>();
    public List<string> langs = new List<string>();
    public List<string> inks = new List<string>();
    public List<string> pages = new List<string>();

	[HideInInspector]
	public Dictionary<string,SpellComponent.SubSpellComponentType> componentsInStore = new Dictionary<string, SpellComponent.SubSpellComponentType>();
	Dictionary<string,SpellComponent.SubSpellComponentType> componentsAvailable = new Dictionary<string, SpellComponent.SubSpellComponentType> ();
    public Dictionary<SpellComponent.SubSpellComponentType, List<string>> ownedComponents = new Dictionary<SpellComponent.SubSpellComponentType, List<string>>();
    public static PlayerInventory instance;
    void Awake()
    {
        
        if(instance != null)
        {
            Debug.LogError("THI IS A SINGLETON ");
        }
        else
        {
            instance = this;
        }

		Init ();
        ComponentLoader loader = ComponentLoader.GetInstance();

        foreach (ComponentLoader.UnLoadedSpellComponent comp in loader.StartingSpellComponents)
        {
            string value = comp.name;
            switch (comp.type)
            {
                case SpellComponent.SubSpellComponentType.Paper:
                    pages.Add(value);
                    break;
                case SpellComponent.SubSpellComponentType.Rune:
                    runes.Add(value);
                    break;
                case SpellComponent.SubSpellComponentType.language:
                    langs.Add(value);
                    break;
                case SpellComponent.SubSpellComponentType.Ink:
                    inks.Add(value);
                    break;
                default:
                    break;
            }
        }
    }

	public void AddComponent(string componentName, bool shop)
	{
		Dictionary<string,SpellComponent.SubSpellComponentType> type = !shop ? componentsAvailable : componentsInStore;
		switch (type [componentName]) {
		case SpellComponent.SubSpellComponentType.Paper:
			pages.Add(componentName);
			break;
		case SpellComponent.SubSpellComponentType.Rune:
			runes.Add(componentName);
			break;
		case SpellComponent.SubSpellComponentType.language:
			langs.Add(componentName);
			break;
		case SpellComponent.SubSpellComponentType.Ink:
			inks.Add(componentName);
			break;
		default:
			break;
		}
		if (shop) {
			componentsInStore.Remove(componentName);
		} else {
			componentsAvailable.Remove (componentName);
		}

	}

    public string AddRandomComponent()
    {
		string component = GetRandomComponent ();
		AddComponent (component, false);
		return component;
    }

	public string AddComponentToStore()
	{
		string component = GetRandomComponent();
		componentsInStore.Add (component, componentsAvailable [component]);
		componentsAvailable.Remove (component);
		return component;

	}
	string GetRandomComponent()
	{
		List<string> keyList = new List<string>(componentsAvailable.Keys);
		return keyList[Random.Range (0,keyList.Count)];
	}

    void Init()
    {
        ownedComponents[SpellComponent.SubSpellComponentType.Paper] = pages;
        ownedComponents[SpellComponent.SubSpellComponentType.language] = langs;
        ownedComponents[SpellComponent.SubSpellComponentType.Rune] = runes;
        ownedComponents[SpellComponent.SubSpellComponentType.Ink] = inks;

        ComponentLoader loader = ComponentLoader.GetInstance();
        //foreach (ComponentLoader.UnLoadedSpellComponent comp in loader.StartingSpellComponents)
        //{
        //    string value = comp.name;
        //    switch (comp.type)
        //    {
        //        case SpellComponent.SubSpellComponentType.Paper:
        //            pages.Add(value);
        //            break;
        //        case SpellComponent.SubSpellComponentType.Rune:
        //            runes.Add(value);
        //            break;
        //        case SpellComponent.SubSpellComponentType.language:
        //            langs.Add(value);
        //            break;
        //        case SpellComponent.SubSpellComponentType.Ink:
        //            inks.Add(value);
        //            break;
        //        default:
        //            break;
        //    }
        //}

        foreach (ComponentLoader.UnLoadedSpellComponent comp in loader.UnlockableSpellComponents)
		{
			if(!componentsAvailable.ContainsKey(comp.name))
			{
				componentsAvailable.Add(comp.name,comp.type);
			}
		}
    }
	
    void FillAll()
    {
        //LoadStarterComponents();
        ComponentLoader loader = ComponentLoader.GetInstance();
        foreach (ComponentLoader.UnLoadedSpellComponent comp in loader.UnlockableSpellComponents)
        {
            string value = comp.name;
            switch (comp.type)
            {
                case SpellComponent.SubSpellComponentType.Paper:
                    pages.Add(value);
                    break;
                case SpellComponent.SubSpellComponentType.Rune:
                    runes.Add(value);
                    break;
                case SpellComponent.SubSpellComponentType.language:
                    langs.Add(value);
                    break;
                case SpellComponent.SubSpellComponentType.Ink:
                    inks.Add(value);
                    break;
                default:
                    break;
            }
        }

    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BookUiMaster : MonoBehaviour {
    // Tab stuff
    void Awake()
    {
        playerInventory = PlayerInventory.instance;
        SpawnTabs();
        ComponentSelected += stats.UpdateStats;
    }

    public GameObject TabButton;
    ProjectileSpellBookBuilder oldBuilder;
    public float xStart = 0;
    public float ySpacing = 0;
    public float yStart = 0;
    public Text titleText;
    public InputField nameInput;
    public Scrollbar scrollbar;
    void SpawnTabs()
    {
        for (int i = 0; i < layOuts.Count; i++)
        {
            GameObject tabObject = GameObject.Instantiate(TabButton, Vector2.zero, Quaternion.identity) as GameObject;
            tabObject.transform.parent = transform;
            tabObject.transform.localPosition = new Vector2(xStart, yStart + (ySpacing * i));
            tabObject.transform.eulerAngles = new Vector3(0, 0, 90);
            Tab tab = tabObject.GetComponent<Tab>();
            tab.index = i;
            tab.TabSelected += ChangeLayOut;
            layOuts[i].layoutTabObject = tabObject;

        }
    }

    [System.Serializable]
    public class LayOutInfo
    {
        public Sprite tabIcon;
        public string title;
        public SpellComponent.SubSpellComponentType type;
        public List<GameObject> gameObjects = new List<GameObject>();
        [HideInInspector]
        public GameObject layoutTabObject;
    }

    PlayerInventory playerInventory;
    public GlobalStats stats;
    public List<LayOutInfo> layOuts = new List<LayOutInfo>();
    public Dictionary<SpellComponent.SubSpellComponentType, int> typeToSelected = new Dictionary<SpellComponent.SubSpellComponentType, int>();
    int selectedLayOut = 0;

    public delegate void LayOutChangedHandler(LayOutInfo layoutInfo, List<string> components);
    public event LayOutChangedHandler LayOutChanged;

    void ChangeLayOut(int index)
    {
        selectedLayOut = index;
        titleText.text = layOuts[index].title;
        int i = 0;
        foreach (LayOutInfo lOut in layOuts)
        {
            if(i == index)
            {
                i++;
                continue;
            }
            lOut.layoutTabObject.transform.localScale = new Vector3(40, 40, 1);
            lOut.layoutTabObject.gameObject.GetComponent<Button>().enabled = true;
            foreach(GameObject layoutObject in lOut.gameObjects)
            {
                layoutObject.SetActive(false);
            }
            i++;
        }

        layOuts[index].layoutTabObject.transform.localScale = new Vector3(55, 55, 1);
        layOuts[index].layoutTabObject.gameObject.GetComponent<Button>().enabled = false;

        foreach (GameObject layoutObject in layOuts[index].gameObjects)
        {
            layoutObject.SetActive(true);
            UiStats stats = layoutObject.GetComponent<UiStats>();
            if(stats)
            {
                stats.builder = oldBuilder;
                ComponentSelected -= stats.UpdateStats;
                ComponentSelected += stats.UpdateStats;

            }
        }

        if (LayOutChanged != null)
        {
            LayOutChanged(layOuts[index], playerInventory.ownedComponents[layOuts[index].type]);
        }   
    }

    public delegate void ComponentSelectedEventHandler(SpellComponent.SubSpellComponentType type, string components);
    public event ComponentSelectedEventHandler ComponentSelected;

    public void CompIndexSelected(int index)
    {
        Debug.Log("index " + index);
        SpellComponent.SubSpellComponentType type = layOuts[selectedLayOut].type;
        string component = playerInventory.ownedComponents[type][index];
        if(ComponentSelected != null)
        {
            ComponentSelected(type, component);
        }
        scrollbar.value = 1;
    }

    public void InitBook(ProjectileSpellBookBuilder builder, ProjectileSpellBookBuilder newBuilder, SpellGrid spellGrid)
    {
        oldBuilder = newBuilder;
        stats.builder = newBuilder;
        spellGrid.GridChanged += stats.UpdateStats;

        SpellComponent.SubSpellComponentType type = SpellComponent.SubSpellComponentType.Rune;
        InitType(type, builder);
        int index = playerInventory.ownedComponents[type].FindIndex(delegate (string s) { return s == builder.GetRune().GetTitle(); });
        if (index >= 0)
        {
            CompIndexSelected(index);
        }
        else
        {
            Debug.LogError("Cant find that in the option list " + builder.GetRune().GetTitle());
        }

        type = SpellComponent.SubSpellComponentType.language;
        InitType(type, builder);
        index = playerInventory.ownedComponents[type].FindIndex(delegate (string s) { return s == builder.lang.GetTitle(); });
        if (index >= 0)
        {
            CompIndexSelected(index);
        }
        else
        {
            Debug.LogError("Cant find that in the option list " + builder.lang.GetTitle());
        }

        type = SpellComponent.SubSpellComponentType.Paper;
        InitType(type, builder);
        index = playerInventory.ownedComponents[type].FindIndex(delegate (string s) { return s == builder.page.GetTitle(); });
        if (index >= 0)
        {
            CompIndexSelected(index);
        }
        else
        {
            Debug.LogError("Cant find that in the option list " + builder.page.GetTitle());
        }

        type = SpellComponent.SubSpellComponentType.Ink;
        InitType(type, builder);
        CompIndexSelected(0);
        ChangeLayOut(layOuts.Count - 1);
        stats.UpdateStats();
        scrollbar.value = 1;
    }

    void InitType(SpellComponent.SubSpellComponentType type, ProjectileSpellBookBuilder builder)
    {
        int i = 0;
        while (i < layOuts.Count)
        {
            if(type == layOuts[i].type)
            {
                ChangeLayOut(i);
                break;
            }
            i++;
        }
    }
}

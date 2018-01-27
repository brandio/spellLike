using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class GlobalStats : MonoBehaviour {

    [System.Serializable]
    public struct Stat
    {
        public string name;
        public float value;
        public string toolTip;
    }
    public ProjectileSpellBookBuilder builder;
    public List<Stat> stats = new List<Stat>();
    public List<Text> texts = new List<Text>();
    public GameObject statObject;
    public float yOffSet = 0;
    public float spacing = 0;
    public float xOffSet = 0;
    void Start()
    {
        int i = 0;
        foreach(Stat stat in stats)
        {
            GameObject newStatObject = GameObject.Instantiate(statObject,Vector2.zero, Quaternion.identity) as GameObject;
            i++;
            newStatObject.transform.parent = transform;
            newStatObject.transform.localScale = new Vector3(1, 1, 1);
            newStatObject.transform.localPosition = new Vector2(xOffSet, yOffSet - spacing * i);
            newStatObject.GetComponent<Text>().text = stat.name + ": ";
            newStatObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "" + stat.value;
            texts.Add(newStatObject.transform.GetChild(0).gameObject.GetComponent<Text>());
            newStatObject.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Text>().text = "" + stat.toolTip;
        }
    }

   public void UpdateStats()
    {
        UpdateStats(SpellComponent.SubSpellComponentType.Ink , "");
    }

   public void UpdateStats(SpellComponent.SubSpellComponentType type, string components)
    {
        if(texts.Count < 4)
        {
            return;
        }

        if(builder.page != null)
        {
            texts[0].text = "" + builder.CalculateCost();
            texts[1].text = "" + builder.CalculateTotalDamage();
            texts[2].text = "" + builder.CalculateDPS();
            texts[3].text = "" + builder.page.GetCoolDown();
            texts[5].text = "" + builder.page.GetTitle();
        }
        if(builder.lang != null)
        {
            texts[6].text = "" + builder.lang.GetTitle();
        }
        if (builder.GetRune() != null)
        {
            texts[4].text = "" + builder.GetRune().GetTitle();
        }

    }
}

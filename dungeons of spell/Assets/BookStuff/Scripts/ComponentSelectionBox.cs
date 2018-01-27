using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class ComponentSelectionBox : MonoBehaviour {

    public GameObject button;
    public int xStart;
    public int yStart;
    public int xSpacing;
    public int ySpacing;
    public int columns;
    public BookUiMaster uiMaster;
    public Scrollbar scrollBar;
    RectTransform rectTransform;
    List<GameObject> buttons = new List<GameObject>();

    void PlaceButtons(List<string> components)
    {
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, (components.Count / columns + 1) * (ySpacing + 10));
        for (int i = 0; i < components.Count; i++)
        {
            Vector2 pos = new Vector2(xStart + (i % columns) * xSpacing, yStart - (i / columns) * ySpacing);
            GameObject buttonObject = GameObject.Instantiate(button, Vector2.zero, Quaternion.identity) as GameObject;
            buttons.Add(buttonObject);
            buttonObject.GetComponent<SpellComponentButton>().index = i;
            buttonObject.GetComponent<SpellComponentButton>().bookUiMaster = uiMaster;
            buttonObject.transform.GetChild(0).GetComponent<Text>().text = components[i];
            buttonObject.transform.parent = transform;
            buttonObject.transform.localPosition = pos;
            buttonObject.transform.localScale = new Vector3(1, 1, 1);
        }
        scrollBar.value = 0;
    }

    // Use this for initialization
    void Awake () {
        scrollBar.value = 0;
        rectTransform = this.GetComponent<RectTransform>();
        uiMaster.LayOutChanged += UpdateNewLayout;
    }
	
    void ClearButtons()
    {
        foreach(GameObject button in buttons)
        {
            Destroy(button);
        }
    }

    void UpdateNewLayout(BookUiMaster.LayOutInfo layOutInfo, List<string> comps)
    {
        ClearButtons();
        Debug.Log(comps.Count);
        PlaceButtons(comps);
        scrollBar.value = 0;
    }
}

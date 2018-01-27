using UnityEngine;
using System.Collections;

public class Tab : MonoBehaviour {
    //[HideInInspector]
    public int index;

    public delegate void TabSelectedEventHandler(int index);
    public event TabSelectedEventHandler TabSelected;

    public void pressedOn()
    {
        TabSelected(index);
    }

}

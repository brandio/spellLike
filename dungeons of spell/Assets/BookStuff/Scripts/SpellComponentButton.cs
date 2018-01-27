using UnityEngine;
using System.Collections;

public class SpellComponentButton : MonoBehaviour {
    [HideInInspector]
    public int index;
    [HideInInspector]
    public BookUiMaster bookUiMaster;

    public void Selected()
    {
        bookUiMaster.CompIndexSelected(index);
    }
}

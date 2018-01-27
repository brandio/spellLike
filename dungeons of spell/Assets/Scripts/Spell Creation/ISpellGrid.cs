using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public interface ISpellGrid  {
	ISpellPixel[,] GetGrid ();
	void SetPixel(int x, int y, bool active);
    void SetInk(IInk nInk);
    IInk GetInk();
    void OnGridChanged(Ink ink, bool changed);
    Dictionary<Ink,int> GetInkToNumberOfActiveInksOfThatType();
}

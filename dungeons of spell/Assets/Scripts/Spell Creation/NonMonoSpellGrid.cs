using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NonMonoSpellGrid : ISpellGrid {
    int sizeX;
    int sizeY;

    NonMonoSpellPixel[,] spellPixels;

    IInk ink;

    public Dictionary<Ink, int> InkToNumberOfActiveInksOfThatType;

    public delegate void GridChangeHandler();
    public event GridChangeHandler GridChanged;

    public Dictionary<Ink, int> GetInkToNumberOfActiveInksOfThatType()
    {
        return InkToNumberOfActiveInksOfThatType;
    }

    public void OnGridChanged(Ink ink, bool added)
    {
        if (InkToNumberOfActiveInksOfThatType.ContainsKey(ink))
        {
            if (added)
            {
                InkToNumberOfActiveInksOfThatType[ink] += 1;
            }
            else
            {
                InkToNumberOfActiveInksOfThatType[ink] += -1;
            }
        }
        else if (added)
        {
            InkToNumberOfActiveInksOfThatType.Add(ink, 1);
        }

        if (GridChanged != null)
        {
            GridChanged();
        }
    }

    public NonMonoSpellGrid(int xx, int yy)
    {
        spellPixels = new NonMonoSpellPixel[xx, yy];
        sizeX = xx;
        sizeY = yy;
        InkToNumberOfActiveInksOfThatType = new Dictionary<Ink, int>();
        ink = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("SootExtract", SpellComponent.SubSpellComponentType.Ink)) as Ink;

        for (int x = 0; x < sizeX; x++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                spellPixels[x, y] = new NonMonoSpellPixel(this);
            }
        }
    }

    public ISpellPixel[,] GetGrid()
    {
        return spellPixels;
    }

    public void SetPixel(int x, int y, bool active)
    {
        spellPixels[x, y].SetSpellGrid(this);
        spellPixels[x, y].ChangeActive(true);
    }

    public void SetInk(IInk nInk)
    {
        ink = nInk;
    }

    public IInk GetInk()
    {
        return ink;
    }
}

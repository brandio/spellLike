using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellGrid : MonoBehaviour, ISpellGrid {
	public GameObject gridObject;
	public int sizeX;
	public int sizeY;
	
	ISpellPixel[,] spellPixels;

    IInk ink;

    Dictionary<Ink, int> InkToNumberOfActiveInksOfThatType;
    
    public delegate void GridChangeHandler();
    public event GridChangeHandler GridChanged;

    public Dictionary<Ink, int> GetInkToNumberOfActiveInksOfThatType()
    {
        return InkToNumberOfActiveInksOfThatType;
    }

    public void OnGridChanged(Ink ink, bool added)
    {
		if (ink == null) {
			return;
		}
        if(InkToNumberOfActiveInksOfThatType.ContainsKey(ink))
        {
            if(added)
            {
                InkToNumberOfActiveInksOfThatType[ink] += 1;
            }
            else
            {
                InkToNumberOfActiveInksOfThatType[ink] += -1;
            }
        }
        else if(added)
        {
            InkToNumberOfActiveInksOfThatType.Add(ink, 1);
        }

        if(GridChanged != null)
        {
            GridChanged();
        }
    }

    public void ClearGrid()
	{
        Debug.Log("ClearingGrid");
        if (spellPixels == null) {
            MakeGrid();
            return;
		}
		InkToNumberOfActiveInksOfThatType = new Dictionary<Ink, int>();
		foreach(SpellPixel spellPixel in spellPixels)
		{
			spellPixel.ChangeActive(false);
		}
	}

	public IInk GetInk()
	{
		return ink;
	}

	public void SetInk(IInk nInk)
	{
		ink = nInk;
	}
	
	void MakeGrid()
	{
        // protect against double init
        if (spellPixels != null)
        {
            return;
        }
        spellPixels = new SpellPixel[sizeX,sizeY];
        InkToNumberOfActiveInksOfThatType = new Dictionary<Ink, int>();
        const int distance = 1;
		for (int x = 0; x < sizeX; x++) {
			for (int y = 0; y < sizeY; y++) {
                GameObject gridPixel = Instantiate(gridObject, Vector3.zero  , Quaternion.identity) as GameObject;
				SpellPixel pixel = gridPixel.GetComponent<SpellPixel>();
				pixel.SetSpellGrid(this);
				spellPixels[x,y] = pixel;
				pixel.transform.parent = this.transform;
                pixel.transform.localScale = new Vector3(1, 1, 1);
                pixel.transform.localPosition = new Vector3(x * distance, y * distance, 0);
            }
		}
	}

	void Start()
	{
		Ink nInk = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("SootExtract", SpellComponent.SubSpellComponentType.Ink)) as Ink;
		this.ink = nInk;
	}

	public ISpellPixel[,] GetGrid()
	{
        NonMonoSpellGrid spellGrid = new NonMonoSpellGrid(sizeX, sizeY);
        for (int x = 0; x < spellPixels.GetLength(0); x++)
        {
            for (int y = 0; y < spellPixels.GetLength(1); y++)
            {
                if (spellPixels[x, y].IsActive())
                {
                    spellGrid.SetInk(spellPixels[x, y].GetInk());
                    spellGrid.SetPixel(x, y, true);
                }
            }
        }
        return spellGrid.GetGrid();
    }

	public void SetPixel(int x, int y, bool active)
	{
        spellPixels [x, y].ChangeActive (true);
	}
	
    public void SetGrid(ISpellPixel[,] grid)
    {
        ClearGrid();
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                if (grid[x, y].IsActive())
                {
                    SetInk(grid[x, y].GetInk());
                    SetPixel(x, y, true);
                }
            }
        }
    }

    void Awake () {
        MakeGrid();
    }


}

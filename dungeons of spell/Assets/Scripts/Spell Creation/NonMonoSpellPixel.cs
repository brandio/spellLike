using UnityEngine;
using System.Collections;

public class NonMonoSpellPixel : ISpellPixel {

    ISpellGrid grid;
    IInk ink;
    bool active;

    public NonMonoSpellPixel(ISpellGrid sg)
    {
        grid = sg;
    }

    public bool IsActive()
    {
        return active;
    }

    public IInk GetInk()
    {
        return ink;
    }

    public void SetSpellGrid(ISpellGrid sg)
    {
        grid = sg;
    }

    public void UpdateInk()
    {
        ink = grid.GetInk();
    }

    public void ChangeActive()
    {
        active = !active;
    }

    public void ChangeActive(bool setActive)
    {
        if (!setActive)
        {
            grid.OnGridChanged((Ink)ink, false);
            active = false;
        }
        else
        {
            active = true;
            ink = grid.GetInk() as Ink;
            grid.OnGridChanged((Ink)ink, true);
        }
    }

}

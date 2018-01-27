using UnityEngine;
using System.Collections;

public interface ISpellPixel
{
    bool IsActive();

    IInk GetInk();

    void SetSpellGrid(ISpellGrid sg);

    void UpdateInk();

    void ChangeActive();

    void ChangeActive(bool setActive);
}

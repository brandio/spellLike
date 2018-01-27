using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]
public class SpellPixel : MonoBehaviour, ISpellPixel {
	public bool active = false;
	public Sprite activeSprite;
	public Sprite unactiveSprite;

    [HideInInspector]
    public Ink ink;

	SpriteRenderer spriteRenderer;
	ISpellGrid spellGrid;

	void Awake () {
		spriteRenderer = this.GetComponent<SpriteRenderer> ();
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
		spellGrid = sg;
	}

	public void UpdateInk()
	{
		ink = spellGrid.GetInk() as Ink;
	}

	public void ChangeActive()
	{
		ChangeActive (!active);
	}

	public void ChangeActive(bool setActive)
	{
        if (!setActive) {
            spellGrid.OnGridChanged(ink, false);
            active = false;
            spriteRenderer.sprite = unactiveSprite;
            spriteRenderer.color = Color.white;

        }
        else
        {
            UpdateInk();
            spellGrid.OnGridChanged(ink, true);
            active = true;
            spriteRenderer.sprite = activeSprite;
            ink = spellGrid.GetInk() as Ink;
            spriteRenderer.color = ink.GetColor();
        }
    }


}

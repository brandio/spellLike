using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpellBookHud : MonoBehaviour {
    public Shoot PlayerSpellsBooks;
    Text text;
    SpellBook mBook;
	// Use this for initialization
	void Awake () {
        text = GetComponent<Text>();
        text.text = "0";
        PlayerSpellsBooks.SpellChanged += UpdateDisplay;
	}
    void UpdateCount(SpellBook spell)
    {
        string count = "∞";
        if (spell != null && spell.chargesCurrent != -1)
        {
            count = spell.chargesCurrent + " / " + spell.chargesMax;
        }
        text.text = mBook.name + "\n " + count;
    }

    void UpdateDisplay(SpellBook book)
    {
        mBook = book;
        string count = "∞";
        if (book != null && book.chargesCurrent != -1)
        {
            ProjectileSpellBook proj = (ProjectileSpellBook)book;
            proj.SpellCasted += UpdateCount;
                count = book.chargesCurrent + " / " + book.chargesMax;
        }
        text.text = book.name + "\n " + count;
    }
}

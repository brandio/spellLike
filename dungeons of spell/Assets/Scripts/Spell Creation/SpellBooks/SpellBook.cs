using UnityEngine;
using System.Collections;

public abstract class SpellBook {
    
    public float coolDown;
	public float cost;
	public float chaos;
    public float chargesMax;
    public float chargesCurrent;
    public string name = "My Spell Book";
    private CharacterLevel characterLevel;

    public void LevelUpUpdateChargers(CharacterLevel level)
    {
        characterLevel = level;
        float oldMax = chargesMax;
        chargesMax = Mathf.Floor(level.GetCost() / cost * 100);
        chargesCurrent += chargesMax - oldMax;
    }

    public int IncreaseCharges(int amount)
    {
        chargesCurrent += amount;
        int chargeAmount = amount;
        if (chargesCurrent >= chargesMax)
        {
			chargeAmount = amount - (int)Mathf.Round(chargesCurrent - chargesMax);
            chargesCurrent = chargesMax;
        }
        return chargeAmount;
    }

    protected void setCharges()
    {
        float costPost = 10;
        if(characterLevel != null)
        {
            costPost = characterLevel.GetCost();
        }

        chargesMax = Mathf.Floor(costPost / cost * 100);


        chargesCurrent = chargesMax;
    }

    public abstract void Cast(Transform transform, bool ParentRotation);
}

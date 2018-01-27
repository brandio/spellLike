using UnityEngine;
using System.Collections;

public class Mana : MonoBehaviour {

	public float startingMana;
	public float manaRegenPerSecond;

	float currentMana;

	public Transform manaBar;

	void Start () {
		currentMana = startingMana;
	}

	void Update()
	{
		currentMana += Time.deltaTime * manaRegenPerSecond;
		if (currentMana > 100) {
			currentMana = 100;
		}
		UpdateManaBar ();
	}
	public bool HasMana(float mana)
	{
		if (currentMana > mana)
			return true;
		return false;
	}

	public void ReduceMana(float mana)
	{
		if (currentMana < mana) {
			Debug.LogError("Not enough mana");
		}
		else
		{
			print (currentMana);
			currentMana -= mana;
		}
	}

	void UpdateManaBar()
	{
		//manaBar.localScale = new Vector3 (currentMana * .05f, .05f, 1);

	}
}

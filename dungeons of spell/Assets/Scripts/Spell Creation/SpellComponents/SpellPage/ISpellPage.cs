using UnityEngine;
using System.Collections;

public interface ISpellPage : ISpellComponent {
	float GetCoolDown();
	float GetDamage();
	float GetChaos();
}

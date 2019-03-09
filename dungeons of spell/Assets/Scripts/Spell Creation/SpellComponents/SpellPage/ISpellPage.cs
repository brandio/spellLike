using UnityEngine;
using System.Collections;

public interface ISpellPage : ISpellComponent {
	float GetCoolDown();
    float GetDamage();
    float GetMoveSpeed();
    float GetCharges();
    float GetReloadTime();
	float GetChaos();
}

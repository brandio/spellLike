using UnityEngine;
using System.Collections;

public interface IInk : ISpellComponent  {
    
    float GetMoveSpeed();
    float GetDamageMod();
    float GetPriority();

    Color GetColor();
    OnHitEffect.DamageBehaviour GetOnHitEffect();
    SpellCollisionEffect.SpellCollisionBehaviour GetOnSpellCollisionEffect();
}

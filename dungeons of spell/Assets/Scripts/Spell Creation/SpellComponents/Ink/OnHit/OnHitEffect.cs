using UnityEngine;
using System.Collections;

public abstract class OnHitEffect  {
    public delegate void DamageBehaviour(GameObject hit, float damage, GameObject caster);

    public abstract DamageBehaviour GetOnHitEffectBehaviour();
}

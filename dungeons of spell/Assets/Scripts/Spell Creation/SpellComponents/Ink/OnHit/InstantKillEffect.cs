using UnityEngine;
using System.Collections;

public class InstantKillEffect : OnHitEffect
{
    void KillHit(GameObject hit, float damage, GameObject caster)
    {
        Health health = hit.GetComponent<Health>();
        if (damage > health.GetPercent() * health.health)
        {
            health.TakeDamage(damage);
        }
    }

    public override DamageBehaviour GetOnHitEffectBehaviour()
    {
        return KillHit;
    }
}

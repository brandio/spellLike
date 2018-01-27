using UnityEngine;
using System.Collections;

public class FireOnHitEffect : OnHitEffect
{
    const float fireChance = 50;
    void FireHit(GameObject hit, float damage, GameObject caster)
    {
        Health health = hit.GetComponent<Health>();
        if (Random.Range(0, 100) < fireChance)
        {
            health.TakeDamageOverTime(damage, 3, .7f);
        }

        health.TakeDamage(damage);
    }

    public override DamageBehaviour GetOnHitEffectBehaviour()
    {
        return FireHit;
    }
}

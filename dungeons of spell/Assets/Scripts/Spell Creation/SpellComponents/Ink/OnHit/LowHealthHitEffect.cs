using UnityEngine;
using System.Collections;

public class LowHealthHitEffect : OnHitEffect
{
    void LowHealthHit(GameObject hit, float damage, GameObject caster)
    {
        if (caster == null)
        {
            caster = GameObject.FindGameObjectWithTag("Player");
        }
        Health playerHealth = caster.GetComponent<Health>();
        hit.GetComponent<Health>().TakeDamage(damage + (1 * playerHealth.GetPercent()));
    }

    public override DamageBehaviour GetOnHitEffectBehaviour()
    {
        return LowHealthHit;
    }
}

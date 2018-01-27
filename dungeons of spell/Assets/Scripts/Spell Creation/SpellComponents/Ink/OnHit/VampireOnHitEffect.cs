using UnityEngine;
using System.Collections;

public class VampireOnHitEffect : OnHitEffect
{
    const float healAmt = 33.3333f;

    void VampireHit(GameObject hit, float damage, GameObject caster)
    {
        if(caster == null)
        {
            caster = GameObject.FindGameObjectWithTag("Player");
        }
        Health health = caster.GetComponent<Health>();
        if(Random.Range(0,100) < healAmt)
        {
            health.Heal((int)Mathf.Ceil(1));
        }
        
        hit.GetComponent<Health>().TakeDamage(damage);
    }

    public override DamageBehaviour GetOnHitEffectBehaviour()
    {
        return VampireHit;
    }
}

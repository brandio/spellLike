using UnityEngine;
using System.Collections;

public class GoldOnHitEffect : OnHitEffect {
    const float goldChance = 20;
    void GoldHit(GameObject hit, float damage, GameObject caster)
    {
        if(Random.Range(0,100) < goldChance + damage)
        {
            hit.GetComponent<CoinDrop>().numberOfCoins += 2;
        }

        hit.GetComponent<Health>().TakeDamage(damage);
    }

    public override DamageBehaviour GetOnHitEffectBehaviour()
    {
        return GoldHit;
    }
}

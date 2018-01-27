using UnityEngine;
using System.Collections;

public class PotionJuiceEffect : SpellCollisionEffect
{

    void HitEffect(GameObject hit, GameObject hitter)
    {
        if (Random.Range(0, 100) < 90)
        {
            int amount = 2;
            GameObject.Find("Player").GetComponent<Health>().Heal(amount);
            

        }
        hit.SetActive(false);
        hitter.SetActive(false);
    }

    public override SpellCollisionEffect.SpellCollisionBehaviour SpellCollisionEffectBehaviour()
    {
        return HitEffect;
    }
}

using UnityEngine;
using System.Collections;

public class EarthEffect : SpellCollisionEffect
{

	// Use this for initialization
	void HitEffect (GameObject hit, GameObject hitter) {
        hit.SetActive(false);
        if(Random.Range(0,100) < 50)
        {
            hitter.SetActive(false);
        }
	}
	
	// Update is called once per frame
	public override SpellCollisionEffect.SpellCollisionBehaviour SpellCollisionEffectBehaviour() {
        return HitEffect;
	}
}

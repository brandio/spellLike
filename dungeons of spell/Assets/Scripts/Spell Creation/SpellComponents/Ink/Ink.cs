using UnityEngine;
using System.Collections;

public class Ink : SpellComponent, IInk {
    public float moveSpeed;
    public float damageMod;
    public float priority;
    public Color color;

    public string onHitEffectName;
    public string onCollisionEffectName;



    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public float GetDamageMod()
    {
        return damageMod;
    }

    public float GetPriority()
    {
        return priority;
    }

    public Color GetColor()
    {
        return color;
    }

    public OnHitEffect.DamageBehaviour GetOnHitEffect()
    {
        /// Debug.LogError(onHitEffectName);
        if (onHitEffectName == "" || onHitEffectName == null) {
            return DefaultDamageBehaviour;
        }

        System.Runtime.Remoting.ObjectHandle handle = System.Activator.CreateInstance(null, onHitEffectName);
        OnHitEffect onHitEffect = handle.Unwrap() as OnHitEffect;
        return onHitEffect.GetOnHitEffectBehaviour();
    }

    public SpellCollisionEffect.SpellCollisionBehaviour GetOnSpellCollisionEffect()
    {
        if (onCollisionEffectName == "" || onCollisionEffectName == null)
        {
            return DefaultSpellCollisionBehaviour;
        }

        System.Runtime.Remoting.ObjectHandle handle = System.Activator.CreateInstance(null, onCollisionEffectName);
        SpellCollisionEffect onCollision = handle.Unwrap() as SpellCollisionEffect;
        return onCollision.SpellCollisionEffectBehaviour();
    }

    void DefaultDamageBehaviour(GameObject hit, float damage, GameObject caster)
	{
        hit.GetComponent<Health>().TakeDamage(damage);
    }

    void DefaultSpellCollisionBehaviour(GameObject hit, GameObject hitter)
    {
        hit.SetActive(false);
    }
	

}

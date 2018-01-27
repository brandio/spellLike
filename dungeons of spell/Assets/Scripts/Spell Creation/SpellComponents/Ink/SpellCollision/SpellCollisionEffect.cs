using UnityEngine;
using System.Collections;

public abstract class SpellCollisionEffect  {

    public delegate void SpellCollisionBehaviour(GameObject hit, GameObject hitter);

    public abstract SpellCollisionBehaviour SpellCollisionEffectBehaviour();
}

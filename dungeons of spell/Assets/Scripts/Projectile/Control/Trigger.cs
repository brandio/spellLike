using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {
	public bool active = false;

	public SpellEvent spellEvent;
	void OnTriggerEnter2D(Collider2D other)
	{	
		if (!active)
			return;

		if((LayerMask.NameToLayer("Enemy") == other.gameObject.layer))
		{

			ProjectileController pc = gameObject.GetComponent<ProjectileController>();
			spellEvent.CallAndWait(pc);
			pc.End ();
		}
	}
}

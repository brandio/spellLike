using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileController : MonoBehaviour {
	public ProjectileMovement pm;
	SpriteRenderer sr;

	public SpellCreationSegment spellSeg;
	public ProjectileSpellBook spell;
	IEnumerator PlayEvents()
	{
		List<SpellEvent> events = spellSeg.GetEvents ();
		for (int i = 0; i < events.Count; i++) {
			yield return new WaitForSeconds (events[i].CallAndWait(this));
            bool projActive = false;
            if(!spellSeg.makePixels)
            {
                projActive = true;
            }
            else
            {
                foreach (Transform child in transform)
                {
                    if (child.gameObject.activeSelf == true)
                    {
                        projActive = true;
                        break;
                    }
                }
            }

			if(!projActive)
			{
				events[events.Count - 1].CallAndWait(this);
				break;
			}
				
		}
	}

	public void End()
	{
		spellSeg.GetEvents () [spellSeg.GetEvents ().Count - 1].CallAndWait (this);
	}

	public void SetLayer(bool player)
	{
		if (player) {

		} else {

		}
	}

	public void turn(float amt)
	{
		pm.turn (amt);
	}

    public void returnToSpell()
	{
		spell.ReturnObjToSpell (gameObject);
	}

	public void SetProjectile(SpellCreationSegment seg, ProjectileSpellBook sp)
	{
		spellSeg = seg;
		if (pm == null) {
			pm = gameObject.GetComponent<ProjectileMovement>();
		}
		pm.speed = seg.GetSpeed ();
		pm.turnSpeed = seg.GetTurnSpeed ();
		spell = sp;
        string[] layers = sp.layerMaskNames;
		foreach(Transform child in transform)
		{
			if(sp.source == ProjectileSpellBookBuilder.spellSource.enemy)
			{
				child.gameObject.layer = LayerMask.NameToLayer("ProjectileEnemy");
			}
			else if(sp.source == ProjectileSpellBookBuilder.spellSource.player)
			{
				child.gameObject.layer = LayerMask.NameToLayer("ProjectilePlayer");
			}
            else
            {
                child.gameObject.layer = LayerMask.NameToLayer("ProjectileNeutral");
            }
			child.gameObject.GetComponent<MovementCheck>().layerMask = LayerMask.GetMask(layers);
		}
	}

	public void Init()
	{
		if(spellSeg == null)
		{
			return;
		}
        StartCoroutine ("PlayEvents");
	}

    void Update()
    {
        //this.transform.position = this.transform.position + transform.right * 1;
    }
}

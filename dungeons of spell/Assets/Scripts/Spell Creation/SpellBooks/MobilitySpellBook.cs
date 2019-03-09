using UnityEngine;
using System.Collections;

public class MobilitySpellBook : SpellBook {
	float range;
	const float baseRange = 20;
	public MobilitySpellBook(MobilitySpellBookBuilder mobilitySpellBookBuilder)
	{
		chaos = mobilitySpellBookBuilder.page.GetChaos ();
		coolDown = mobilitySpellBookBuilder.page.GetCoolDown ();
		range = baseRange * mobilitySpellBookBuilder.page.GetDamage ();
	}

    public override void Cast(Transform trans, bool parentsRotation,Vector3 vec)
    {
        return;
    }
    public override void Cast(Transform trans, bool parentsRotation)
	{
		Vector3 world = Vector3.up;
		if (parentsRotation == false) {
			world = trans.position;
		}
		else
		{

			GameObject camera = GameObject.Find ("Main Camera");
			Vector3 mouse = Input.mousePosition;
			world = camera.GetComponent<Camera>().ScreenToWorldPoint(mouse);
		}

		GameObject player = GameObject.Find ("Player");
		Vector2 offset = Random.insideUnitCircle  * chaos/10;
		if(Vector3.Distance(player.transform.position,world) < range)
		{
			Debug.Log (Vector3.Distance(player.transform.position,world) + " distance " + chaos + " chaos " + offset);
			player.transform.position = (Vector2)world + offset;
			
		}

	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileSpellBook : SpellBook 
{
    public GameObject caster;
	public ProjectileSpellBookBuilder spellBuilder;
	public ProjectileSpellBookBuilder.spellSource source;

    public string[] layerMaskNames;

	Dictionary<SpellCreationSegment,List<GameObject>> openObjs =  new Dictionary<SpellCreationSegment,List<GameObject>>();

    bool returned = false;
    public void ReturnProj()
    {
        returned = true;
        foreach (KeyValuePair<SpellCreationSegment, List<GameObject>> entry in openObjs)
        {
            foreach (GameObject obj in entry.Value)
            {
                ProjectileParentPool.Give(obj);
                List<Transform> objectsToReparent = new List<Transform>();
                foreach (Transform child in obj.transform)
                {
                    objectsToReparent.Add(child);
                }
                foreach(Transform t in objectsToReparent)
                {
                    t.parent = ProjectilePool.projectilePoolInstance.transform;
                    ProjectilePool.Give(t.gameObject);
                }
            }
        }
        openObjs.Clear();
    }

	// Check out  and init projectiles foreach seg
	void CheckOutProj(ProjectileSpellBookBuilder builder)
	{
		foreach (SpellCreationSegment seg in builder.GetSpellSegs()) {
			List<GameObject> gameObjs = new List<GameObject>();
			for(int i = 0; i < 15; i ++)
			{
				GameObject projParent = ProjectileParentPool.Take();
				if(builder.trigger == ProjectileSpellBookBuilder.EventTrigger.Proximity)
				{
					projParent.GetComponent<Trigger>().active = true;
					projParent.GetComponent<Trigger>().spellEvent = builder.spellEvent;
				}
				foreach(SpellPixelFinal spf in seg.GetPixels()) {
                    GameObject projPixel = ProjectilePool.Take ();
					projPixel.transform.parent = projParent.transform;
					projPixel.transform.localPosition = Vector2.zero + spf.m_position * .25f;
					projPixel.transform.rotation = Quaternion.identity;
					projPixel.GetComponent<SpriteRenderer>().color = spf.m_ink.GetColor();
					ProjectileCollision projCollision= projPixel.GetComponent<ProjectileCollision>();
                    projCollision.caster = caster;
                    projCollision.priority = spf.m_ink.GetPriority();
                    projCollision.spellCollisionBehaviour = spf.m_ink.GetOnSpellCollisionEffect();
                    projCollision.damage = spf.damage;
					projCollision.damageBehaviour = spf.m_ink.GetOnHitEffect();
					projCollision.backGroundCollisionBehaviour = builder.lang.GetCollisionBehaviour();
				}
				projParent.GetComponent<ProjectileController>().SetProjectile(seg, this);
                
				gameObjs.Add (projParent);
                projParent.SetActive(false);
            }
			openObjs.Add(seg,gameObjs);
		}
	}

    public delegate void SpellCastEventHandler(SpellBook book);
    public event SpellCastEventHandler SpellCasted;
    bool thrown;
    public override void Cast(Transform trans, bool parentsRotation)
    {
        if(!thrown)
        {
            if(parentsRotation)
            {
                CastFromTrans(trans.position, trans.parent.rotation);
            }
            else
            {
                CastFromTrans(trans.position, trans.rotation);
            }
            
        }
        else
        {
            CastThrown(trans, parentsRotation);
        }
    }

    public override void Cast(Transform trans, bool parentsRotation, Vector3 offset)
    {
        if (!thrown)
        {
            if (parentsRotation)
            {
                CastFromTrans(trans.position + offset, trans.parent.rotation);
            }
            else
            {
                CastFromTrans(trans.position + offset, trans.rotation);
            }

        }
        else
        {
            CastThrown(trans, parentsRotation);
        }
    }

    void CastFromTrans(Vector3 pos, Quaternion rotation)
	{
        if (chargesCurrent == 0 && source == ProjectileSpellBookBuilder.spellSource.player)
        {
            return;
        }
        float random = 0;
		int dist = 5;
		for (int i = 0; i < dist; i++) {
			random += UnityEngine.Random.Range (-chaos/dist, chaos/dist);
		}

        chargesCurrent--;
        
		foreach(KeyValuePair<SpellCreationSegment,List<GameObject>> entry in openObjs)
		{
			List<GameObject> objs = entry.Value;
			GameObject obj = objs[objs.Count - 1];
			objs.RemoveAt(objs.Count - 1);
            obj.SetActive(true);
			obj.transform.position = pos;
            obj.transform.rotation = rotation;
			obj.transform.Rotate(new Vector3(0,0,entry.Key.GetStartingRotation() + random));
            obj.GetComponent<ProjectileController>().Init();
            foreach (Transform child in obj.transform)
			{
				child.gameObject.SetActive (true);
			}
		}
        if(SpellCasted != null)
            SpellCasted(this);
    }

    void CastThrown(Transform trans, bool parentsRotation)
    {
        float range = 20;
        Vector3 world = Vector3.up;
        if (parentsRotation == false)
        {
            world = trans.position;
        }
        else
        {
            GameObject camera = GameObject.Find("Main Camera");
            Vector3 mouse = Input.mousePosition;
            world = camera.GetComponent<Camera>().ScreenToWorldPoint(mouse);
        }

        Vector2 offset = Random.insideUnitCircle * chaos / 10;
        if (Vector3.Distance(trans.position, world) < range)
        {
            CastFromTrans((Vector2)world + offset, trans.rotation);
        }
    }

	public void ReturnObjToSpell(GameObject obj)
	{
        obj.SetActive(false);
        foreach (Transform child in obj.transform)
        {
            child.gameObject.SetActive(false);
        }
        if (returned)
        {
            //Debug.LogError("THIS IS WRONG");
            ProjectileParentPool.Give(obj);
            foreach (Transform child in obj.transform)
            {
                child.parent = ProjectilePool.projectilePoolInstance.transform;
                ProjectilePool.Give(child.gameObject);
            }
        }
        else
        {
            openObjs[obj.GetComponent<ProjectileController>().spellSeg].Add(obj);
        }
	}

	public void Init(ProjectileSpellBookBuilder builder)
	{
        name = builder.name;

        source = builder.source;
        caster = builder.caster;
        if(source == ProjectileSpellBookBuilder.spellSource.player)
        {
            spellBuilder = builder;
            caster = GameObject.FindGameObjectWithTag("Player");
        }
        thrown = builder.GetRune().IsThrown();
        MakeTriggerEvent(builder);

		chaos = builder.page.GetChaos ();
		coolDown = builder.page.GetCoolDown ();
        reloadTime = builder.page.GetReloadTime();
        loadSize = builder.page.GetCharges();
        moveSpeedMod = builder.page.GetMoveSpeed();
        layerMaskNames = builder.GetLayers();
		CheckOutProj (builder);

        if((source == ProjectileSpellBookBuilder.spellSource.player))
        {
            cost = builder.CalculateCost();
            setCharges();
        }
        
    }

	void MakeTriggerEvent(ProjectileSpellBookBuilder spellBookBuilder)
	{
		switch (spellBookBuilder.trigger) 
		{
		case ProjectileSpellBookBuilder.EventTrigger.None:
			break;
		case ProjectileSpellBookBuilder.EventTrigger.Timed:
			foreach(SpellCreationSegment seg in spellBookBuilder.GetSpellSegs())
			{
				seg.RemoveEvent();
				seg.AddEvent(spellBookBuilder.spellEvent);
				seg.AddEvent(new SpellEnd());
			}
			break;
		}
	}

	//public List<SpellCreationSegment> GetSegments()
	//{
	//	return spellSegments;
	//}

}

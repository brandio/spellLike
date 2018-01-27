using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SpellCreationControl : MonoBehaviour {
	
	Camera cam;
    GameObject currentGameObject;
    /*
	SpellGrid spellGrid;
	SpellBookBuilder spellBuilder;
	ISpellPattern pattern;

	public bool isChild = true;

	
	void OpenNewCreation()
	{
		GameObject creation = Instantiate (spellCreation, transform.position, Quaternion.identity) as GameObject;
		creation.GetComponent<SpellCreationControl> ().parent = this;
		this.gameObject.SetActive (false);
	}

	
	public SpellCreationControl parent;
	public GameObject overWorldObject;
	public GameObject creationObject;
	public GameObject playerShoot;
	public GameObject spellCreation;

	[System.Serializable] 
	public struct UiStuff
	{
		public GameObject page;
		public GameObject projectory;
		public GameObject spellGrid;
		public GameObject trigger;
		public GameObject pattern;
		public GameObject play;
		public GameObject proj;
		public GameObject mob;
	}
	public UiStuff uiStuff;

	public void Play()
	{
		/*
		MobilitySpellBookBuilder mob = new MobilitySpellBookBuilder ();
		mob.page = new Bamboo ();
		MobilitySpellBook mobspellbook = new MobilitySpellBook (mob);

		ProjectileSpellBookBuilder builder = new ProjectileSpellBookBuilder (true);
		foreach(SpellCreationSegment  seg in new LongMinePattern().CreateSpellSegs(spellGrid))
		{
			builder.AddSeg(seg);
		}
		builder.page = new PaperPage ();
		builder.projectory = new BounceProjectory ();
		builder.trigger = ProjectileSpellBookBuilder.EventTrigger.Timed;
		builder.spellEvent = new SpellCastEvent(mobspellbook);
		ProjectileSpellBook mySpell = new ProjectileSpellBook (builder);

		Shoot shoot2 = playerShoot.GetComponent<Shoot> ();
		shoot2.current_spell = mySpell;
		overWorldObject.SetActive (true);
		this.gameObject.SetActive (false);
		creationObject.SetActive(false);
		return;
		

		if (spellBuilder is ProjectileSpellBookBuilder) {
			ProjectileSpellBookBuilder projSpellBuilder = (ProjectileSpellBookBuilder)spellBuilder;
            //foreach(SpellCreationSegment  seg in pattern.CreateSpellSegs(spellGrid))
            //{
            //	projSpellBuilder.AddSeg(seg);
            //}
            ProjectileSpellBook spell = projSpellBuilder.MakeSpellBook();
            if (parent == this) {
				Shoot shoot = playerShoot.GetComponent<Shoot> ();
				shoot.current_spell = spell;
				overWorldObject.SetActive (true);
			} else {
				ProjectileSpellBookBuilder projectileSpellBuilder = (ProjectileSpellBookBuilder)parent.spellBuilder;
				projSpellBuilder.spellEvent = new SpellCastEvent(spell);
				parent.gameObject.SetActive(true);
			}
			this.gameObject.SetActive (false);
			creationObject.SetActive(false);
		}
		else if(spellBuilder is MobilitySpellBookBuilder)
		{
			Shoot shoot = playerShoot.GetComponent<Shoot> ();
			shoot.current_spell = new MobilitySpellBook((MobilitySpellBookBuilder)spellBuilder);
			overWorldObject.SetActive (true);
			this.gameObject.SetActive (false);
		}


	}
	
	public void SpellPattern(int i)
	{
        ProjectileSpellBookBuilder projSpellBuilder = (ProjectileSpellBookBuilder)spellBuilder;
        projSpellBuilder.rune = ComponentLoader.instance.LoadComponent(new ComponentLoader.UnLoadedSpellComponent("Straight", SpellComponent.SubSpellComponentType.Rune)) as SpellRune;
        projSpellBuilder.grid = spellGrid;
        switch (i) 
		{
		case 0:
			//pattern = new StraightPattern();
			break;
		case 1:
			pattern = new TwinCurvePattern();
			break;
		case 2:
			pattern = new ShotGunPattern();
			break;
		case 3:
			pattern = new BlowFishPattern();
			break;
		case 4:
			pattern = new PorcupinePattern();
			break;
		case 5:
			//pattern = new LongMinePattern();
			break;
		}
	}
	
	public void Page(int i)
	{

        spellBuilder.page = ComponentLoader.instance.LoadComponent(new ComponentLoader.UnLoadedSpellComponent("Paper", SpellComponent.SubSpellComponentType.Paper)) as SpellPage;
        /*
		switch (i) 
		{
		case 0:
			spellBuilder.page = (PaperPage)new PaperPage();
			break;
		case 1:
			spellBuilder.page = new SilkPage();
			break;
		case 2:
			spellBuilder.page = new Bamboo();
			break;
		case 3:
			spellBuilder.page = new Payprus();
			break;
		}
        
    }


	public void Projectory(int i)
	{
        ProjectileSpellBookBuilder projSpellBuilder = (ProjectileSpellBookBuilder)spellBuilder;
        projSpellBuilder.lang = ComponentLoader.instance.LoadComponent(new ComponentLoader.UnLoadedSpellComponent("English", SpellComponent.SubSpellComponentType.language)) as Language;
        Debug.Log(projSpellBuilder.lang + "land");
        /*
        
		ProjectileSpellBookBuilder projSpellBuilder = (ProjectileSpellBookBuilder)spellBuilder;
		switch (i) 
		{
		case 0:
			projSpellBuilder.projectory = new BasicProjectory();
			break;
		case 1:
			projSpellBuilder.projectory = new BounceProjectory();
			break;
		case 2:
			projSpellBuilder.projectory = new GhostProjectory();
			break;
		case 3:
			projSpellBuilder.projectory = new StickProjectory();
			break;
		}
        
    }

    public void Trigger(int i)
	{
		ProjectileSpellBookBuilder projSpellBuilder = (ProjectileSpellBookBuilder)spellBuilder;
		switch (i) 
		{
		case 0:
			projSpellBuilder.trigger = ProjectileSpellBookBuilder.EventTrigger.None;
			break;
		case 1:
			OpenNewCreation();
			projSpellBuilder.trigger = ProjectileSpellBookBuilder.EventTrigger.Timed;
			break;
		case 2:
			projSpellBuilder.trigger = ProjectileSpellBookBuilder.EventTrigger.Remote;
			break;
		case 3:
			OpenNewCreation();
			projSpellBuilder.trigger = ProjectileSpellBookBuilder.EventTrigger.Proximity;
			break;
		}
	}
	

	// Use this for initialization


	public void SpellType(int i)
	{
		// Teleportation
		if(i == 0)
		{
			spellBuilder = new ProjectileSpellBookBuilder(true);
			if(isChild)
			{
				spellBuilder.page = parent.spellBuilder.page;

				uiStuff.pattern.SetActive(true);
				uiStuff.spellGrid.SetActive(true);
			}
			else
			{

				uiStuff.page.SetActive(true);
				uiStuff.projectory.SetActive(true);
				uiStuff.spellGrid.SetActive(true);
				uiStuff.trigger.SetActive(true);
				uiStuff.pattern.SetActive(true);
			}


			Projectory (0);
			Page (0);
			SpellPattern (0);
			Trigger (0);
		}
		if (i == 1) {
			spellBuilder = new MobilitySpellBookBuilder();
			uiStuff.page.SetActive(true);
			Page (0);
		}
		uiStuff.play.SetActive (true);
		uiStuff.mob.SetActive (false);
		uiStuff.proj.SetActive (false);

		//
	}
    */
    //public SpellGrid grid;
    void Start()
    {
        GameObject camera = GameObject.Find("BookCamera");
        cam = camera.GetComponent<Camera>();
        currentGameObject = this.gameObject;
    }

    void RayCastToMouse()
	{
		Vector3 mouse = Input.mousePosition;
		Vector3 world = cam.ScreenToWorldPoint(mouse);
		world.z = -1;
		
		Vector3 dir = new Vector3(0,0,10);
		RaycastHit2D  hit= Physics2D.Raycast(world,dir);
		if(hit.collider != null && hit.collider.gameObject != currentGameObject)
		{
			currentGameObject = hit.collider.gameObject;
			RayCastResult(hit.collider.gameObject);
		}
	}
	
	void RayCastResult(GameObject hitObject)
	{
		string tag = hitObject.tag;
		switch (tag) 
		{
		case "SpellPixel":
			hitObject.GetComponent<SpellPixel>().ChangeActive();
			break;
		}
	}

	void Update () {
		if (Input.GetMouseButton(0)) {
			RayCastToMouse ();
		}
		if (Input.GetMouseButtonUp(0)) {
			currentGameObject = this.gameObject;
		}
	}
}

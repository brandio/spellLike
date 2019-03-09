using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
    int currentSpell = 0;
    SpellBook[] spells = new SpellBook[2];

    Movement movement; 
    SpriteRenderer render;
	public AudioSource shootSource;
    public AudioSource reloadSource;
    public AudioSource lastShot;
    public GramDrawer reloadGram;
    bool onCoolDown = false;

    SpellBook basicSpell;
    public delegate void SpellChangeHandler(SpellBook currentSpell);
    public event SpellChangeHandler SpellChanged;

    public delegate void ReloadEventHandler();
    public event ReloadEventHandler Reloaded;
    bool reloading = false;
    public void SetNonBasicSpell(SpellBook book, int index)
    {
        spells[index] = book;
        SpellChanged(book);
    }

    public SpellBook GetNonBasicSpell(int index)
    {
        return spells[index];
    }

    void OnSpellChange(bool up)
    {
        if (up)
        {
            if (currentSpell == 1)
            {
                currentSpell = -1;
            }
            currentSpell++;
        }
        else
        {
            currentSpell--;
            if (currentSpell == -1)
            {
                currentSpell = 1;
            }
        }
        SpellChanged(spells[currentSpell]);
        reloadGram.numPoints = (int)spells[currentSpell].loadSize;
    }

    void OnPlayerLevelUp(CharacterLevel level)
    {
        foreach(SpellBook spell in spells)
        {
            if(spell != null)
            {
                spell.LevelUpUpdateChargers(level);
                SpellChanged(spell);
            }
        }
        SpellChanged(spells[currentSpell]);
    }

    void Awake()
    {
        GameObject.Find("Player").GetComponent<CharacterLevel>().LeveledUp += OnPlayerLevelUp;
        if (spells[0] == null)
        {
            NonMonoSpellGrid spellGrid = new NonMonoSpellGrid(4, 4);
            spellGrid.SetPixel(1, 1, true);
            spellGrid.SetPixel(1, 2, true);
            spellGrid.SetPixel(1, 3, true);
            ProjectileSpellBookBuilder builder = new ProjectileSpellBookBuilder(ProjectileSpellBookBuilder.spellSource.player);
            builder.grid = spellGrid;
            builder.lang = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("English", SpellComponent.SubSpellComponentType.language)) as Language;
            builder.SetRune(ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("Raging Badger", SpellComponent.SubSpellComponentType.Rune)) as SpellRune);
            builder.page = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("Paper", SpellComponent.SubSpellComponentType.Paper)) as SpellPage;
            spells[0] = builder.MakeSpellBook();
        }
        if (spells[1] == null)
        {
            NonMonoSpellGrid spellGrid = new NonMonoSpellGrid(4, 4);
            spellGrid.SetPixel(1, 1, true);
            spellGrid.SetPixel(2, 1, true);
            spellGrid.SetPixel(1, 2, true);
            spellGrid.SetPixel(2, 2, true);
            ProjectileSpellBookBuilder builder = new ProjectileSpellBookBuilder(ProjectileSpellBookBuilder.spellSource.player);
            builder.grid = spellGrid;
            builder.lang = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("English", SpellComponent.SubSpellComponentType.language)) as Language;
            builder.SetRune(ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("Bomb", SpellComponent.SubSpellComponentType.Rune)) as SpellRune);
            builder.page = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("Paper", SpellComponent.SubSpellComponentType.Paper)) as SpellPage;
            spells[1] = builder.MakeSpellBook();
        }
        if( basicSpell == null)
        {
            NonMonoSpellGrid spellGrid = new NonMonoSpellGrid(4, 4);
            spellGrid.SetPixel(2, 2, true);
            ProjectileSpellBookBuilder builder = new ProjectileSpellBookBuilder(ProjectileSpellBookBuilder.spellSource.player);
            builder.grid = spellGrid;
            builder.lang = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("English", SpellComponent.SubSpellComponentType.language)) as Language;
            builder.SetRune(ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("Basic", SpellComponent.SubSpellComponentType.Rune)) as SpellRune);
            builder.page = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("Paper", SpellComponent.SubSpellComponentType.Paper)) as SpellPage;
            basicSpell = builder.MakeSpellBook();
        }
        SpellChanged(spells[currentSpell]);
    }
	void Start()
	{
        currentSpell = 0;
        render = GetComponent<SpriteRenderer> ();
		//shootSource = this.GetComponent<AudioSource> ();
		movement = this.transform.parent.parent.gameObject.GetComponent<Movement> ();
        reloadGram.numPoints = (int)spells[currentSpell].loadSize;
    }

	IEnumerator SetCoolDown(bool isBasicSpell = false)
	{
        onCoolDown = true;
        float coolDown = 0;
        coolDown = isBasicSpell ? basicSpell.coolDown : spells[currentSpell].coolDown;
        coolDown = spells[currentSpell].coolDown;

        StartCoroutine(Fade(coolDown));
        yield return new WaitForSeconds(coolDown);
        onCoolDown = false;
    }

	IEnumerator Fade(float coolDown)
	{
		float i = 0;
		while( i < coolDown)
		{
			i = i + Time.deltaTime;
			float percent = i/ coolDown;
			render.color = new Color (percent, percent, percent);
			yield return null;
		}
	}

	void Update () 
    {
        if (!reloading)
        {
            if (Input.GetMouseButton(1) && !onCoolDown && Controls.GetInstance().active)
            {
                FireBasic();
            }
            else if (Input.GetMouseButton(0) && !onCoolDown && Controls.GetInstance().active)
            {
                if (spells[currentSpell].NeedReload())
                {
                    Reload();
                    return;
                }
                Fire();
            }
            else if(Input.GetKeyDown(KeyCode.R))
            {
                Reload();

            }
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                OnSpellChange(Input.GetAxis("Mouse ScrollWheel") > 0);
            }
        }
        else
        {
            if(Time.time > timeTillReload)
            {
                DoneReloading();
            }
        }
    }

    void Fire()
	{
        shoot(spells[currentSpell]);
        StartCoroutine(SetCoolDown());
    }

    float timeTillReload = 0;
    void Reload()
    {
        reloadGram.gameObject.SetActive(true);
        reloadGram.DrawOverTime(Time.time + spells[currentSpell].reloadTime);
        spells[currentSpell].Reload();
        timeTillReload = Time.time + spells[currentSpell].reloadTime;
        reloading = true;
    }

    void DoneReloading()
    {
        reloadSource.Stop();
        reloading = false;
        if(Reloaded != null)
            Reloaded();
        reloadSource.Play();
        //reloadGram.gameObject.SetActive(false);
    }
    void FireBasic()
    {
        shoot(basicSpell);
        StartCoroutine(SetCoolDown(true));
    }

    public void shoot(SpellBook spell)
	{
        //ScreenShake.instance.shake (.01f * spells[currentSpell].cost);
        ScreenShake.instance.shake(.2f);
        if(spell.chargesCurrent == spell.nextChargesTillReload + 1)
        {
            lastShot.Play();
        }
        else
        {
            shootSource.pitch = Random.Range(.992f, 1.08f);
            shootSource.Play();

        }
        spell.Cast (transform, true);
	}

    public float GetSpeedMod()
    {
        return spells[currentSpell].moveSpeedMod;
    }

    public void RechargeSpells()
    {
        foreach (SpellBook book in spells)
        {
            book.IncreaseCharges(500);
        }
    }

}

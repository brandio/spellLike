using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
    int currentSpell = 0;
    SpellBook[] spells = new SpellBook[2];

    Movement movement; 
    SpriteRenderer render;
	AudioSource audioSource;
	bool onCoolDown = false;

    SpellBook basicSpell;
    public delegate void SpellChangeHandler(SpellBook currentSpell);
    public event SpellChangeHandler SpellChanged;

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
        if(up)
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
            builder.page = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("Bamboo", SpellComponent.SubSpellComponentType.Paper)) as SpellPage;
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
		audioSource = this.GetComponent<AudioSource> ();
		movement = this.transform.parent.parent.gameObject.GetComponent<Movement> ();
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

	void Update () {

		if (Input.GetMouseButton(1) && !onCoolDown && Controls.GetInstance().active)
        {
            FireBasic();
        }
		if (Input.GetMouseButton(0) && !onCoolDown && Controls.GetInstance().active)
        {
            Fire();
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            OnSpellChange(Input.GetAxis("Mouse ScrollWheel") > 0);
        }
    }
	
	void Fire()
	{
        shoot(spells[currentSpell]);
        StartCoroutine(SetCoolDown());
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
        audioSource.Play ();
		spell.Cast (transform, true);
	}

    public void RechargeSpells()
    {
        foreach (SpellBook book in spells)
        {
            book.IncreaseCharges(500);
        }
    }

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SpellCreationInterface : MonoBehaviour {
    public static SpellCreationInterface instance;

    [System.Serializable] 
	public struct UiStuff
	{
		public SpellGrid spellGrid;
        public BookUiMaster uiMaster;
    }

	public UiStuff uiStuff;
	public ProjectileSpellBookBuilder projSpellBookBuilder;
    public void NameChanged(string name)
    {
        projSpellBookBuilder.name = name;
    }

    void RuneChanged(string rune)
    {
        SpellRune spellRune = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent(rune, SpellComponent.SubSpellComponentType.Rune)) as SpellRune;
        Debug.Log(spellRune.GetTitle() + "RUNE NAME");
        projSpellBookBuilder.SetRune(spellRune);
    }

    void LangChanged(string language)
    {
        Language lang = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent(language, SpellComponent.SubSpellComponentType.language)) as Language;
        projSpellBookBuilder.lang = lang;
    }

    void PageChanged(string paper)
    {
        SpellPage page = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent(paper, SpellComponent.SubSpellComponentType.Paper)) as SpellPage;
        projSpellBookBuilder.page = page;
    }

    void InkChanged(string grid)
    {
        Ink ink = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent(grid, SpellComponent.SubSpellComponentType.Ink)) as Ink;
        uiStuff.spellGrid.SetInk (ink);
    }

    public GameObject playerShoot;
    public GameObject overWorldObject;

    public void Play()
    {
        ProjectileSpellBook mySpell = projSpellBookBuilder.MakeSpellBook();
        Shoot shoot = playerShoot.GetComponent<Shoot>();
        if(basic)
        {
            shoot.SetNonBasicSpell(mySpell,0);
        }
        else
        {
            shoot.SetNonBasicSpell(mySpell,1);
        }
        
        overWorldObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public delegate void SpellInterfaceOpenEventHandler(ProjectileSpellBookBuilder builder, ProjectileSpellBookBuilder newBuilder, SpellGrid grid);
    public event SpellInterfaceOpenEventHandler SpellInterfaceOpened;
    bool basic;
    int number;

    void Awake () 
	{
        if(instance != null)
        {
            Debug.Log("YOU CAN ONLY HAVE ONE INSTance");
        }
        instance = this;
        SpellInterfaceOpened += uiStuff.uiMaster.InitBook;
        uiStuff.uiMaster.ComponentSelected += ComponenetSelected;
        uiStuff.uiMaster.nameInput.onEndEdit.AddListener(NameChanged);
        gameObject.SetActive(false);
    }

    void ComponenetSelected (SpellComponent.SubSpellComponentType type, string component)
    {
        switch(type)
        {
            case SpellComponent.SubSpellComponentType.Rune:
                RuneChanged(component);
                break;
            case SpellComponent.SubSpellComponentType.language:
                LangChanged(component);
                break;
            case SpellComponent.SubSpellComponentType.Paper:
                PageChanged(component);
                break;
            case SpellComponent.SubSpellComponentType.Ink:
                InkChanged(component);
                break;
        }
    }

    public void Open (bool basicSpell, int spellNumber) {
        gameObject.SetActive(true) ;
        basic = basicSpell;
        number = spellNumber;
        // Get the old builder to set values on the new book
        ProjectileSpellBook book = null;
        if (basicSpell)
        {
            book = playerShoot.GetComponent<Shoot>().GetNonBasicSpell(0) as ProjectileSpellBook; 
        }
        else
        {
            book = playerShoot.GetComponent<Shoot>().GetNonBasicSpell(1) as ProjectileSpellBook;
        }
        book.ReturnProj();
        ProjectileSpellBookBuilder builder = book.spellBuilder;
        uiStuff.spellGrid.SetGrid(builder.grid.GetGrid());
        // Set our builder to the new builder
        projSpellBookBuilder = new ProjectileSpellBookBuilder (ProjectileSpellBookBuilder.spellSource.player);

        // Clear and set the grid
		//uiStuff.spellGrid.ClearGrid ();
        projSpellBookBuilder.grid = uiStuff.spellGrid;

        if(SpellInterfaceOpened != null)
        {
            SpellInterfaceOpened(builder, projSpellBookBuilder, uiStuff.spellGrid);
        }        
    }
}

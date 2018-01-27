using UnityEngine;
using System.Collections;

public class Melee : MonoBehaviour {

    public void Start()
    {
        EnemyShoot shoot = this.GetComponent<EnemyShoot>();
        NonMonoSpellGrid grid = new NonMonoSpellGrid(6, 6);
        Ink ink = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("SootExtract", SpellComponent.SubSpellComponentType.Ink)) as Ink;
        grid.SetInk(ink);
        grid.SetPixel(2, 2, true);
        grid.SetPixel(2, 3, true);
        grid.SetPixel(3, 2, true);
        grid.SetPixel(3, 3, true);

        ProjectileSpellBookBuilder spellBuilder = new ProjectileSpellBookBuilder(ProjectileSpellBookBuilder.spellSource.enemy);
        spellBuilder.grid = grid;
        spellBuilder.page = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("Cotton", SpellComponent.SubSpellComponentType.Paper)) as SpellPage;
        spellBuilder.lang = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("English", SpellComponent.SubSpellComponentType.language)) as Language;
        spellBuilder.SetRune(ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("BullFrog", SpellComponent.SubSpellComponentType.Rune)) as SpellRune);
        shoot.current_spell = spellBuilder.MakeSpellBook();
        shoot.coolDown = spellBuilder.page.GetCoolDown();
        Debug.Log("Cooldown " + shoot.coolDown);
    }
}

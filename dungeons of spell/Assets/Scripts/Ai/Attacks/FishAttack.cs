using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyShoot))]
public class FishAttack : MonoBehaviour 
{
    public void Start()
    {
        EnemyShoot shoot = this.GetComponent<EnemyShoot>();
        NonMonoSpellGrid grid = new NonMonoSpellGrid(6, 6);
        Ink ink = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("Water", SpellComponent.SubSpellComponentType.Ink)) as Ink;
        grid.SetInk(ink);
        grid.SetPixel(2, 2, true);
        grid.SetPixel(2, 3, true);
        grid.SetPixel(3, 2, true);
        grid.SetPixel(3, 3, true);


        ProjectileSpellBookBuilder spellBuilder = new ProjectileSpellBookBuilder(ProjectileSpellBookBuilder.spellSource.enemy);
        spellBuilder.caster = gameObject;
        spellBuilder.grid = grid;
        spellBuilder.page = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("Paper", SpellComponent.SubSpellComponentType.Paper)) as SpellPage;
        spellBuilder.lang = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("GlurpleTongue", SpellComponent.SubSpellComponentType.language)) as Language;
        spellBuilder.SetRune(ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("AlphaStar", SpellComponent.SubSpellComponentType.Rune)) as SpellRune);
        shoot.current_spell = spellBuilder.MakeSpellBook();
        shoot.coolDown = spellBuilder.page.GetCoolDown();
    }
}

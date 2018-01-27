using UnityEngine;
using System.Collections;
[RequireComponent(typeof(EnemyShoot))]
public class SporeBurst : MonoBehaviour
{

    public void Start()
    {
        EnemyShoot shoot = this.GetComponent<EnemyShoot>();
        NonMonoSpellGrid grid = new NonMonoSpellGrid(6, 6);
        Ink ink = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("Spore Oil", SpellComponent.SubSpellComponentType.Ink)) as Ink;
        grid.SetInk(ink);
        grid.SetPixel(2, 2, true);
        grid.SetPixel(1, 2, true);
        grid.SetPixel(2, 1, true);
        grid.SetPixel(1, 1, true);

        ProjectileSpellBookBuilder spellBuilder = new ProjectileSpellBookBuilder(ProjectileSpellBookBuilder.spellSource.neutral);
        spellBuilder.caster = gameObject;
        spellBuilder.grid = grid;
        spellBuilder.page = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("Paper", SpellComponent.SubSpellComponentType.Paper)) as SpellPage;
        spellBuilder.lang = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("English", SpellComponent.SubSpellComponentType.language)) as Language;
        spellBuilder.SetRune(ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("Spore Burst", SpellComponent.SubSpellComponentType.Rune)) as SpellRune);
        shoot.current_spell = spellBuilder.MakeSpellBook();
        shoot.coolDown = spellBuilder.page.GetCoolDown();
    }
}

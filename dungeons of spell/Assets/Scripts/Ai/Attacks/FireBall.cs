using UnityEngine;
using System.Collections;
[RequireComponent (typeof (EnemyShoot)) ]
public class FireBall : MonoBehaviour {

    public void Start()
    {
        EnemyShoot shoot = this.GetComponent<EnemyShoot>();
        NonMonoSpellGrid grid = new NonMonoSpellGrid(6, 6);
        Ink ink = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("FireWeedSap", SpellComponent.SubSpellComponentType.Ink)) as Ink;
        grid.SetInk(ink);
        grid.SetPixel(2, 2, true);
        grid.SetPixel(2, 3, true);
        grid.SetPixel(3, 2, true);
        grid.SetPixel(3, 3, true);
        grid.SetPixel(4, 4, true);
        grid.SetPixel(3, 4, true);
        grid.SetPixel(4, 3, true);
        grid.SetPixel(2, 4, true);
        grid.SetPixel(4, 2, true);

        grid.SetPixel(3, 1, true);
        grid.SetPixel(3, 5, true);
        grid.SetPixel(1, 3, true);
        grid.SetPixel(5, 3, true);

        ProjectileSpellBookBuilder spellBuilder = new ProjectileSpellBookBuilder(ProjectileSpellBookBuilder.spellSource.enemy);
        spellBuilder.caster = gameObject;
        spellBuilder.grid = grid;
        spellBuilder.page = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("Paper", SpellComponent.SubSpellComponentType.Paper)) as SpellPage;
        spellBuilder.lang = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("English", SpellComponent.SubSpellComponentType.language)) as Language;
        spellBuilder.SetRune(ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("Raging Badger", SpellComponent.SubSpellComponentType.Rune)) as SpellRune);
        shoot.current_spell = spellBuilder.MakeSpellBook();
        shoot.coolDown = spellBuilder.page.GetCoolDown();
    }
}

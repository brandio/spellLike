using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Rain : ISpellPattern
{

    public SpellBook SpellBookRain(ISpellGrid fgrid)
    {
        NonMonoSpellGrid grid = new NonMonoSpellGrid(6, 6);
        Ink ink = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("FireWeedSap", SpellComponent.SubSpellComponentType.Ink)) as Ink;
        grid.SetInk(ink);
        grid.SetPixel(2, 2, true);
        grid.SetPixel(2, 3, true);
        grid.SetPixel(2, 4, true);


        ProjectileSpellBookBuilder spellBuilder = new ProjectileSpellBookBuilder(ProjectileSpellBookBuilder.spellSource.player);
        //spellBuilder.caster = ;
        spellBuilder.grid = grid;
        spellBuilder.page = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("Paper", SpellComponent.SubSpellComponentType.Paper)) as SpellPage;
        spellBuilder.lang = ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("English", SpellComponent.SubSpellComponentType.language)) as Language;
        spellBuilder.SetRune(ComponentLoader.GetInstance().LoadComponent(new ComponentLoader.UnLoadedSpellComponent("Raging Badger", SpellComponent.SubSpellComponentType.Rune)) as SpellRune);
        return spellBuilder.MakeSpellBook();
    }

    public List<SpellCreationSegment> CreateSpellSegs(ISpellGrid grid, float damageMod)
    {
        List<SpellCreationSegment> segs = new List<SpellCreationSegment>();
        Vector2 maxRange = new Vector2(2.1f, 2.1f);
        Vector2 minRange = new Vector2(-2.1f, -2.1f);

        SpellBook book = SpellBookRain(grid);
        SpellCreationSegment seg = new SpellCreationSegment();

        seg.makePixels = false;

        seg.AddEvent(new SpeedChange(.001f, seg));
        seg.AddEvent(new SpellFaceEvent(new Vector3(0, 0, -90), seg));
        for (int i = 0; i < 8; i++)
        {
            seg.AddEvent(new SpellCastEvent(book, minRange, maxRange));
            seg.AddEvent(new Wait(0.25f, 1f, seg));

        }
        seg.AddEvent(new SpellEnd());
        segs.Add(seg);

        return segs;
    }
}

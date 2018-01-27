using UnityEngine;
using System.Collections;

public class Language : SpellComponent, ILanguage {

    IProjectory projectory;
    public string projectoryString;
    public string[] layers;
    public string[] GetLayers()
    {
        return layers;
    }

    IProjectory GetProjectory()
    {
        System.Runtime.Remoting.ObjectHandle handle = System.Activator.CreateInstance(null, projectoryString);
        IProjectory projectory = handle.Unwrap() as IProjectory;
        return projectory;
    }

    public ProjectileCollision.BackGroundCollisionBehaviour GetCollisionBehaviour()
    {
        if(projectory == null)
        {
            projectory = GetProjectory();
        }
        return projectory.OnBackGroundCollision;
    }
}

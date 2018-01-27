using UnityEngine;
using System.Collections;

public interface ILanguage : ISpellComponent  {
    string[] GetLayers();
    ProjectileCollision.BackGroundCollisionBehaviour GetCollisionBehaviour();
}

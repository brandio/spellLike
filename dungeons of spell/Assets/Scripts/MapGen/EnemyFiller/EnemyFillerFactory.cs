using UnityEngine;
using System.Collections;

public class EnemyFillerFactory  {

    public IEnemyFiller MakeEnemyFiller()
    {
        return new BasicEnemyFiller();
    }
}

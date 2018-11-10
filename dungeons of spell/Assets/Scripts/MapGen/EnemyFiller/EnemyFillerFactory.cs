using UnityEngine;
using System.Collections;

public class EnemyFillerFactory  {

    public enum EnemyType {Default,Lake,Boss }
    public IEnemyFiller MakeEnemyFiller()
    {
        return new BasicEnemyFiller();
    }

    public IEnemyFiller MakeEnemyFiller(EnemyType type)
    {
        if(type == EnemyType.Boss)
        {
            return new BossFiller();
        }

        return new BasicEnemyFiller();
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public interface IEnemyFiller {

    List<GameObject> MakeEnemies(float roomSize, Room room);
}

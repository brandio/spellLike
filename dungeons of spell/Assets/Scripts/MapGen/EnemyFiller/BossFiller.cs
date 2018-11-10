using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BossFiller : IEnemyFiller 
{

    public List<GameObject> MakeEnemies(float roomSize, Room r)
    {
        List<GameObject> enemies = new List<GameObject>();

        string delim = System.IO.Path.DirectorySeparatorChar.ToString();
        GameObject enemy = GameObject.Instantiate(Resources.Load("Enemy" + delim + "Boss") as GameObject, Vector2.zero, Quaternion.identity) as GameObject;
        enemy.SetActive(false);
        enemies.Add(enemy);
        enemy.GetComponent<AiMovement>().room = r;

        return enemies;
    }
}

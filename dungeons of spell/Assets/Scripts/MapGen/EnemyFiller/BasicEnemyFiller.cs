using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BasicEnemyFiller : IEnemyFiller {

    const float ENEMIES_PER_ROOMSIZE = .015f;
    public List<GameObject> MakeEnemies(float roomSize, Room r)
    {
        
        List<GameObject> enemies = new List<GameObject>();
        for (int i = 0; i < roomSize * ENEMIES_PER_ROOMSIZE; i++)
        {
            if(Random.Range(0,100) > 0)
            {
                string delim = System.IO.Path.DirectorySeparatorChar.ToString();
                GameObject enemy = GameObject.Instantiate(Resources.Load("Enemy" + delim + "Skull Kandy") as GameObject, Vector2.zero, Quaternion.identity) as GameObject;
                enemy.SetActive(false);
                enemies.Add(enemy);
                enemy.GetComponent<AiMovement>().room = r;
            }
            else
            {
                string delim = System.IO.Path.DirectorySeparatorChar.ToString();
                GameObject enemy = GameObject.Instantiate(Resources.Load("Enemy" + delim + "Fish") as GameObject, Vector2.zero, Quaternion.identity) as GameObject;
                enemy.SetActive(false);
                enemies.Add(enemy);
                enemy.GetComponent<AiMovement>().room = r;
            }
        }
        return enemies;
    }
}

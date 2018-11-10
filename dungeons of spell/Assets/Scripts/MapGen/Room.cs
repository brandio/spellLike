using System;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Room : MonoBehaviour
{
   // public int X;
	//public int Y;
	//public int depth;
    //public enum RoomObj{Wall,Floor};
	//IRoomFiller filler;
	public GameObject floor;

	public List<PathFindingNode> pathFindingNodes;
    public List<GameObject> enemies;

    bool active = false;
    [HideInInspector]
    public bool bossRoom = false;
    public Vector3 center;

    public bool IsCleared()
    {
        if(enemies.Count == 0)
        {
            return true;
        }
        return false;
    }

    public void Activate()
    {
        if (active)
        {
            return;
        }
        else
        {
            active = !active;
        }
        if(RoomEntered != null)
        {
            RoomEntered(this);
        }
        
        placeEnemies();
        if(enemies.Count == 0)
        {
            RoomCleared(this);
        }
    }

    public void DeActivate()
    {
        if (!active)
        {
            return;
        }
        else
        {
            active = !active;
        }
        foreach(GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }
    }
    
    void EnemyDieEventHandler(Health enemy)
    {
        enemies.Remove(enemy.gameObject);
        if(enemies.Count == 0)
        {
            if(RoomCleared != null)
            {
                RoomCleared(this);
            }
        }
    }

    public delegate void RoomClearedEventHandler(Room r);
    public event RoomClearedEventHandler RoomCleared;

    public delegate void RoomEnterEventHandler(Room r);
    public event RoomEnterEventHandler RoomEntered;

    void placeEnemies()
    {
        if(enemies == null)
        {
            enemies = new List<GameObject>();
        }

        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
            enemy.GetComponent<Health>().EnemyDied += EnemyDieEventHandler;
            GameObject player = GameObject.Find("Player");
            bool posBad = true;
            int i = 0;
            if(bossRoom)
            {
                enemy.transform.position = center;
                posBad = false;
            }
            while (posBad)
            {
                i++;
                enemy.transform.position = pathFindingNodes[UnityEngine.Random.Range(0, pathFindingNodes.Count)].position + new Vector2(UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(-3f, 3f));
                if (enemy.GetComponent<MovementCheck2>().IsPositionClear(enemy.transform.position) && Vector2.Distance(enemy.transform.position, player.transform.position) > 12)
                {
                    posBad = false;
                }
                if(i > 10)
                {
                    posBad = false;
                }
            }
        }
    }

}

using UnityEngine;
using System.Collections;

public class EnemtSpawner : MonoBehaviour {
	public Maze m;
	public GameObject enemy;
	public float spawnRate = 10;
	public float timer = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer = timer + Time.deltaTime;
		if (timer > spawnRate) {
			while(true)
			{
				int X = Random.Range (1 , m.sizeX - 1);
				int Y = Random.Range (1 , m.sizeY - 1);
				if(m.maze[X,Y] == 1)
				{
					timer = 0;
					Instantiate(enemy,new Vector3(X * 2, Y * 2, 0),Quaternion.identity);
					break;
				}
			}
		}
	}
}

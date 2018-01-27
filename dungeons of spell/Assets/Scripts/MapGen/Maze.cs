using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour {

	public int sizeX;
	public int sizeY;
	[HideInInspector]
	public int[,] maze;

	public int drunkenRunSize;
	public int drunkenRunAmt;
	public GameObject wall;
	public GameObject floor;
	public struct intPair
	{
		public intPair(int j, int k)
		{
			x = j;
			y = k;
		}
		public int x;
		public int y;
	}
		
	void DrunkRun()
	{
		for (int j = 0; j < drunkenRunAmt; j++) {
			intPair current = new intPair (Random.Range (1, sizeX - 1), Random.Range (1, sizeY - 1));
			for (int i = 0; i < drunkenRunSize; i++) {
				maze [current.x, current.y] = 1;
				int direction = Random.Range (0, 4);
				intPair pairNew = new intPair (current.x, current.y);
				if (direction == 0)
					pairNew.x = current.x - 1;
				else if (direction == 1)
					pairNew.y = current.y + 1;
				else if (direction == 2)
					pairNew.x = current.x + 1;
				else if (direction == 3)
					pairNew.y = current.y - 1;

				if (IsInRange (pairNew)) {
					current = pairNew;
				}
			}
		}
	}

	public List<intPair> GetNeighbors(intPair pair)
	{
		List<intPair> neighbors = new List<intPair> ();
		intPair left = new intPair (pair.x - 1, pair.y);
		intPair right = new intPair (pair.x + 1, pair.y);
		intPair up = new intPair (pair.x, pair.y + 1);
		intPair down = new intPair (pair.x, pair.y - 1);
		if (IsInRange (left)) {
			neighbors.Add (left);
		}
		if (IsInRange (right)) {
			neighbors.Add (right);
		}
		if (IsInRange (up)) {
			neighbors.Add (up);
		}
		if (IsInRange (down)) {
			neighbors.Add (down);
		}
		return neighbors;
	}


	public List<intPair> GetNeighborsCorner(intPair pair)
	{
		List<intPair> neighbors = new List<intPair> ();
		intPair left = new intPair (pair.x - 1, pair.y -1);
		intPair right = new intPair (pair.x + 1, pair.y + 1);
		intPair up = new intPair (pair.x - 1, pair.y + 1);
		intPair down = new intPair (pair.x +1, pair.y - 1);
		if (IsInRange (left)) {
			if(maze[pair.x - 1, pair.y] == 1 || maze[pair.x , pair.y -1] == 1)
			{
				neighbors.Add (left);
			}
		}
		if (IsInRange (right)) {
			if(maze[pair.x + 1, pair.y] == 1 || maze[pair.x , pair.y + 1] == 1)
			{
				neighbors.Add (right);
			}
		}
		if (IsInRange (up)) {
			if(maze[pair.x - 1, pair.y] == 1 || maze[pair.x , pair.y + 1] == 1)
			{
				neighbors.Add (up);
			}
		}
		if (IsInRange (down)) {
			if(maze[pair.x + 1, pair.y] == 1 || maze[pair.x , pair.y - 1] == 1)
			{
				neighbors.Add (down);
			}
		}
		neighbors.AddRange (GetNeighbors (pair));
		return neighbors;
	}

	public void GenerateMaze()
	{
		maze = new int[sizeX,sizeY];
		Stack stack = new Stack();
		intPair start = new intPair (sizeX / 2, sizeY / 2);
		if (start.x % 2 == 0) {
			start.x = start.x - 1;
		}
		if (start.y % 2 == 0) {
			start.y = start.y - 1;
		}
		Random.Range (0, 4);
		stack.Push (start);
		while (stack.Count > 0) {
			intPair currentTile = (intPair)stack.Peek();
			maze[currentTile.x,currentTile.y] = 1;
			if(IsDeadEnd(currentTile))
			{
				stack.Pop();
				continue;
			}
			intPair newTile = new intPair (currentTile.x, currentTile.y);
			int direction = 0;
			while (true) {
				newTile.x = currentTile.x;
				newTile.y = currentTile.y;
				// Get a direction
				// 0 = left, 1 = up, 2 = right, distance = down
				direction = Random.Range (0, 4);
				
				if (direction == 0)
					newTile.x = currentTile.x - distance;
				else if (direction == 1)
					newTile.y = currentTile.y + distance;
				else if (direction == 2)
					newTile.x = currentTile.x + distance;
				else if (direction == 3)
					newTile.y = currentTile.y - distance;
				
				if(IsAWall(newTile))
				{
					break;
				}
			}

			if (direction == 0)
			{
				//stack.Push(new intPair(currentTile.x - 1, currentTile.y));
				for(int i = 1; i < distance; i++)
				{
					maze[currentTile.x - i, currentTile.y] = 1;
				}
			}
			else if (direction == 1)
			{
				for(int i = 1; i < distance; i++)
				{
					maze[currentTile.x, currentTile.y + i] = 1;
				}
			}
			else if (direction == 2)
			{
				for(int i = 1; i < distance; i++)
				{
					maze[currentTile.x + i, currentTile.y] = 1;
				}
			}
			else if (direction == 3)
			{
				for(int i = 1; i < distance; i++)
				{
					maze[currentTile.x , currentTile.y - i] = 1;
				}
			}
			stack.Push(newTile);

		}
		//perlinNoise ();
		DrunkRun ();
		DrawMaze ();
	}

	void DrawMaze()
	{

		for(int x = 0; x < sizeX; x++)
		{
			for(int y = 0; y < sizeY; y++)
			{
				if(maze[x,y] == 0)
				{
					int rnd = Random.Range (0,2);
					GameObject treeObj = (GameObject)Instantiate(wall,Vector3.zero,Quaternion.identity);
					treeObj.transform.parent = transform;
					treeObj.transform.localPosition = new Vector3(x * 4, y * 4);
					if(rnd == 1)
					{
						treeObj.transform.localScale = new Vector3(treeObj.transform.localScale.x * -1 , treeObj.transform.localScale.y,treeObj.transform.localScale.z);
					}
				}
				else
				{
					//Instantiate(floor,new Vector3(x * 4,y * 4),Quaternion.identity);
					GameObject floorobj = (GameObject) Instantiate(floor,Vector3.zero,Quaternion.identity);
					floorobj.transform.parent = transform;
					floorobj.transform.localPosition = new Vector3(x * 4, y * 4);
				}
			}
		}
	}

	bool IsAWall(intPair pair)
	{
		if (IsInRange(pair) && maze [pair.x, pair.y] == 0)
			return true;
		return false;
	}
	bool IsInRange(intPair pair)
	{
		if (pair.x >= sizeX - 1 || pair.x < 1) {
			return false;
		}
		if (pair.y >= sizeY -1 || pair.y < 1) {
			return false;
		}
		return true;
	}
	public int distance = 2;
	bool IsDeadEnd(intPair pair)
	{
		intPair up = new intPair (pair.x, pair.y + distance);
		intPair down = new intPair (pair.x, pair.y - distance);
		intPair left = new intPair (pair.x - distance, pair.y);
		intPair right = new intPair (pair.x + distance, pair.y);
		bool d = (IsAWall (up) || IsAWall (down) || IsAWall (left) || IsAWall (right));
		return(!d);
	}
	

	// Use this for initialization
	void Start () {
		//GenerateMaze ();
	}
}

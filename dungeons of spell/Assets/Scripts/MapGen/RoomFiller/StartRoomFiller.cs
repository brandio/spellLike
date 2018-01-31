using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class StartRoomFiller : IRoomFiller
{
    public List<Vector2> doors;

    protected int sizeX;
	protected int sizeY;
	protected Transform transform;
	protected Vector2 maxEdges;
    protected Vector2 minEdges;
    protected string[,] grid;
	protected Room room;
    protected const int tileSize = 2;

    public struct IntPair : IEquatable<IntPair>
    {
        public IntPair(int j, int k)
        {
            x = j;
            y = k;
        }
        public int x;
        public int y;

        bool IEquatable<IntPair>.Equals(IntPair other)
        {
            if (x == other.x && y == other.y)
                return true;
            return false;
        }
    }

    virtual protected bool IsInRange(IntPair pair)
    {
        if (pair.x >= sizeX - 1 || pair.x < 0)
        {
            return false;
        }
        if (pair.y >= sizeY - 1 || pair.y < 0)
        {
            return false;
        }
        return true;
    }

    public StartRoomFiller(Vector2 pos, int x, int y, Room r, List<Vector2> ds)
	{
        room = r;
		transform = room.transform;
		transform.position = pos;
       
		sizeX = x;
		sizeY = y;
		grid = new string[sizeX,sizeY];
        for(int i = 0; i < grid.GetLength(0); i++)
        {
            for(int j = 0; j < grid.GetLength(1); j++)
            {
                grid[i, j] = "O";
            }
        }
        minEdges = new Vector2(transform.position.x, transform.position.y);
        maxEdges = new Vector2 (sizeX * tileSize + transform.position.x, sizeY * tileSize + transform.position.y);
        room.pathFindingNodes = new List<PathFindingNode> ();
        doors = ds;
        foreach(Vector2 doorPos in doors)
        {
            int xPos = (int)(doorPos.x - transform.position.x) / tileSize;
            int yPos = (int)(doorPos.y - transform.position.y) / tileSize;
            grid[xPos, yPos] = "R";
        }
	}

	public void AddDoor(Vector2 pos)
	{
        int x = (int)(pos.x - transform.position.x) / tileSize;
        //Debug.Log(pos.x + " " + transform.position.x + " x size");
        int y = (int)(pos.y - transform.position.y) / tileSize;
        //Debug.Log(pos.y + " " + transform.position.y + " y size");
        doors.Add(pos);
        
        grid[x,y] = "R";
	}

	public GameObject MakeFloor()
	{
		// Create the floor
		GameObject floor = GameObject.Instantiate (Resources.Load("Floor") as GameObject) as GameObject;
		floor.transform.parent = transform;
		floor.transform.localScale = new Vector3(sizeX * 2 ,sizeY * 2);
		floor.transform.localPosition = new Vector3(sizeX - 1,sizeY - 1);
		room.floor = floor;
		return floor;
	}

	void Lake()
	{
		for (int i = 10; i < 14; i++) {
			for (int j = 10; j < 14; j++) {
				grid [i, j] = "M";
			}
		}

	}
	public virtual Room FillRoom()
	{
		//MakeDoors ();
		MakeBorder ();
		Lake ();
        RoomGenerator generator = new RoomGenerator(transform.position, grid, room);
        return room;
	}

	protected void MakePathFindingConnections()
	{
		string[] strings = new string[3]{"Water","BackGround", "Destructable"};
		int mask = LayerMask.GetMask (strings);
		for(int i = 0; i < room.pathFindingNodes.Count; i++)
		{
			for(int j = 0; j < room.pathFindingNodes.Count; j++)
			{
				RaycastHit2D hit = Physics2D.Linecast(room.pathFindingNodes[i].position,room.pathFindingNodes[j].position,mask);
				if(hit.collider == null)
				{
                    //Debug.DrawLine(room.pathFindingNodes[i].position, room.pathFindingNodes[j].position, Color.green, 20);
                    room.pathFindingNodes[i].AddConnection(room.pathFindingNodes[j]);
				}
			}
		}
	}

    protected void FillEnemy()
    {
        IEnemyFiller enemyFiller = new EnemyFillerFactory().MakeEnemyFiller();
        room.enemies = enemyFiller.MakeEnemies(sizeX * sizeY, room);
        MakePathFindingConnections();
    }

    protected void MakeBorder()
	{
        for(int x = 0; x < grid.GetLength(0); x++)
        {
            for(int y = 0; y < grid.GetLength(1); y++)
            {
                if ((x == 0 || y == 0) && grid[x,y] == "O")
                {
                    
                    grid[x, y] = "X";
                }
                else if ((x == grid.GetLength(0) - 1 || y == grid.GetLength(1) - 1) && grid[x,y] == "O")
                {
                    grid[x, y] = "X";
                }
            }
        }
        /*
		MakeCorner (Direction.up, new RoomCord (0, 0));
		MakeCorner (Direction.right, new RoomCord (0, sizeY - 1));
		MakeCorner (Direction.down, new RoomCord (sizeX - 1, sizeY - 1));
		MakeCorner (Direction.left, new RoomCord (sizeX - 1, 0));
		
		RoomCord pos = new RoomCord (0, 1);
		Direction currentDir = Direction.up;
		do
		{
			if(grid[pos.xPos,pos.yPos] == 2)
			{
				currentDir = GetNextDirectionClockWise(currentDir);
				pos = pos + GetIntDirection(currentDir);
				continue;
				
			}
			if(grid[pos.xPos,pos.yPos] == 0)
			{
				/*
				string path = "Walls" + System.IO.Path.DirectorySeparatorChar.ToString () + "Side1" + System.IO.Path.DirectorySeparatorChar.ToString () + "Path";
				GameObject pathObj = GameObject.Instantiate(Resources.Load(path) as GameObject) as GameObject;
				pathObj.transform.parent = transform;
				pathObj.transform.localPosition = pos.GetLocalPos();
			     /
				MakeWall(currentDir,pos);
			}
			pos = pos + GetIntDirection(currentDir);
			
		}while(pos != new RoomCord(0,0));
        */
	}

	protected void MakeDoors(bool locked)
	{
		foreach (Vector2 doorPos in doors) {
			// Temp Code to get door;
			if(doorPos.x % 2 != 0 || doorPos.y % 2 != 0)
			{
				//Debug.LogError("Door pos wrong");
				
			}
			string pathDelim = System.IO.Path.DirectorySeparatorChar.ToString ();
			string pathBegin = "RoomStuff" + pathDelim + "Door";
			int startingCornerSide = UnityEngine.Random.Range (1, 2);
			GameObject door = GameObject.Instantiate (Resources.Load (pathBegin) as GameObject, doorPos, Quaternion.identity) as GameObject;
            door.transform.parent = transform;
            if (doorPos.x  < transform.position.x + tileSize * (sizeX - 1) + 1 && (doorPos.x > transform.position.x + tileSize * (sizeX - 1) - 1))
            {
                door.transform.localEulerAngles = new Vector3(0, 0, 90);
            }
            if (doorPos.x < transform.position.x + tileSize + 1 && doorPos.x > transform.position.x - 1)
            {
                door.transform.eulerAngles = new Vector3(0, 0, -90);
            }
            if (doorPos.y < transform.position.y + tileSize * (sizeY - 1) + 1 && doorPos.y > transform.position.y + tileSize * (sizeY - 1) - 1)
            {
                door.transform.eulerAngles = new Vector3(0, 0, 180);
            }
            
			door.GetComponent<RoomEnterance> ().SetRoom(room);

			if(locked)
			{
				door.GetComponent<Door>().Lock(room);
			}

		}
	}

	enum Direction {up,left,right,down }
	Direction GetNextDirectionClockWise(Direction currentDir)
	{
		switch(currentDir)
		{
		case Direction.left:
			return Direction.up;
		case Direction.up:
			return Direction.right;
		case Direction.right:
			return Direction.down;
		case Direction.down:
			return Direction.left;
		default:
			Debug.LogError("Bad Direction");
			return Direction.up;
		}
	}

	RoomCord GetIntDirection (Direction currentDir)
	{
		switch (currentDir)
		{
		case Direction.left:
			return new RoomCord(-1,0);
		case Direction.up:
			return new RoomCord(0,1);
		case Direction.right:
			return new RoomCord(1,0);
		case Direction.down:
			return new RoomCord(0,-1);
		default:
			Debug.LogError("Bad Direction");
			return new RoomCord(0,0);
		}
	}

	bool IsInRange(Vector2 pair)
	{
		if (pair.x > sizeX - 1 || pair.x < 0) {
			return false;
		}
		if (pair.y > sizeY -1 || pair.y < 0) {
			return false;
		}
		return true;
	}

	int GetRotation(Direction dir)
	{
		if (dir == Direction.right) {
			return -90;
		}
		if (dir == Direction.left) {
			return 90;
		}
		if (dir == Direction.down) {
			return 180;
		}
		return 0;
	}

	void MakeWall(Direction direction, RoomCord cords)
	{
		//string path = "Walls" + System.IO.Path.DirectorySeparatorChar.ToString () + "Side1" + System.IO.Path.DirectorySeparatorChar.ToString () + "Path";
		//GameObject pathObj = GameObject.Instantiate(Resources.Load(path) as GameObject) as GameObject;
		//pathObj.transform.parent = transform;
		/*
		bool lengthGood = false;
		int length = Random.Range (1, 3);
		while(!lengthGood && length > 0)
		{
			lengthGood = true;
			for (int i = 0; i < length; i++) {
				RoomCord currentPos = cords + i * GetIntDirection(direction);
				if(grid[currentPos.xPos, currentPos.yPos] != 0)
				{
					length = length - 1;
					lengthGood = false;
					break;
				}
			}
		}
		string path = "Walls" + System.IO.Path.DirectorySeparatorChar.ToString () + "Side1" + System.IO.Path.DirectorySeparatorChar.ToString ();
		GameObject pathObj = GameObject.Instantiate(Resources.Load(path + length + System.IO.Path.DirectorySeparatorChar.ToString () + "Path") as GameObject) as GameObject;
		pathObj.transform.parent = transform;
		pathObj.transform.localPosition = cords.GetLocalPos();
		pathObj.transform.eulerAngles = new Vector3 (0, 0, GetRotation (direction));

		for(int i = 0; i < length; i++)
		{
			RoomCord currentPos = cords + i * GetIntDirection(direction);
			grid[currentPos.xPos, currentPos.yPos] = 1;
		}
        */
	}

	void MakeCorner(Direction direction, RoomCord pos)
	{
        /*
		// Load corner object
		string pathDelim = System.IO.Path.DirectorySeparatorChar.ToString();
		string pathBegin = "Walls" + pathDelim + "Side";
		int startingCornerSide = Random.Range (1, 2);
		GameObject corner = GameObject.Instantiate (Resources.Load (pathBegin + startingCornerSide + pathDelim +"Corner") as GameObject);

		// Put corner in position
		corner.transform.parent = transform;
		corner.transform.localPosition = pos.GetLocalPos ();

		// Mark corner on grid
		grid [pos.xPos, pos.yPos] = 2;*/
	}
    

}

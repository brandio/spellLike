using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class DungeonRoomMaker : MonoBehaviour {
	const int tileSize = 2;
	public int depth = 0;
	public GameObject roomObj;
	public LayerMask mask;
	public int maxsize;
	public int minsize;
	public int tries;

	public GameObject path;
    public GameObject pathBottom;
    public GameObject wall;
	Dictionary<Vector2,node> positionToNode = new Dictionary<Vector2,node>();
    [HideInInspector]
    public Vector2 bottomLeft = new Vector2(10000, 10000);
    [HideInInspector]
    public Vector2 topRight = new Vector2(-100000, -10000);

	void Start()
	{
        StartCoroutine ("MakeDungeon");
	}


    bool MakeCorrider2(node start, node finish, int xDir, int yDir, int thickness)
    {
        // Get the distances
        int xDist = thickness;
        int yDist = thickness;
        if (xDir == 1)
        {
            float xStart = start.pos.x + start.sizeX * tileSize;
            xDist = (int)(finish.pos.x - xStart) / tileSize;
            
        }
        else if(xDir == -1)
        {
            float yStart = finish.pos.y + finish.sizeY * tileSize;
            xDist = (int)(start.pos.x - yStart) / tileSize;
        }
        else if(yDir == 1)
        {
            float xStart = start.pos.x + start.sizeX * tileSize;
            xDist = (int)(finish.pos.x - xStart) / tileSize;
        }
        else if(yDir == -1)
        {
            float yStart = finish.pos.y + finish.sizeY * tileSize;
            float distance = (start.pos.y) - yStart;
        }
        return true;
    }

    bool MakeCorrider(node start, node finish, int xDir, int yDir)
	{
       // MakeCorrider2(start, finish, xDir, yDir, 1);
        float minx = 0;
		float miny = 0;
		float maxx = 0;
		float maxy = 0;

		if (xDir != 0) {
			if(start.pos.y < finish.pos.y)
			{
				miny = finish.pos.y + 1;
			}
			else
			{
				miny = start.pos.y + 1;
			}
			if(start.pos.y + (start.sizeY) * tileSize < finish.pos.y + (finish.sizeY)  * tileSize)
			{
				maxy = start.pos.y + (start.sizeY - 2) * tileSize;
			}
			else
			{
				maxy = finish.pos.y + (finish.sizeY - 2) * tileSize;
			}

			if(maxy <= miny)
			{
				return false;
			}
			//Going left
			int yStart = Random.Range ((int)miny,(int)maxy);
			yStart = MathUtility.roundUp(yStart,2);
			if(yStart + tileSize >= maxy || yStart - tileSize <= miny)
			{
				return false;
			}
            if (xDir == -1)
            {
                float xStart = finish.pos.x + finish.sizeX * tileSize;
                float distance = (start.pos.x) - xStart;

                finish.roomBuilder.AddDoor(new Vector2(xStart - tileSize, yStart));
                start.roomBuilder.AddDoor(new Vector2(xStart + distance, yStart));
                finish.roomBuilder.AddDoor(new Vector2(xStart - tileSize, yStart + tileSize));
                start.roomBuilder.AddDoor(new Vector2(xStart + distance, yStart + tileSize));

                for (int i = 0; i < distance / tileSize; i++)
                {
                    GameObject pathObj = Instantiate(path, new Vector3(xStart + i * 2, yStart, 0), Quaternion.identity) as GameObject;
                    start.roomBuilder.floorObjs.Add(pathObj);
                    finish.roomBuilder.floorObjs.Add(pathObj);
                }
                for (int i = 0; i < distance / tileSize; i++)
                {
                    GameObject pathObj =  Instantiate(path, new Vector3(xStart + i * 2, yStart + tileSize, 0), Quaternion.identity) as GameObject;
                    start.roomBuilder.floorObjs.Add(pathObj);
                    finish.roomBuilder.floorObjs.Add(pathObj);
                }
                for (int i = 0; i < distance / tileSize; i++)
                {
                    GameObject pathObj =  Instantiate(wall, new Vector3(xStart + i * 2, yStart - tileSize, 0), Quaternion.identity) as GameObject;
                    start.roomBuilder.floorObjs.Add(pathObj);
                    finish.roomBuilder.floorObjs.Add(pathObj);
                }
                for (int i = 0; i < distance / tileSize; i++)
                {
                    GameObject pathObj =  Instantiate(pathBottom, new Vector3(xStart + i * 2, yStart + tileSize * 2, 0), Quaternion.identity) as GameObject;
                    start.roomBuilder.floorObjs.Add(pathObj);
                    finish.roomBuilder.floorObjs.Add(pathObj);
                }
            }
            // Going right
            if (xDir == 1) {
				float xStart = start.pos.x + start.sizeX * tileSize;
				float distance = (finish.pos.x) - xStart;

				start.roomBuilder.AddDoor(new Vector2( xStart -  tileSize, yStart ));
				finish.roomBuilder.AddDoor(new Vector2(xStart +  distance, yStart ));
                start.roomBuilder.AddDoor(new Vector2(xStart - tileSize, yStart + tileSize));
                finish.roomBuilder.AddDoor(new Vector2(xStart + distance, yStart + tileSize));

                for (int i = 0; i < distance/tileSize; i++)
				{
                    GameObject pathObj =  Instantiate(path, new Vector3(xStart + i * 2,yStart,0),Quaternion.identity) as GameObject ;
                    start.roomBuilder.floorObjs.Add(pathObj);
                    finish.roomBuilder.floorObjs.Add(pathObj);
                }
                for (int i = 0; i < distance / tileSize; i++)
                {
                    GameObject pathObj =  Instantiate(path, new Vector3(xStart + i * 2, yStart + tileSize, 0), Quaternion.identity) as GameObject ;
                    start.roomBuilder.floorObjs.Add(pathObj);
                    finish.roomBuilder.floorObjs.Add(pathObj);
                }
                for (int i = 0; i < distance / tileSize; i++)
                {
                    GameObject pathObj = Instantiate(wall, new Vector3(xStart + i * 2, yStart - tileSize, 0), Quaternion.identity) as GameObject;
                    start.roomBuilder.floorObjs.Add(pathObj);
                    finish.roomBuilder.floorObjs.Add(pathObj);
                }
                for (int i = 0; i < distance / tileSize; i++)
                {
                    GameObject pathObj =  Instantiate(pathBottom, new Vector3(xStart + i * 2, yStart + 2 * tileSize, 0), Quaternion.identity) as GameObject;
                    start.roomBuilder.floorObjs.Add(pathObj);
                    finish.roomBuilder.floorObjs.Add(pathObj);
                }
            }
		}
		if (yDir != 0) {
			if(start.pos.x < finish.pos.x)
			{
				minx = finish.pos.x + 1;
			}
			else
			{
				minx = start.pos.x + 1;
			}
			if(start.pos.x + (start.sizeX - 2) * tileSize < finish.pos.x + (finish.sizeX - 2) * tileSize)
			{
				maxx = start.pos.x + (start.sizeX - 2) * tileSize;
			}
			else
			{
				maxx = finish.pos.x + (finish.sizeX - 2) * tileSize;
			}
			if(maxx <= minx)
			{
				return false;
			}
			int xStart = Random.Range ((int)minx,(int)maxx);
			xStart = MathUtility.roundUp(xStart,tileSize);

            if (xStart + tileSize >= maxx || xStart - tileSize <= minx)
			{
				return false;
			}
			//Debug.Log (minx + " minx " + maxx + " maxx " + yDir + " dir " + xStart);
			if (yDir == -1) {
				float yStart = finish.pos.y + finish.sizeY * tileSize;
				float distance = (start.pos.y) - yStart;

				finish.roomBuilder.AddDoor(new Vector2(xStart, yStart -  tileSize));
				start.roomBuilder.AddDoor(new Vector2(xStart, yStart +  distance));
                finish.roomBuilder.AddDoor(new Vector2(xStart + tileSize, yStart - tileSize));
                start.roomBuilder.AddDoor(new Vector2(xStart + tileSize, yStart + distance));
                for (int i = 0; i < distance/2; i++)
				{
                    GameObject pathObj =  Instantiate(path,new Vector3(xStart + tileSize ,yStart + i * 2,0),Quaternion.identity) as GameObject;
                    start.roomBuilder.floorObjs.Add(pathObj);
                    finish.roomBuilder.floorObjs.Add(pathObj);
                }
                for (int i = 0; i < distance / 2; i++)
                {
                    GameObject pathObj =  Instantiate(path, new Vector3(xStart, yStart + i * 2, 0), Quaternion.identity) as GameObject;
                    start.roomBuilder.floorObjs.Add(pathObj);
                    finish.roomBuilder.floorObjs.Add(pathObj);
                }
                for (int i = 0; i < distance / 2; i++)
                {
                    GameObject pathObj =  Instantiate(wall, new Vector3(xStart + tileSize * 2, yStart + i * 2, 0), Quaternion.identity) as GameObject;
                    start.roomBuilder.floorObjs.Add(pathObj);
                    finish.roomBuilder.floorObjs.Add(pathObj);
                }
                for (int i = 0; i < distance / 2; i++)
                {
                    GameObject pathObj =  Instantiate(wall, new Vector3(xStart - tileSize, yStart + i * 2, 0), Quaternion.identity) as GameObject;
                    start.roomBuilder.floorObjs.Add(pathObj);
                    finish.roomBuilder.floorObjs.Add(pathObj);
                }
            }
			if (yDir == 1) {
				float yStart = start.pos.y + start.sizeY * tileSize;
				float distance = (finish.pos.y) - yStart;

				start.roomBuilder.AddDoor(new Vector2(xStart, yStart -  tileSize));
				finish.roomBuilder.AddDoor(new Vector2(xStart, yStart +  distance));
                start.roomBuilder.AddDoor(new Vector2(xStart + tileSize, yStart - tileSize));
                finish.roomBuilder.AddDoor(new Vector2(xStart + tileSize, yStart + distance));



                for (int i = 0; i < distance/2; i++)
				{
                    GameObject pathObj =  Instantiate(path,new Vector3(xStart + tileSize ,yStart + i * 2,0),Quaternion.identity) as GameObject ;
                    start.roomBuilder.floorObjs.Add(pathObj);
                    finish.roomBuilder.floorObjs.Add(pathObj);
                }
                for (int i = 0; i < distance / 2; i++)
                {
                    GameObject pathObj =  Instantiate(path, new Vector3(xStart, yStart + i * 2, 0), Quaternion.identity) as GameObject;
                    start.roomBuilder.floorObjs.Add(pathObj);
                    finish.roomBuilder.floorObjs.Add(pathObj);
                }
                for (int i = 0; i < distance / 2; i++)
                {
                    GameObject pathObj =  Instantiate(wall, new Vector3(xStart - tileSize, yStart + i * 2, 0), Quaternion.identity) as GameObject;
                    start.roomBuilder.floorObjs.Add(pathObj);
                    finish.roomBuilder.floorObjs.Add(pathObj);
                }
                for (int i = 0; i < distance / 2; i++)
                {
                    GameObject pathObj =  Instantiate(wall, new Vector3(xStart + tileSize * 2, yStart + i * 2, 0), Quaternion.identity) as GameObject;
                    start.roomBuilder.floorObjs.Add(pathObj);
                    finish.roomBuilder.floorObjs.Add(pathObj);
                }
            }
		}
		return true;
	}

	IEnumerator MakeDungeon () {
		List<node> completedNodes = new List<node>();
		Stack nodes = new Stack();
		int sizeX = Random.Range (20, 20);
		int sizeY = Random.Range (20, 20);
		node startNode = new node (0, sizeX, sizeY, new Vector2 (0, 0));
		nodes.Push(startNode);
		MakeRoom (startNode);
		startNode.roomBuilder.MakeFloor ();
		AddToDict(startNode);
		while (nodes.Count > 0) {

			node currentnode = (DungeonRoomMaker.node)nodes.Peek ();

			yield return null;
			if(currentnode.count < depth)
			{
				if(currentnode.surroundedBottom && currentnode.surroundedTop && currentnode.surroundedRight && currentnode.surroundedLeft)
				{
					completedNodes.Add((node)nodes.Pop());
				}
				else
				{
					if(!currentnode.surroundedTop)
					{

						currentnode.surroundedTop = FindSpace(0,1,currentnode,nodes);
					}
					if(!currentnode.surroundedBottom)
					{

						currentnode.surroundedBottom = FindSpace(0,-1,currentnode,nodes);
					}
					if(!currentnode.surroundedRight)
					{

						currentnode.surroundedRight = FindSpace(1,0,currentnode,nodes);
					}
					if(!currentnode.surroundedLeft)
					{

						currentnode.surroundedLeft = FindSpace(-1,0,currentnode,nodes);
					}
				}
			}
			else
			{
				completedNodes.Add((node)nodes.Pop());
			}
		}

        List<RoomBuilder> roomBuilders = new List<RoomBuilder>();
        foreach (System.Collections.Generic.KeyValuePair<Vector2, node> n in positionToNode)
        {
            roomBuilders.Add(n.Value.roomBuilder);
        }
        DetermineDungeon(roomBuilders);

        int i = 0;
        foreach (System.Collections.Generic.KeyValuePair<Vector2, node> n in positionToNode) {
            PlayerRoomManager.instance.Rooms.Add(n.Value.roomBuilder.RoomFiller());
            if (i == 0)
                PlayerRoomManager.instance.Rooms[0].Activate();
            if (n.Value.pos.x < bottomLeft.x)
                bottomLeft.x = n.Value.pos.x;
            if (n.Value.pos.y < bottomLeft.y)
                bottomLeft.y = n.Value.pos.y;
            if (n.Value.pos.x + n.Value.sizeX * tileSize > topRight.x)
                topRight.x = n.Value.pos.x + n.Value.sizeX * tileSize;
            if (n.Value.pos.y + n.Value.sizeY * tileSize > topRight.y)
                topRight.y = n.Value.pos.y + n.Value.sizeY * tileSize;
            i++;
            yield return null;
		}

        if(DungeonDone != null)
        {
            DungeonDone();
        }
	}

    void DetermineDungeon(List<RoomBuilder> roomBuilders)
    {
        List<RoomBuilder> maxDepth = roomBuilders.Where(o => o.depth == depth).ToList().OrderByDescending(o => o.sizeX * o.sizeY).ToList();
        maxDepth[0].AddRoomFiller(new BossOneFiller(maxDepth[0].position, maxDepth[0].sizeX, maxDepth[0].sizeY, maxDepth[0].room, maxDepth[0].doors));
        //maxDepth[1].AddRoomFiller(new BossOneFiller(maxDepth[1].position, maxDepth[1].sizeX, maxDepth[1].sizeY, maxDepth[1].room, maxDepth[1].doors));

        RoomBuilder portalRoom = maxDepth[maxDepth.Count - 1];
        RoomBuilder portalRoom2 = maxDepth[maxDepth.Count - 2];
        PortalRoomFiller portalRoomFiller1 = new PortalRoomFiller(portalRoom.position, portalRoom.sizeX, portalRoom.sizeY, portalRoom.room, portalRoom.doors);
        PortalRoomFiller portalRoomFiller2 = new PortalRoomFiller(portalRoom2.position, portalRoom2.sizeX, portalRoom2.sizeY, portalRoom2.room, portalRoom2.doors);
        portalRoomFiller1.destinationRoom = portalRoomFiller2;
        portalRoomFiller2.destinationRoom = portalRoomFiller1;
        portalRoom.AddRoomFiller(portalRoomFiller1);
        portalRoom2.AddRoomFiller(portalRoomFiller2);

        List<RoomBuilder> depthSorted = roomBuilders.OrderByDescending(o => o.depth).ToList();
        List<RoomBuilder> sizeSorted = roomBuilders.OrderBy(o => o.depth).ToList();

        int numStores = Random.Range(1, 4);
        int stores = 0;
        foreach (RoomBuilder builder in depthSorted)
        {
            if (!builder.hasRoomFiller)
            {
                if (Random.Range(0, 100) > 20)
                {
                    builder.AddRoomFiller(new StoreRoomFiller(builder.position, builder.sizeX, builder.sizeY, builder.room, builder.doors));
                }
                stores++;
                if (stores >= numStores)
                    break;
            }
        }

        foreach (RoomBuilder builder in depthSorted)
        {

            if(!builder.hasRoomFiller)
            {
                if (Random.Range(0, 100) > 10)
                {
                    builder.AddRoomFiller(new GraveYardRoom(builder.position, builder.sizeX, builder.sizeY, builder.room, builder.doors));
                }
                else if(Random.Range(0, 100) > 32)
                {
                    builder.AddRoomFiller(new LakeRoomFiller(builder.position, builder.sizeX, builder.sizeY, builder.room, builder.doors));
                }
                else
                {
                    builder.AddRoomFiller(new GraveYardRoom(builder.position, builder.sizeX, builder.sizeY, builder.room, builder.doors));
                }
            }
            //Debug.Log(builder.depth);
        }
    }
    
    public delegate void DungeonDoneHandler();
    public event DungeonDoneHandler DungeonDone;
	bool FindSpace(int xx, int yy, node currentnode, Stack nodes)
	{

		bool found = true;
		for(int i = 0; i < tries; i++)
		{
			int x = Random.Range (minsize, maxsize);
			int y = Random.Range (minsize, maxsize);
			if(x % 2 == 0)
			{
				x = x - 1;
			}
			if(y % 2 == 0)
			{
				y = y - 1;
			}
			int offsetx = Random.Range (2, 9);
			int offsetxy = Random.Range (2, 9);

			Vector2 ps = new Vector2(0,0);
			if(xx >= 0 && yy >= 0)
			{
				ps = currentnode.pos + new Vector2(currentnode.sizeX * 2 * Mathf.Abs(xx),currentnode.sizeY * 2 * Mathf.Abs(yy)) + new Vector2(offsetx * 2, offsetxy * 2);
			}
			else
			{
				ps = currentnode.pos + (-1 * (new Vector2(x * 2 * Mathf.Abs(xx),y * 2 * Mathf.Abs(yy)) + new Vector2(offsetx * 2, offsetxy * 2)));
			}

			RaycastHit2D hit = Physics2D.BoxCast((Vector2)ps + new Vector2(x * tileSize, y * tileSize)/2, new Vector2(x * tileSize + 2, y * tileSize + 2),0,Vector2.zero,mask);
			if(hit.collider == null)
			{
				//currentnode.surroundedTop = false;
				node newNode = new node(currentnode.count + 1,x,y,ps);

				if(1 != 0)
				{
					MakeRoom (newNode);
					if(MakeCorrider(currentnode,newNode,xx,yy))
					{
						newNode.roomBuilder.MakeFloor();
						currentnode.nodes.Add(newNode);
						newNode.nodes.Add(currentnode);
						found = false;
						nodes.Push (newNode);
						AddToDict(newNode);
						break;
					}
					else
					{
						newNode.roomBuilder.Clear();
					}
				}
			}
			if(hit.collider != null) //&& i == tries - 1)
			{
				Vector2 ourPosition;
				Vector2 startPos = currentnode.pos;
				if(yy != 0)
				{
					startPos.x = startPos.x + currentnode.sizeX * 2/2;
				}
				else
				{
					startPos.y = startPos.y + currentnode.sizeY * 2/2;
				}
				if(xx >= 0 && yy >= 0)
				{
					ourPosition = startPos + new Vector2((currentnode.sizeX + 6) * 2 * Mathf.Abs(xx),(currentnode.sizeY + 6) * 2 * Mathf.Abs(yy));
				}
				else
				{
					ourPosition = startPos - new Vector2(20 * Mathf.Abs(xx), 20 * Mathf.Abs(yy));
				}
				RaycastHit2D hit2 = Physics2D.BoxCast(ourPosition,new Vector2(20,20),0,Vector2.zero,mask);

                //if (hit2.collider != null && positionToNode.ContainsKey( (Vector2)hit2.transform.position))
                if (hit2.collider != null && hit2.transform.parent != null && positionToNode.ContainsKey((Vector2)hit2.transform.parent.position))
                    {
					if(!currentnode.nodes.Contains(positionToNode[(Vector2)hit2.transform.parent.position]))
					{
						if(MakeCorrider(currentnode,positionToNode[hit2.transform.parent.position],xx,yy))
						{
							currentnode.nodes.Add(positionToNode[hit2.transform.parent.position]);
							positionToNode[hit2.transform.parent.position].nodes.Add (currentnode);
						}
                        break;

                    }
				}


			}
			
		}
		return found;
	}

	void AddToDict(node nodeToAdd)
	{
		positionToNode.Add(new Vector2(nodeToAdd.pos.x, nodeToAdd.pos.y),nodeToAdd);
	}

	void MakeRoom(node nodeForRoom)
	{
        nodeForRoom.roomBuilder = new RoomBuilder(nodeForRoom.sizeX, nodeForRoom.sizeY, nodeForRoom.pos, nodeForRoom.count);
	}

	class node
	{
		public int count;
		public bool surroundedTop;
		public bool surroundedBottom;
		public bool surroundedLeft;
		public bool surroundedRight;
		public int sizeX;
		public int sizeY;
		public Vector2 pos;

		public HashSet<node> nodes;
		public RoomBuilder roomBuilder;
		public node(int c,int x, int y, Vector2 p)
		{
			nodes = new HashSet<node>();
			count = c;
			sizeX = x;
			sizeY = y;
			surroundedTop = false;
			surroundedBottom = false;
			surroundedLeft = false;
			surroundedRight = false;
			pos = p;

		}
	}
}

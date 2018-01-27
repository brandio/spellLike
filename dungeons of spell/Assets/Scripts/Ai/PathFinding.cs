using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinding 
{
	// The start Tile that we need a path to
	PathFindingNode m_startPosition;
	// The destination Tile that we need a path to
	PathFindingNode m_endPosition;


	// Node is the final node of the path
	// It can be followed back to get the entire path
	public NodeHolder m_finalNode;
	// callback function
	public delegate void PathFindingCallBack(NodeHolder n);
	// ifItShouldBePathing
	bool pathing;

	public PathFinding()
	{

	}

	public void init(PathFindingNode start, PathFindingNode end)
	{
		pathing = false;
		m_startPosition = start;
		m_endPosition = end;
	}

	public NodeHolder GetFinalNode()
	{
		return m_finalNode;
	}

	public Stack<Vector2> MakePathFromNodes()
	{
		Stack<Vector2> path = new Stack<Vector2> ();
		NodeHolder currentNode = m_finalNode;
		path.Push(currentNode.GetTile().position);
		while (currentNode.GetPreviousNode() != null) {
            Debug.DrawLine(currentNode.GetTile().position, currentNode.GetPreviousNode().GetTile().position, Color.yellow, 10);
			currentNode = currentNode.GetPreviousNode();
			path.Push(currentNode.GetTile().position);
		}
		return path;
	}

	// Holds a path finding node, prev node and the heuristics that a* needs
	public class NodeHolder : System.IComparable<NodeHolder>, System.IEquatable<NodeHolder>
	{
		//-------------------------------
		public NodeHolder(PathFindingNode start, NodeHolder prev, PathFindingNode end)
		{
			init(start, prev, end);
		}
		
		//-------------------------------
		public int CompareTo(NodeHolder obj)
		{
			if(obj.GetFvalue() > this.fVal)
				return -1;
			if(obj.GetFvalue() < this.fVal)
				return 1;
			return 0;
		}
		
		//-------------------------------
		public bool Equals(NodeHolder obj)
		{
			return (this.GetTile ().position.x == obj.GetTile ().position.x && this.GetTile ().position.y == obj.GetTile ().position.y);
		}
		
		//-------------------------------
		public float GetGValue()
		{
			return gVal;
		}
		
		//-------------------------------
		public float GetFvalue()
		{
			return fVal;
		}

		public PathFindingNode GetTile()
		{
			return node;
		}
		
		public void UpdateG(NodeHolder rhs)
		{
			m_prevNode = rhs.GetPreviousNode();
			gVal = rhs.GetGValue();
			fVal = gVal + hVal;
		}

		public NodeHolder GetPreviousNode()
		{
			return m_prevNode;
		}
		//-------------------------------
		void init(PathFindingNode newNode, NodeHolder prevPathNode, PathFindingNode end)
		{
			node = newNode;
			m_prevNode = prevPathNode;
			hVal = CalculateHVal(end);
			gVal = CalculateGVal();
			fVal = gVal + hVal;
		}
		
		//-------------------------------
		float CalculateGVal()
		{
			if(m_prevNode == null)
			{
				return 0;
			}

			return m_prevNode.GetGValue () + Vector2.Distance (node.position, m_prevNode.GetTile().position);
		}
		
		//-------------------------------
		float CalculateHVal(PathFindingNode end)
		{
            if(end == null)
            {
                Debug.LogError("END IS NULL");
            }
            if (end.position == null)
            {
                Debug.LogError("END pos IS NULL");
            }
            return Vector2.Distance (node.position, end.position);
		}

		//-------------------------------
		//--Members----------------------
		//-------------------------------
		// coridinate
		PathFindingNode node;
		// Distance to the destination 
		float hVal;
		// Cost to get to this node
		float gVal;
		// Final herustic value, is a sum of g and h
		float fVal;
		// Prev node
		NodeHolder m_prevNode;
	};

	public IEnumerator aStar(PathFindingCallBack callBack)
	{
		Debug.Log ("start aStar");
		int i = 0;
		List<NodeHolder> closedList = new List<NodeHolder>();
		PriorityQueue<NodeHolder> openList = new PriorityQueue<NodeHolder>();
		NodeHolder current = new NodeHolder(m_startPosition, null, m_endPosition);
		openList.EnQueue(current);
		pathing = true;

		while(openList.size() != 0 )
		{
			// Check to see if we are stuck or told to quit finding a path
			i = i + 1;
			if(i > 2000 || !pathing)
			{
				pathing = false;
				Debug.LogError("failed to find path");
				yield break;
			}

			NodeHolder currentNode  = openList.DeQueue();
			closedList.Add(currentNode);
			List<PathFindingNode> neighbors = currentNode.GetTile().GetConnections();
			foreach(PathFindingNode neighbor in neighbors)
			{

				NodeHolder neighborNode = new NodeHolder(neighbor,currentNode,m_endPosition);
				if(neighbor.Equals(m_endPosition))
				{
					Debug.Log ("Found Path");
					m_finalNode = neighborNode;
					callBack(m_finalNode);
					yield break;
				}
				
				if(openList.Contains(neighborNode))
				{
					NodeHolder node = openList.FindItem(neighborNode);
					if(neighborNode.GetGValue() < node.GetGValue())
					{
						node.UpdateG(neighborNode);
						openList.IncreasePriority(openList.IndexOf (node));

					}
					if(i%600 == 0)
					{
						yield return null;
					}
				}
				else if(closedList.Contains(neighborNode))
				{
					int index = closedList.IndexOf(neighborNode);
					NodeHolder node = closedList[index];
					if(neighborNode.GetGValue() < node.GetGValue())
					{
						closedList.Remove(node);
					}
					openList.EnQueue(neighborNode);

					if(i%600 == 0)
					{
						yield return null;
					}
				}
				else 
				{
					openList.EnQueue(neighborNode);
				}
				if(i%600 == 0)
				{
					yield return null;
				}
			}
			
		}
		yield break;
	}


	public Stack<Vector2> aStarInstant()
	{
		int i = 0;
		List<NodeHolder> closedList = new List<NodeHolder>();
		PriorityQueue<NodeHolder> openList = new PriorityQueue<NodeHolder>();
		NodeHolder current = new NodeHolder(m_startPosition, null, m_endPosition);
		openList.EnQueue(current);
	
		while(openList.size() != 0 )
		{	
			// Check to see if we are stuck
			i = i + 1;
            //Debug.Log(openList.Size() + " " + closedList.Count);
			if(i > 1000)
			{
				Debug.LogError("failed to find path");
				return new Stack<Vector2>();
			}

			NodeHolder currentNode  = openList.DeQueue();
			closedList.Add(currentNode);
			List<PathFindingNode> neighbors = currentNode.GetTile().GetConnections();

			foreach(PathFindingNode neighbor in neighbors)
			{
				NodeHolder neighborNode = new NodeHolder(neighbor,currentNode,m_endPosition);
				if(neighbor.Equals(m_endPosition))
				{
					m_finalNode = neighborNode;
					return MakePathFromNodes();
				}
			
				if(openList.Contains(neighborNode))
				{
					NodeHolder node = openList.FindItem(neighborNode);
					if(neighborNode.GetGValue() < node.GetGValue())
					{
						node.UpdateG(neighborNode);
						openList.IncreasePriority(openList.IndexOf (node));
					}
				}
				else if(closedList.Contains(neighborNode))
				{
					int index = closedList.IndexOf(neighborNode);
					NodeHolder node = closedList[index];
					if(neighborNode.GetGValue() < node.GetGValue())
					{
						closedList.Remove(node);
					}


					openList.EnQueue(neighborNode);
				}
				else 
				{
					openList.EnQueue(neighborNode);
				}
			}	
		}
		return new Stack<Vector2>();
	}

}
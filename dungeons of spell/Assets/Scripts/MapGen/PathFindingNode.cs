using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFindingNode {
	
	List<PathFindingNode> connections;
	public Vector2 position;

	public List<PathFindingNode> GetConnections()
	{
		return new List<PathFindingNode> (connections);
	}

	public PathFindingNode(Vector2 pos)
	{
		position = pos;
		connections = new List<PathFindingNode> ();

	    //GameObject.Instantiate (Resources.Load ("Node"),pos, Quaternion.identity);
	}

	public void AddConnectionBothWays(PathFindingNode node)
	{
		connections.Add(node);
		node.AddConnection(this);
	}

	public void AddConnection(PathFindingNode node)
	{
		if (node.Equals (this)) {
			return;
		}
		int i = Random.Range (0, 10);
        
            /*
        if (i > 5) {
			Debug.DrawLine (node.position, this.position + new Vector2(Random.Range (0,2),Random.Range (0,2)), Color.red, 20);
		} 
		else {
			Debug.DrawLine (node.position, this.position + new Vector2(Random.Range (0,2),Random.Range (0,2)), Color.green, 20);
		}
        */
		connections.Add(node);
	}	
}

using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour, PathingObject{
	public float speed = 0;
	MovementCheck mc;
	Reflection reflection;
	public Animator anim;
	public float speedMod = 1;
    public LayerMask mask;
	// Use this for initialization
	void Start () {
		mc = this.GetComponent<MovementCheck> ();
		reflection = transform.GetChild(0).GetComponent<Reflection> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Controls.GetInstance ().active) {
			Move ();
		}

	}

	public Room room;
    public Transform GetTransform()
    {
        return transform;
    }
	public PathFindingNode GetPosition()
	{
		PathFindingNode minNode = null;
		float minDist = -1;
        bool foundNode = false;
		foreach (PathFindingNode node in room.pathFindingNodes) {
			float dist = Vector2.SqrMagnitude((Vector2)node.position - (Vector2)transform.position);
			{
                RaycastHit2D hit = Physics2D.Linecast(transform.position, node.position, mask);
                if (hit.collider != null && foundNode)
                {
                    continue;
                }
                else
                {
                    foundNode = true;
                }
                if (dist < minDist || minDist < 0)
				{
					minDist = dist;
					minNode = node;
				}
			}
		}
		return minNode;
	}

	void Move()
	{
		Vector3 moveVector = new Vector3 (0, 0, 0);
		if (Input.GetKey (KeyCode.A)) {
			moveVector = moveVector + new Vector3(-1,0,0);
			if(anim.gameObject.transform.localScale.x  > 0)
			{
				anim.gameObject.transform.localScale = new Vector3(anim.gameObject.transform.localScale.x * -1,anim.gameObject.transform.localScale.y,anim.gameObject.transform.localScale.z);

			}

		}
		if (Input.GetKey (KeyCode.D)) {
			moveVector = moveVector + new Vector3(1,0,0);
			if(anim.gameObject.transform.localScale.x  < 0)
			{
				anim.gameObject.transform.localScale = new Vector3(anim.gameObject.transform.localScale.x * -1,anim.gameObject.transform.localScale.y,anim.gameObject.transform.localScale.z);
			}
		}
		if (Input.GetKey (KeyCode.W)) {
			moveVector = moveVector + new Vector3(0,1,0);

		}
		if (Input.GetKey (KeyCode.S)) {
			moveVector = moveVector + new Vector3(0,-1,0);
		}
		Vector3 pos = this.transform.position + (moveVector * speed * speedMod * Time.deltaTime);
		if(mc.CheckMove(pos))
		{
			if(moveVector == Vector3.zero)
			{
				reflection.SetBool("Walking",false);
			}
			else
			{
				reflection.SetBool("Walking",true);
			}

			this.transform.position = pos;
		}

	}
}

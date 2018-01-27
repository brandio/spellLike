using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (MovementCheck2))]
public class AiMovement : MonoBehaviour, PathingObject {

    // How fast we move
    public float movementSpeed;
    // How often we refresh to get a full path
    public float timeToRefreshPath;
    // How much we veer of the path
    public float randomness;

    // Have to flip the transform of the sprite depending on what direction we are moving
    // Should probably have a separate component for this but w/e
    public Transform spriteTransform;

    [HideInInspector]
    public Room room;
	const float lookAheadAmount = .4f;
    MovementCheck2 movementCheck;
    LayerMask mask;
    Vector2 movementDir = Vector2.zero;
    float lastRefreshTime;
	PathingObject destObj;
	Vector2 destPos;
	Stack<Vector2> fullPath;
	float lookAhead = 0;
	float currentMovementSpeed;
    float lastDirectionChangeTime = 0;
    float minDirectionChangeTime = .1f;

    const float MIN_DIST = .2f;

    /*
        Start PathingObject methods
    */
    public Transform GetTransform()
    {
        return transform;
    }


    public PathFindingNode GetPosition()
	{
		PathFindingNode minNode = null;
		float minDist = -1;
        int layer = gameObject.layer;
		bool foundNode = false;
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        foreach (PathFindingNode node in room.pathFindingNodes) {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, node.position, mask);
            Debug.DrawLine(transform.position, node.position, Color.red);
			if(hit.collider != null && foundNode)
            {
                continue;
            }
			else
			{
				foundNode = true;
			}
			float dist = Vector2.SqrMagnitude((Vector2)node.position - (Vector2)transform.position);
			{
				if(dist < minDist || minDist < 0)
				{
					minDist = dist;
					minNode = node;
				}
			}
		}
        gameObject.layer = layer;
        return minNode;
	}
    /*
    End PathingObject methods
    */

	void Start()
	{
		lookAhead = movementSpeed * lookAheadAmount;
		currentMovementSpeed = movementSpeed;
	}

    // The only public function this component has. Sets this object to move towards a target object
    public void MoveTowardsObject(PathingObject destinationObject)
    {
		if(currentMovementSpeed  != movementSpeed && Time.time - lastDirectionChangeTime > minDirectionChangeTime)
		{
			currentMovementSpeed = movementSpeed;
			lookAhead = currentMovementSpeed * lookAheadAmount;
			return;
		}

        if (Time.time - timeToRefreshPath > lastRefreshTime || destinationObject != destObj)
        {
            lastRefreshTime = Time.time + Random.Range(-randomness, randomness);
            destObj = destinationObject;
            GetPath();
            GetStartingDir();
        }

        //MoveTowardsPlayerIfPossible();

        Debug.DrawLine(transform.position, destPos,Color.blue);
        Debug.DrawRay(transform.position, movementDir * 2, Color.yellow);
        // Check if we reached it
        if (Mathf.Abs(Vector2.Distance(destPos, transform.position)) < MIN_DIST * 2)
        {
            if (fullPath.Count > 0 && fullPath.Peek() != null)
            {
                destPos = fullPath.Pop();
                //Debug.DrawLine(this.transform.position,destPos,Color.cyan,1);
                GetStartingDir();
            }
            else
            {
                movementDir = Vector2.zero;
                return;
            }
        }

        // Change direction if we need to
        GetEndOneAxisMoveDir();

        // Check for collisions
        CheckForCollision();
        if (true)//movementCheck.IsPositionClear(transform.position + (Vector3)movementDir * currentMovementSpeed * Time.deltaTime))
        {
            if(movementDir.x > 0 && spriteTransform.localScale.x < 0)
            {
                //spriteTransform.localScale = new Vector3(spriteTransform.localScale.x * -1, spriteTransform.localScale.y, spriteTransform.localScale.z);
            }
            else if(movementDir.x < 0 && spriteTransform.localScale.x > 0)
            {
                //spriteTransform.localScale = new Vector3(spriteTransform.localScale.x * -1, spriteTransform.localScale.y, spriteTransform.localScale.z);
            }
            
            transform.position += (Vector3)movementDir * currentMovementSpeed * Time.deltaTime;
        }
        else
        {
            GetCollisionMoveDir(true);
        }
    }

    // Cache values
    void Awake()
    {
        
        movementCheck = GetComponent<MovementCheck2>();
        mask = movementCheck.mask;
    }

    // Get full pathfinding path
	void GetPath()
	{
        if(MoveTowardsPlayerIfPossible())
        {
            fullPath = new Stack<Vector2>();
            fullPath.Push(destPos);
            return;
        }
		PathFinding pathFinding = new PathFinding ();
		pathFinding.init (this.GetPosition (), destObj.GetPosition ());
		fullPath = pathFinding.aStarInstant ();
        //Debug.DrawLine (this.GetPosition ().position, destObj.GetPosition ().position,Color.yellow,timeToRefreshPath);

        bool goodPath = false;
        Vector2 dPos = transform.position;
        if (fullPath.Count > 0)
        {
            dPos = fullPath.Pop();
        }
        int layer = gameObject.layer;
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        while (fullPath.Count > 0 && !goodPath)
        {
            
            RaycastHit2D hit = Physics2D.Linecast(transform.position, fullPath.Peek(), mask);
            if(hit.collider != null)
            {
                goodPath = true;
            }
            else
            {
                dPos = fullPath.Pop();
            }
        }
        gameObject.layer = layer;
        
        destPos = dPos + new Vector2(Random.Range(-randomness, randomness), Random.Range(-randomness, randomness));
    }

	// Gets the direction when a new destination is set
	void GetStartingDir()
	{
        
		float xDir = destPos.x - transform.position.x;
		float yDir = destPos.y - transform.position.y;
        const float middleAmount = 5;
		if (Mathf.Abs(xDir) < MIN_DIST & Mathf.Abs(yDir) < MIN_DIST) {
			movementDir = Vector2.zero;
			return;
		}
		if (Mathf.Abs (xDir) - middleAmount > Mathf.Abs (yDir)) {
			movementDir = (xDir > 0 ? new Vector2 (1, 0) : new Vector2 (-1, 0));
		}
        else if (Mathf.Abs(yDir) - middleAmount > Mathf.Abs(xDir))
        {
			movementDir = (yDir > 0 ? new Vector2 (0, 1) : new Vector2 (0, -1));
		}
        else
        {
            int x = (xDir > 0 ? 1 : -1);
            int y = (yDir > 0 ? 1 : -1);
            movementDir = new Vector2(x, y);
        }
	}

	// Gets the direction when the destination is reached in only one axis
	void GetEndOneAxisMoveDir()
	{
		float xDist = destPos.x - transform.position.x;
		float yDist = destPos.y - transform.position.y;
		if (movementDir.y != 0 && movementDir.x != 0) {
			if(Mathf.Abs(xDist) < MIN_DIST)
			{
				movementDir.x = 0;
			}
			if(Mathf.Abs(yDist) < MIN_DIST)
			{
				movementDir.y = 0;
			}

		}
		if (movementDir.y == 0 ) {

			if(Mathf.Abs(xDist) < MIN_DIST)
			{
				if(Mathf.Abs(yDist) > MIN_DIST)
				{
					movementDir = (yDist > 0 ? new Vector2 (0, 1) : new Vector2 (0, -1));
				}
			}
			return;
		}
		if (movementDir.x == 0) {

			if(Mathf.Abs(yDist) < MIN_DIST)
			{
				if(Mathf.Abs(xDist)> MIN_DIST)
				{
					movementDir = (xDist > 0 ? new Vector2 (1, 0) : new Vector2 (-1, 0));
				}
			}
			return;
		}
	}

    // Gets the direction when a collision is detected
	void GetCollisionMoveDir(bool close)
	{
        if(Time.time - lastDirectionChangeTime < minDirectionChangeTime)
        {
			currentMovementSpeed = currentMovementSpeed/2;
			lookAhead = currentMovementSpeed * lookAheadAmount;
            return;
        }
        lastDirectionChangeTime = Time.time;

        float xDist = destPos.x - transform.position.x;
		float yDist = destPos.y - transform.position.y;
        float xDir = 0;
        float yDir = 0;
		if (movementDir.x == 0 && movementDir.y != 0) {
            xDir = (xDist > 0 ? 1 : -1);
            yDir = (close ? 0 : movementDir.y);
		}
		else if (movementDir.x != 0 && movementDir.y == 0) {
            yDir = (yDist > 0 ? 1 : -1);
            xDir = (close ? 0 : movementDir.x);
        }
        else if (movementDir.x != 0 && movementDir.y != 0)
        {
            if(Mathf.Abs(xDist) >= Mathf.Abs(yDist))
            {
                xDir = (xDist > 0 ? 1 : -1);
            }
            else
            {
                yDir = (yDist > 0 ? 1 : -1);
            }
			movementDir = new Vector2(xDir,yDir);
			if(!movementCheck.IsPositionAheadClear(movementDir,lookAhead/2))
			{
				if(Mathf.Abs(xDist) <= Mathf.Abs(yDist))
				{
					xDir = (xDist > 0 ? 1 : -1);
					yDir = 0;
				}
				else
				{
					yDir = (yDist > 0 ? 1 : -1);
					xDir = 0;
				}
			}
        }
        movementDir = new Vector2(xDir, yDir);
		if (!movementCheck.IsPositionAheadClear (movementDir, currentMovementSpeed*Time.deltaTime)) 
		{
			if(!close)
			{
				GetCollisionMoveDir(true);
				return;
			}

			movementDir *= -1;
			if (!movementCheck.IsPositionAheadClear (movementDir, currentMovementSpeed*Time.deltaTime)) 
			{
				lastRefreshTime = Time.time + Random.Range(-randomness, randomness);
				GetPath();
				GetStartingDir();
			}
		}
    }

	
    bool MoveTowardsPlayerIfPossible()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position,destObj.GetTransform().position, mask);
        
        if(hit.collider == null)
        {
            Debug.DrawLine(transform.position, destObj.GetTransform().position, Color.white);
            destPos = destObj.GetTransform().position;
            GetStartingDir();
            return true;
        }
        else
        {
            Debug.DrawLine(transform.position, destObj.GetTransform().position, Color.black);
            return false;
        }
    }

    void CheckForCollision()
	{
		if (!movementCheck.IsPositionAheadClear(movementDir, lookAhead)) {
            GetCollisionMoveDir(!movementCheck.IsPositionAheadClear(movementDir, currentMovementSpeed/2));
		}
	}
}

using UnityEngine;
using System.Collections;

public class MovementCheck : MonoBehaviour {
	public LayerMask layerMask;
	public Vector2 size;
    RaycastHit2D[] results = new RaycastHit2D[1];
    RaycastHit2D result;
    [HideInInspector]
	public RaycastHit2D hit2d;

    public bool CheckMove(Vector3 pos)
	{
		Physics2D.BoxCastNonAlloc (pos, size, 0, new Vector2(0,0),results, 0, layerMask);
		if (results[0].collider != null) {
            result = results[0];
            results = new RaycastHit2D[1];
            
            return false;
        }
		return true;
	}

	public bool CheckMoveIgnoreSelf(Vector3 pos)
	{
		RaycastHit2D hit = Physics2D.BoxCast (pos, size, 0, new Vector2(0,0), 0, layerMask);
		if (hit.collider != null) {
			return false;
		}
		return true;
	}

	public GameObject HitObject(Vector3 pos)
	{
        return result.collider.gameObject;
		RaycastHit2D hit = Physics2D.BoxCast (pos, size, 0, new Vector2(0,0), 0, layerMask);
		hit2d = hit;
        Vector2 half = size * .5f;

        Vector2 start = new Vector2(pos.x - half.x, pos.y - half.y);
        Vector2 end = new Vector2(pos.x + half.x, pos.y + half.y);

		if (hit.collider != null) {
			return hit.collider.gameObject;
		}
		return null;
	}


	

}

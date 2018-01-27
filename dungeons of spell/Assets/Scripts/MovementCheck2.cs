using UnityEngine;
using System.Collections;
[RequireComponent(typeof(BoxCollider2D))]
public class MovementCheck2 : MonoBehaviour {
    [HideInInspector]
    public Collider2D hitCollider;
    public LayerMask mask;
    BoxCollider2D boxCollider;
	void Awake ()
    {
        boxCollider = GetComponent<BoxCollider2D>();
	}

    public bool IsPositionClear(Vector2 pos)
    {
        int layer = gameObject.layer;
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        RaycastHit2D hit = Physics2D.BoxCast(pos + boxCollider.offset, new Vector2(boxCollider.size.x * transform.localScale.x * .9f, boxCollider.size.y * transform.localScale.y * .9f), 0, Vector2.zero, 0, mask);
        Debug.DrawLine(pos, pos + new Vector2(boxCollider.size.x * transform.localScale.x * .9f, boxCollider.size.y * transform.localScale.y * .9f), Color.blue);
        if (hit.collider != null)
        {
            gameObject.layer = layer;
            hitCollider = hit.collider;
            return false;
        }
        gameObject.layer = layer;
        return true;
    }

    public bool IsPositionAheadClear(Vector2 movementDir, float lookAhead)
    {
        float sizeScale = .5f;
        int layer = gameObject.layer;
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(boxCollider.size.x * transform.localScale.x * sizeScale, boxCollider.size.y * transform.localScale.y * sizeScale), 0, movementDir, lookAhead, mask);
        //ExtDebug.DrawBoxCastBox(transform.position, new Vector2(boxCollider.size.x * transform.localScale.x * sizeScale, boxCollider.size.y * transform.localScale.y * sizeScale), Quaternion.identity, movementDir, lookAhead, Color.green, .1f);
        if (hit.collider != null)
        {
            gameObject.layer = layer;
            hitCollider = hit.collider;
            Debug.DrawRay((Vector2)transform.position - (Vector2)new Vector2(boxCollider.size.x * transform.localScale.x * sizeScale, boxCollider.size.y * transform.localScale.y * sizeScale), 2 * new Vector2(boxCollider.size.x * transform.localScale.x * .9f, boxCollider.size.y * transform.localScale.y * .9f), Color.red, 1);
            //Debug.DrawLine(transform.position, hit.collider.gameObject.transform.position, Color.red, 1);
            ExtDebug.DrawBoxCastBox(transform.position, new Vector2(boxCollider.size.x * transform.localScale.x * sizeScale, boxCollider.size.y * transform.localScale.y * sizeScale), Quaternion.identity, movementDir, lookAhead, Color.red, 1);
            Debug.DrawLine(transform.position, hit.collider.gameObject.transform.position, Color.red, 1);
            return false;
        }
        ExtDebug.DrawBoxCastBox(transform.position, new Vector2(boxCollider.size.x * transform.localScale.x * sizeScale, boxCollider.size.y * transform.localScale.y * sizeScale), Quaternion.identity, movementDir, lookAhead, Color.green, .1f);
        gameObject.layer = layer;
        return true;
    }
}

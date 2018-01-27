using UnityEngine;
using System.Collections;
[RequireComponent(typeof(SpriteRenderer))]
public class YSort : MonoBehaviour {
    SpriteRenderer spriteRenderer;

    public bool updateOrder = false;
    public float yOffset = 0;

    /// ----------------------------------------------------------
	void Start () {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        UpdatePosition();
    }

    /// ----------------------------------------------------------
    void Update () {
        if(updateOrder)
        {
            UpdatePosition();
        }
    }

    /// ----------------------------------------------------------s
    void UpdatePosition()
    {
        int pos = Mathf.RoundToInt(transform.position.y - yOffset);
        pos /= 2;
        spriteRenderer.sortingOrder = (pos * -1);
    }
}

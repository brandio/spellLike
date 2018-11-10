using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CircleCollider2D))]
public class InRangeActive : MonoBehaviour {

	protected bool playerInside = false;
	protected GameObject player;
    Material mat;
    public SpriteRenderer sRenderer;
    public float offSet = 0;
    float outlineMax = 1.08f;
    CircleCollider2D colliderCircle;
    protected Color outLineColor = new Color(0, 1, 1);
    void Awake()
    {
        colliderCircle = this.GetComponent<CircleCollider2D>();
        if (sRenderer == null)
            sRenderer = this.GetComponent<SpriteRenderer>();
        if(sRenderer != null)
        {
            mat = sRenderer.material;
        }
    }

    //IEnumerator MakeOutline()
    //{
    //    float outline = 1.0f;
    //    while (playerInside && outline < 1.05f)
    //    {
    //        outline += 0.001f;
    //        yield return new WaitForSeconds(0.01f);
    //        mat.SetFloat("_Outline", outline);

    //    }
    //    if (!playerInside)
    //    {
    //        mat.SetFloat("_Outline", 1);

    //    }
    //}

    float Normalize(float input)
    {
        float range = 1;
        float a = (input - 0) / range;
        float range2 = outlineMax - 1;
        a = (a * range2) + 1;
        return a;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            SetOutline(other);
    }

    protected void SetOutline(Collider2D other)
    {
        float distanceFromCenter = Vector2.Distance(this.transform.position, other.transform.position) - offSet;
        float percentOffFull = distanceFromCenter / colliderCircle.radius;
        percentOffFull = 1 + (1 - distanceFromCenter);
        percentOffFull = Normalize(percentOffFull);
        if (percentOffFull < 1)
            return;
        Debug.Log(percentOffFull);
        if(mat != null)
        {
            mat.SetFloat("_Outline", percentOffFull);
            mat.SetColor("_OutlineColor", outLineColor);
        }

    }
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
            playerInside = true;
            player = other.gameObject;
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
            if (mat != null)
                mat.SetFloat("_Outline", 1.00f);
            playerInside = false;
		}
	}

}

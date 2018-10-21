using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CircleCollider2D))]
public class InRangeActive : MonoBehaviour {

	protected bool playerInside = false;
	protected GameObject player;
    public Material mat;

    void Awake()
    {
        SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();
        if(renderer != null)
        {
            mat = renderer.material;
        }
    }

    IEnumerator MakeOutline()
    {
        float outline = 1.0f;
        while(playerInside && outline < 1.05f)
        {
            outline += 0.001f;
            yield return new WaitForSeconds(0.01f);
            mat.SetFloat("_Outline", outline);

        }
        if(!playerInside)
        {
            mat.SetFloat("_Outline", 1);

        }
    }
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
            playerInside = true;

            if (mat != null)
                StartCoroutine("MakeOutline");
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

using UnityEngine;
using System.Collections;

public class ScaleTexture : MonoBehaviour {
    const float scale = 6;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Renderer>().material.mainTextureScale = new Vector2(transform.localScale.x * scale, transform.localScale.z * scale);
        gameObject.GetComponent<Renderer>().material.SetFloat("_Scale", transform.localScale.x * scale);
    }

}

using UnityEngine;
using System.Collections;

public class ScaleTexture : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Renderer>().material.mainTextureScale = new Vector2(transform.localScale.x * 2, transform.localScale.z * 2);
    }

}

using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {
	public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (this.transform.position.x + Time.deltaTime * speed, this.transform.position.y + Time.deltaTime * speed * .2f, 0);
	}
}

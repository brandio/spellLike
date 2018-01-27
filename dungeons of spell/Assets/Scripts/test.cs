    using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.V))
		 {
			Application.LoadLevel(0);
			//Application.LoadLevel(Application.loadedLevel);
		}
	}
}

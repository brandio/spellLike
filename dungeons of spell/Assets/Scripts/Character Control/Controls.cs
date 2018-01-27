using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {
    public GameObject player;
    public GameObject cameraObj;
    public GameObject hud;
	static Controls instance;
	public bool active = true;
	public static Controls GetInstance()
	{
		return instance;
	}


	// Use this for initialization
	void Start () {
		if (instance != null) {
			Debug.Log ("SINGLETOOOOOON");
		} else {
			instance = this;
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}

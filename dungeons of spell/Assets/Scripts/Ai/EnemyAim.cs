using UnityEngine;
using System.Collections;

public class EnemyAim : MonoBehaviour {

	Camera cam;
	Transform player;
    
    
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").transform;
		GameObject camera = GameObject.Find ("Main Camera");
		cam = camera.GetComponent<Camera> ();
	}
	
	void Aim()
	{
		Vector3 playerPos = player.position;
		Vector2 direction = playerPos - this.transform.position;
		
		float angle = Mathf.Atan (direction.y / direction.x) * Mathf.Rad2Deg;
		if (direction.x < 0) {
			angle = angle + 180;
		}
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}
	
	// Update is called once per frame
	void Update () {
		Aim ();
	}
}

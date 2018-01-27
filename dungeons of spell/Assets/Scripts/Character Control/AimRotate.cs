using UnityEngine;
using System.Collections;

public class AimRotate : MonoBehaviour {

	Camera cam;
	// Use this for initialization
	void Start () {
		GameObject camera = GameObject.Find ("Main Camera");
		cam = camera.GetComponent<Camera> ();
	}
	
	void RayCastToMouse()
	{
		Vector3 mouse = Input.mousePosition;
		Vector3 world = cam.ScreenToWorldPoint(mouse);
		Vector2 direction = world - this.transform.position;
		
		float angle = Mathf.Atan (direction.y / direction.x) * Mathf.Rad2Deg;
		if (direction.x < 0) {
			angle = angle + 180;
		}
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		//print (Vector2.Angle (this.transform.position, world) + " " + Vector2.Angle ( world, this.transform.position)+ " " + angle);
	}
	
	// Update is called once per frame
	void Update () {
		RayCastToMouse ();
	}
}

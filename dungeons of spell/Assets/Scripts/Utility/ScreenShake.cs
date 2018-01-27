using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {
	//Vector3 originalCameraPosition;
	float  yMove;
	float shakeAmt = 0;
	
	public Camera mainCamera;

	public static ScreenShake instance;

	void Start()
	{
		instance = this;
	}

	public void shake(float amt)
	{
		yMove = 0;
		//originalCameraPosition = mainCamera.transform.position;
		shakeAmt = amt;
		InvokeRepeating("CameraShake", 0, .15f);
		Invoke("StopShaking", 0.3f);
	}

	void OnCollisionEnter2D(Collision2D coll) 
	{
		
		shakeAmt = coll.relativeVelocity.magnitude * .0025f;
		InvokeRepeating("CameraShake", 0, .01f);
		Invoke("StopShaking", 0.3f);
		
	}
	
	void CameraShake()
	{
		if(shakeAmt>0) 
		{
			float quakeAmt = Random.value*shakeAmt*2 - shakeAmt;
			Vector3 pp = mainCamera.transform.position;
			yMove = yMove + quakeAmt;
			pp.y+= quakeAmt; // can also add to x and/or z
			mainCamera.transform.position = pp;
		}
	}
	
	void StopShaking()
	{
		CancelInvoke("CameraShake");
		mainCamera.transform.position = mainCamera.transform.position - new Vector3(0,yMove,0);
	}
}

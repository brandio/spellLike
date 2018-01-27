using UnityEngine;
using System.Collections;

public class ProjectileMovement : MonoBehaviour {
	public float speed;
    public float turnSpeed;
	[HideInInspector]
	public float tempSpeed = 1;
	[HideInInspector]
	public bool rotateNow = true;
	IEnumerator rotate(float destAngle)
	{
		rotateNow = true;
		yield return null;
		float amt = 0;
		float startAngle = gameObject.transform.eulerAngles.z;
		destAngle = startAngle + destAngle;
		float dist = Mathf.Abs(destAngle - startAngle);

		float total = 0;
		while (total < 1 && rotateNow) {
			float roat = Mathf.Lerp(startAngle,destAngle,total);
			this.transform.eulerAngles = new Vector3(0,0,roat);
			amt = amt + Time.deltaTime * turnSpeed;
			total = amt/dist;
			yield return null;
		}
		//print ("done rotating");
	}

	public void turn(float amt)
	{
		StartCoroutine (rotate(amt));
	}

	void OnDisable  () {
		tempSpeed = 1;
	}

    void Move()
    {
        if(speed == 0)
        {
            return;
        }
		this.transform.position = this.transform.position + transform.right * Time.deltaTime * speed * tempSpeed;
	}


	// Update is called once per frame
	void Update () {
		Move ();
	}
}

using UnityEngine;
using System.Collections;

public class RandomRotation : MonoBehaviour {
    public float min;
    public float max;
	// Update is called once per frame
	void Awake () {
        transform.eulerAngles = new Vector3(0, 0, Random.Range(min, max));	
	}
}

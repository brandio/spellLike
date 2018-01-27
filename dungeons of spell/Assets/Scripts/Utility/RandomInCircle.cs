using UnityEngine;
using System.Collections;

public class RandomInCircle : MonoBehaviour {
    public float diameter;
	// Use this for initialization
	void Start () {
        this.transform.localPosition = Random.insideUnitCircle * diameter;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

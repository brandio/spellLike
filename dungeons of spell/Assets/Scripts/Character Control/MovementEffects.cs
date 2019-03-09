using UnityEngine;
using System.Collections;

public class MovementEffects : MonoBehaviour {
    public AudioSource moveSound;
    public float speed = 3;
    float lastTime = 0;

    void MakeSound()
    {
        if(lastTime + speed > Time.time)
        {
            moveSound.Play();
            lastTime = Time.time;
        }
    }
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

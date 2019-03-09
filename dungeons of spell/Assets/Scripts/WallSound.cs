using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class WallSound : MonoBehaviour {
    AudioSource source;
    float time;
	// Use this for initialization
	void Start () {
        time = Time.time;
        source = this.GetComponent<AudioSource>();
    }
	
    public void Play()
    {
        if(Time.time > time + .4f)
        {
            time = Time.time;
            source.Play();
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}

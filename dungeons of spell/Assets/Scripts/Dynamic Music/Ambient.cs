using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Ambient : MonoBehaviour {
    //public List<AudioClip> clipsInUse;
    public int clipsInUse;
    public List<AudioClip> clipsNotInUse;

    public List<AudioSource> sourcesInUse;
    public List<AudioSource> sourcesNotInUse;

    public int maxTracks;
    public int addRemoveTimeAverage = 90;
    public int addRemoveVariance = 30;
    public int averageTracks = 2;

    float nextTime = 0;
    public float maxVolum = 1;
    IEnumerator FadeIn(AudioSource source)
    {
        source.volume = 0;
        while (source.volume < maxVolum )
        {
            yield return new WaitForSeconds(.1f);
            source.volume += .01f;
        }
    }

    public void StartTrack()
    {
        if(clipsInUse > maxTracks || clipsNotInUse.Count == 0 || sourcesNotInUse.Count == 0)
        {
            return;
        }
        int index = Random.Range(0, clipsNotInUse.Count);
        AudioClip clipPicked = clipsNotInUse[index];
        clipsNotInUse.RemoveAt(index);

        AudioSource source = sourcesNotInUse[sourcesNotInUse.Count - 1];
        sourcesNotInUse.RemoveAt(sourcesNotInUse.Count - 1);

        sourcesInUse.Add(source);
        source.clip = clipPicked;
        StartCoroutine(FadeIn(source));
        clipsInUse++;
    }

    public void StopTrack()
    {
        if(clipsInUse == 0)
        {
            return;
        }
        else
        {
            clipsInUse--;
        }
    }

    float GetNextTime()
    {
        float sign = Random.Range(0, 100) > 50 ? 1 : -1;
        return Time.time + Random.Range(0, addRemoveVariance) * sign + addRemoveTimeAverage;
    }
	// Use this for initialization
	void Start ()
    {
	    for(int i = 0; i < averageTracks; i++)
        {
            StartTrack();
        }
        nextTime = GetNextTime();
        int index = Random.Range(0, sourcesInUse.Count);
        clipsNotInUse.Add(sourcesInUse[index].clip);
        sourcesInUse.RemoveAt(index);
        clipsInUse--;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(Time.time > nextTime)
        {
            int max = clipsInUse > averageTracks ? 120 : 80;
            if(clipsInUse == averageTracks)
            {
                max = 100;
            }
            if (Random.Range(0, max) > 50)
            {
                StopTrack();
            }
            else
            {
                StartTrack();
            }
            nextTime = GetNextTime();
        }
	}
}

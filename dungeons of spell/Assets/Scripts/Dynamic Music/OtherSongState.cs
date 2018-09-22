using UnityEngine;
using System.Collections;

public class OtherSongState : SongState {

    public AnimationCurve numberOfAddTracksCurve;

    int GetNumberOfTracksToAdd()
    {
        float baseNumber = Random.Range(0, 10);
        return (int)Mathf.Round(numberOfAddTracksCurve.Evaluate(baseNumber / 10));
    }
    
    public override void InitState()
    {
        int numberOfTracksToAdd = GetNumberOfTracksToAdd();
        Debug.Log(numberOfTracksToAdd);
        for (int i = 0; i < numberOfTracksToAdd; i++)
        {
            Song.track track = GetRandomFreeTrack();
            if(track == Song.track.Empty)
            {
                return;
            }
            AudioClip clip = GetClip(track);
            song.StartTrack(clip, track);
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

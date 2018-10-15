using UnityEngine;
using System.Collections;

public class OutOfBattleState : SongState {

    bool inited = false;
	// Use this for initialization
	void Start () {
	
	}

    public override void UpdateSongState()
    {
        if(!inited)
        {
            InitState();
        }
    }

    public override void InitState()
    {
        if(!inited)
        {
            inited = !inited;
            for (int i = 0; i < 2; i++)
            {
                Song.track track = GetRandomFreeTrack();
                if (track == Song.track.Empty)
                {
                    return;
                }
                AudioClip clip = GetClip(track);
                usedTracks.Add(track);
                song.StartTrack(clip, track);
            }
        }
    }

    public override void LeaveState()
    {

    }

    // Update is called once per frame
    void Update ()
    {
	
	}
}

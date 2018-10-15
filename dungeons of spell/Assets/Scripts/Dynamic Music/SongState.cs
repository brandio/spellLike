using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

[RequireComponent(typeof(Song))]
public abstract class SongState : MonoBehaviour {
    public List<AudioClip> drums;
    public List<AudioClip> percs;
    public List<AudioClip> melodies;
    public List<AudioClip> leads;
    public List<AudioClip> basses;
    public List<AudioClip> ambient;
    Dictionary<Song.track, List<AudioClip>> trackToClipList = new Dictionary<Song.track, List<AudioClip>>();
    HashSet<Song.track> tracks = new HashSet<Song.track>();
    protected List<Song.track> usedTracks = new List<Song.track>();
    protected Song song;

    void Awake()
    {
        song = this.GetComponent<Song>();
        trackToClipList.Add(Song.track.Drums,drums);
        trackToClipList.Add(Song.track.Percs,percs);
        trackToClipList.Add(Song.track.Melody,melodies);
        trackToClipList.Add(Song.track.Lead,leads);
        trackToClipList.Add(Song.track.Bass,basses);
        trackToClipList.Add(Song.track.Ambient, ambient);

        foreach (KeyValuePair<Song.track, List<AudioClip>> entry in trackToClipList)
        {
            if(entry.Value.Count > 0)
            {
                tracks.Add(entry.Key);
            }
        }
    }

    protected Song.track GetRandomFreeTrack()
    {

        System.Collections.Generic.IEnumerable<Song.track> freeTracksE = tracks.Union(song.GetFreeTracks());
        List<Song.track> freeTracks = freeTracksE.ToList();
        if (freeTracks.Count == 0)
        {
            return Song.track.Empty;
        }
        return freeTracks[Random.Range(0,freeTracks.Count)];
    }

    protected AudioClip GetClip(Song.track track)
    {
        if(trackToClipList.ContainsKey(track))
        {
            List<AudioClip> clips = trackToClipList[track];
            if(clips.Count > 0)
            {
                return clips[Random.Range(0, clips.Count)];
            }
        }
        return null;
    }

    public abstract void InitState();
    public abstract void LeaveState();

    void ExitState()
    {

    }

    public abstract void UpdateSongState();
}

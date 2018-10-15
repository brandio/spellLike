using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Song : MonoBehaviour, IRoomChangeListener
{
    public PlayerRoomManager roomManager;
    public enum track { Drums, Percs, Melody, Lead, Bass, Ambient, Empty };
    public HashSet<track> freeTracks = new HashSet<track>() { track.Drums, track.Percs, track.Melody, track.Lead, track.Bass, track.Ambient };
    public HashSet<track> usedTracks = new HashSet<track>();

    public AudioSource drums;
    public AudioSource percs;
    public AudioSource melody;
    public AudioSource lead;
    public AudioSource bass;
    public AudioSource background;

    public List<AudioSource> ambient;
    public Dictionary<track, List<AudioSource>> trackToAudioSourceMap = new Dictionary<track, List<AudioSource>>();

    [HideInInspector]
    public bool lowHp = false;

    public SongState battle;
    public SongState auxRoom;
    public SongState defaults;

    SongState currentState;

    IEnumerator FadeIn(AudioSource source)
    {
        source.volume = 0;
        while (source.volume < .9)
        {
            yield return new WaitForSeconds(.1f);
            source.volume += .01f;
        }
    }
    public void RoomClearedEventListener(Room r)
    {
        ChangeState(defaults);
    }

    public void RoomChanged(Room room)
    {
        if(room.enemies.Count > 0)
        {
            ChangeState(battle);
            room.RoomCleared += RoomClearedEventListener;
        }
    }
    
    void ChangeState(SongState state)
    {
        Debug.Log("Changing state");
        if(state == currentState)
        {
            return;
        }
        currentState.LeaveState();
        currentState = state;
        state.InitState();
    }
    // Use this for initialization
    void Start()
    {
        roomManager = PlayerRoomManager.instance;
        roomManager.Register(this);
        trackToAudioSourceMap.Add(track.Drums,new List<AudioSource>(){ drums });
        trackToAudioSourceMap.Add(track.Percs, new List<AudioSource>() { percs });
        trackToAudioSourceMap.Add(track.Melody, new List<AudioSource>() { melody });
        trackToAudioSourceMap.Add(track.Lead, new List<AudioSource>() { lead });
        trackToAudioSourceMap.Add(track.Bass, new List<AudioSource>() { bass });
        trackToAudioSourceMap.Add(track.Ambient, ambient);
        background.Play();
        currentState = defaults;
    }


    public void StartTrack(AudioClip clip, track track)
    {
        Debug.Log("Starting track");
        if(track == track.Ambient)
        {
            return;
        }

        if (freeTracks.Contains(track))
        {
            freeTracks.Remove(track);
            usedTracks.Add(track);
            AudioSource source = trackToAudioSourceMap[track][0];
            source.clip = clip;
            source.Play();
            source.time = background.time;
            StartCoroutine(FadeIn(source));
        }
    }

    public void StopTrack(track track)
    {
        if(track == track.Ambient)
        {
            return;
        }

        if(usedTracks.Contains(track))
        {
            usedTracks.Remove(track);
            freeTracks.Add(track);
            trackToAudioSourceMap[track][0].Stop();
        }
    }

    public HashSet<Song.track> GetFreeTracks()
    {
        return freeTracks;
    }

    void Update()
    {
        currentState.UpdateSongState();
    }
}

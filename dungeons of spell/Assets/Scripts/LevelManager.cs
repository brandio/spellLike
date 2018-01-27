using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
    public DungeonRoomMaker roomMaker;
    public GameObject player;
    public GameObject loadingCamera;
    public GameObject gameCamera;
    void Awake()
    {
        roomMaker.DungeonDone += SetLevel;
    }

    void SetLevel()
    {
        player.SetActive(true);
        gameCamera.SetActive(true);
        loadingCamera.SetActive(false);
    }
}

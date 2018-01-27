using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreen : MonoBehaviour {
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    public Text loadingText;
    bool foundRoomMaker = false;
    DungeonRoomMaker roomMaker;
    GameObject player;
    GameObject gameCamera;
    GameObject hud;
    public GameObject loadingCamera;
    public string[] loadingTextStrings;
    bool loading = true;

    void Start()
    {
        StartCoroutine("changeText");
    }
    IEnumerator changeText()
    {
        while(loading)
        {
            yield return new WaitForSeconds(2);
            loadingText.text = loadingTextStrings[Random.Range(0, loadingTextStrings.GetLength(0))];
            
        }
            
    }

    void Update()
    {
        if(!foundRoomMaker)
        {
            GameObject roomMakerObject = GameObject.FindGameObjectWithTag("Maze");
            if (roomMakerObject != null)
            {
                roomMaker = GameObject.FindGameObjectWithTag("Maze").GetComponent<DungeonRoomMaker>();
                foundRoomMaker = true;
                roomMaker.DungeonDone += SetLevel;
                Controls controls = Controls.GetInstance();
                player = controls.player;
                gameCamera = controls.cameraObj;
                hud = controls.hud;
            }
        }
    }


    void SetLevel()
    {
        hud.SetActive(true);
        player.SetActive(true);
        gameCamera.SetActive(true);
        loading = false;
        loadingCamera.SetActive(false);
    }
}

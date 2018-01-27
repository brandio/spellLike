using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Intro : MonoBehaviour {
    public AudioSource source;
    public AudioSource source2;

    AsyncOperation async;
    public GameObject loadingScreen;
    void Start()
    {
        StartCoroutine("load");
    }
    void Update()
    {
        if(Input.anyKey)
        {
            //source.Play();
            //SceneManager.LoadScene(1);
            //ActivateScene();
        }
    }

    IEnumerator load()
    {
        async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;
        yield return async;
    }

    public void ActivateScene()
    {

        async.allowSceneActivation = true;
        loadingScreen.SetActive(true);

    }
}

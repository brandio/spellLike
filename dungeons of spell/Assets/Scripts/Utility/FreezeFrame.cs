using UnityEngine;
using System.Collections;

public class FreezeFrame : MonoBehaviour {
    public Camera mainCamera;

    public static FreezeFrame instance;

    void Start()
    {
        instance = this;
    }

    public void FreezeFrames(int amt)
    {
        StartCoroutine(Freeze(amt));
    }
    
    IEnumerator Freeze(float frames)
    {
        Camera.main.clearFlags = CameraClearFlags.Nothing;
        Camera.main.cullingMask = 0;
        for (int i = 0; i < frames; i++)
        {
            yield return null;
        }
        Camera.main.cullingMask = -1;
        Camera.main.clearFlags = CameraClearFlags.Skybox;
    }
}

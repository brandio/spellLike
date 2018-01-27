using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SimpleBlit : MonoBehaviour
{
    public Material TransitionMaterial;
    float counter = 0;
    float speed = .44f;
    public Intro intro;

    void Update()
    {
        if (Input.anyKey)
        {
            StartCoroutine("Transition");
        }
    }
    public IEnumerator Transition()
    {
        while(counter < 1)
        {
            TransitionMaterial.SetFloat("_Cutoff", counter);
            counter += speed * Time.deltaTime;
            yield return null;
        }
        TransitionMaterial.SetFloat("_Cutoff", 0);
        intro.ActivateScene();
    }
    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (TransitionMaterial != null)
            Graphics.Blit(src, dst, TransitionMaterial);
    }
}

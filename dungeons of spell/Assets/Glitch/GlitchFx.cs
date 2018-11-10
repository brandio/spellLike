using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GlitchFx : MonoBehaviour
{
    public static GlitchFx instance;
    public float glitchAmount = 0.0f;
    public float shiftMag = 0.0f;
    public Texture2D blockTexture;

    private Shader _glitchShader;
    private Material _glitchMat;
    private float _realGlitch;

    float glitchAmountTemp = 0.0f;
    float shiftMagTemp = 0.0f;
    public void SetTemp(float shiftAmt, float glitchAmount)
    {
        glitchAmountTemp = glitchAmount;
        shiftMagTemp = shiftAmt;
    }

    public void Reset()
    {
        glitchAmountTemp = glitchAmount;
        shiftMagTemp = shiftMag;
    }
    void Start()
    {
        instance = this;
        _glitchShader = Shader.Find("Hidden/GlitchFX/GlitchFX_Shift");
        _glitchMat = new Material(_glitchShader);
        _glitchMat.SetTexture("_GlitchMap", blockTexture);

        Reset();
        Invoke("UpdateRandom", 0.25f);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, _glitchMat);
    }

    void UpdateRandom()
    {
        _glitchMat.SetFloat("_GlitchRandom", Random.Range(-1.0f, 1.0f));
        Invoke("UpdateRandom", Random.Range(0.01f, 0.15f));

    }
    // Update is called once per frame
    void Update()
    {
        glitchAmount = Mathf.Clamp(glitchAmount, 0.0f, 1.0f);

        _glitchMat.SetFloat("_ShiftMag", shiftMagTemp);
        _glitchMat.SetFloat("_GlitchAmount", glitchAmountTemp);
    }
}
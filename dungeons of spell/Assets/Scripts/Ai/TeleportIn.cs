using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class TeleportIn : MonoBehaviour {
    public List<Behaviour> componentsToTeleportIn = new List<Behaviour>();
    public float minTime = 0;
    public float maxTime = 1;
    Animator anim;
    SpriteRenderer render;
    public bool enableTheThings = false;
    public AudioSource audio;
    public float amount;
    void Awake()
    {
        foreach(Behaviour comp in componentsToTeleportIn)
        {
            comp.enabled = false;
        }
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        render.enabled = false;
    }
	
	void Start () {
        render.material.SetFloat("_OffsetBlueX", -1);
        render.material.SetFloat("_OffsetBlueY", 1);
        render.material.SetFloat("_OffsetRedX", -1);
        render.material.SetFloat("_OffsetRedY", -1);
        render.material.SetFloat("_OffsetGreenX", 1);
        StartCoroutine("Teleport");
	}
	
    void EnableStuff()
    {
        foreach (Behaviour comp in componentsToTeleportIn)
        {
            comp.enabled = true;
        }
    }

    void Update()
    {
        if(amount > 0)
        {
            render.material.SetFloat("_OffsetBlueX", -1 * amount);
            render.material.SetFloat("_OffsetBlueY", 1 * amount);
            render.material.SetFloat("_OffsetRedX", -1 * amount);
            render.material.SetFloat("_OffsetRedY", -1 * amount);
            render.material.SetFloat("_OffsetGreenX", 1 * amount);
        }

        if (enableTheThings)
        {
            EnableStuff();
            render.material.SetFloat("_OffsetBlueX", 0);
            render.material.SetFloat("_OffsetBlueY", 0);
            render.material.SetFloat("_OffsetRedX", 0);
            render.material.SetFloat("_OffsetRedY", 0);
            StopTeleport();
            render.material.SetFloat("_OffsetGreenX",0);
            this.enabled = false;
        }

    }

    public void StopTeleport()
    {
        anim.SetBool("TeleportIn", false);
    }

    IEnumerator Teleport()
    {
        float time = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(time);
        render.enabled = true;
        anim.SetBool("TeleportIn", true);
        audio.Play();
    }
}

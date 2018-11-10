using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(MirrorImageState))]
public class BossScriptedEvent : IScriptedEvent
{
    public StatePatternEnemy statePatternEnemy;
    public override void StartEvent()
    {
        Controls.GetInstance().active = false;
        ScreenShake.instance.shake(0.5f);
        GlitchFx.instance.SetTemp(5, .8f);
        MirrorImageState mirrorImageState = this.GetComponent<MirrorImageState>();
        mirrorImageState.statePatternEnemy = statePatternEnemy;
        statePatternEnemy.ChangeState(mirrorImageState);
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;
using System;

public class BossScriptedEvent : IScriptedEvent
{
    public StatePatternRedMan statePatternEnemy;
    public override void StartEvent()
    {
        Controls.GetInstance().active = false;
        ScreenShake.instance.shake(0.5f);
        GlitchFx.instance.SetTemp(5, .8f);
        statePatternEnemy.ChangeState(new MirrorImageState(statePatternEnemy));
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

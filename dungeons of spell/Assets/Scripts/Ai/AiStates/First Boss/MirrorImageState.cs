using UnityEngine;
using System.Collections;

public class MirrorImageState : IEnemyState
{

    public StatePatternRedMan statePatternEnemy;
    Room room;
    double time = 0;
    bool timeSet = false;

    public MirrorImageState(StatePatternEnemy spe)
    {
        statePatternEnemy = (StatePatternRedMan)spe;
    }

    public void EnterState()
    {
        room = statePatternEnemy.gameObject.GetComponent<AiMovement>().room;
        time = Time.time;
        MakeImages();
        timeSet = true;
    }


    void MakeImages()
    {
        statePatternEnemy.SetLastHealth();
        GlitchFx.instance.SetTemp(10, 10);
        for(int i = 0; i < statePatternEnemy.numberMirrorImages; i++)
        {
            GameObject mirrorImage = GameObject.Instantiate(statePatternEnemy.mirrorImages);
            Vector2 circle = Random.insideUnitCircle * 10;
            mirrorImage.transform.position = new Vector3(statePatternEnemy.transform.position.x + circle.x, statePatternEnemy.transform.position.y + circle.y);
            mirrorImage.GetComponent<AiMovement>().room = room;
        }
        Vector2 circle2 = Random.insideUnitCircle * 10;
        statePatternEnemy.transform.position = new Vector3(statePatternEnemy.transform.position.x + circle2.x, statePatternEnemy.transform.position.y + circle2.y);
    }
    public void ExitState()
    {

    }

    public void UpdateState()
    {
        if(Time.time - time >= statePatternEnemy.delayTime && timeSet)
        {
            statePatternEnemy.ChangeState(new RedManChaseState(statePatternEnemy));
            GlitchFx.instance.Reset();
        }
    }
}

using UnityEngine;
using System.Collections;

public class MirrorImageState : MonoBehaviour, IEnemyState
{
    public GameObject mirrorImages;
    public int numberMirrorImages;
    public float delayTime = 3;
    public StatePatternEnemy statePatternEnemy;
    Room room;
    double time = 0;
    bool timeSet = false;

    public void EnterState()
    {
        room = statePatternEnemy.gameObject.GetComponent<AiMovement>().room;
        time = Time.time;
        MakeImages();
        timeSet = true;
    }


    void MakeImages()
    {
        GlitchFx.instance.SetTemp(10, 10);
        for(int i = 0; i < numberMirrorImages; i++)
        {
            GameObject mirrorImage = GameObject.Instantiate(mirrorImages);
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
        if(Time.time - time >= delayTime && timeSet)
        {
            statePatternEnemy.ChangeState(new RedManChaseState(statePatternEnemy));
            GlitchFx.instance.Reset();
        }
    }
}

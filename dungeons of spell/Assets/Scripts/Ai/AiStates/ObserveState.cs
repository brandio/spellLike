using UnityEngine;
using System.Collections;

public class ObserveState : IEnemyState
{
    StatePatternEnemy statePatternEnemy;

    public ObserveState(StatePatternEnemy spe)
    {
        statePatternEnemy = spe;
        statePatternEnemy.anim.SetBool("Walking", false);
    }

    public void EnterState()
    {

    }

    public void ExitState()
    {

    }

    public void UpdateState()
    {
        Movement player = statePatternEnemy.GetPlayer();
        if (player != null)
        {
            if (Vector2.Distance(statePatternEnemy.transform.position, statePatternEnemy.player.transform.position) < statePatternEnemy.minObserveDistance)
            {
                statePatternEnemy.ChangeState(new ChaseState(statePatternEnemy));
            }
        }

    }
}

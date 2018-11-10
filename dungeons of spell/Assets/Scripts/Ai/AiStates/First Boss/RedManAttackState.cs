using UnityEngine;
using System.Collections;

public class RedManAttackState : IEnemyState
{
    StatePatternEnemy statePatternEnemy;
    int numberOfAttacks = 0;
    public RedManAttackState(StatePatternEnemy spe)
    {
        statePatternEnemy = spe;
        statePatternEnemy.anim.SetBool("Walking", false);
        statePatternEnemy.shoot.ShotFired += CountAttacks;
    }

    void CountAttacks()
    {
        numberOfAttacks++;
        if(numberOfAttacks >= statePatternEnemy.numberOfAttacks)
        {
            statePatternEnemy.ChangeState(new ChaseState(statePatternEnemy));
        }
    }

    public void EnterState()
    {
    }

    public void ExitState()
    {
        statePatternEnemy.lastAttackTime = Time.time;
        statePatternEnemy.shoot.ShotFired -= CountAttacks;
    }

    public void UpdateState()
    {
        statePatternEnemy.shoot.shoot();
    }
}

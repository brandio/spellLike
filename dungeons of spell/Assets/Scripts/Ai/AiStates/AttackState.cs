using UnityEngine;
using System.Collections;

public class AttackState : IEnemyState
{
    StatePatternEnemy statePatternEnemy;
    int numberOfAttacks = 0;
    public AttackState(StatePatternEnemy spe)
    {
        statePatternEnemy = spe;
    }

    void CountAttacks()
    {
        numberOfAttacks++;
        Debug.Log("Attacked");
        if(numberOfAttacks >= statePatternEnemy.numberOfAttacks)
        {
            statePatternEnemy.ChangeState(new ChaseState(statePatternEnemy));
        }
    }

    public void EnterState()
    {
        Debug.Log("Attack");
        statePatternEnemy.shoot.ShotFired += CountAttacks;
        statePatternEnemy.anim.SetTrigger("Attacking");
        Debug.Log(statePatternEnemy.anim.GetCurrentAnimatorStateInfo(0).ToString());

    }

    public void ExitState()
    {
        Debug.Log("Attack exit");
        statePatternEnemy.lastAttackTime = Time.time;
        statePatternEnemy.shoot.ShotFired -= CountAttacks;
    }

    public void UpdateState()
    {
        
        //statePatternEnemy.shoot.shoot();
    }
}

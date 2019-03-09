using UnityEngine;
using System.Collections;

public class ChaseState : IEnemyState {
	StatePatternEnemy statePatternEnemy;

	public ChaseState(StatePatternEnemy spe)
	{
		statePatternEnemy = spe;
    }

	public void EnterState()
	{
        Debug.Log("Chase");
        statePatternEnemy.anim.SetTrigger("Walking");
    }

	public void ExitState()
	{
        Debug.Log("Chase exit");
    }

    public void UpdateState()
	{
        Movement player = statePatternEnemy.GetPlayer();
        if(player != null)
        {
            statePatternEnemy.movement.MoveTowardsObject(player);
            if (Vector2.Distance(statePatternEnemy.transform.position, statePatternEnemy.player.transform.position) < statePatternEnemy.maxAttackDistance && statePatternEnemy.CanSeePlayer() && (Time.time - statePatternEnemy.lastAttackTime > statePatternEnemy.timeBetweenAttacks))
            {
                statePatternEnemy.ChangeState(new AttackState(statePatternEnemy));
            }
            else if ((Time.time - statePatternEnemy.lastAttackTime > 2 * statePatternEnemy.timeBetweenAttacks))
            {
                statePatternEnemy.ChangeState(new AttackState(statePatternEnemy));
            }
        }

    }
}

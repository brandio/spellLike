using UnityEngine;
using System.Collections;
using System;

public class RandomWaitState : MonoBehaviour, IEnemyState {
    public float min;
    public float max;
    
    public StatePatternEnemy spe;
    IEnumerator WaitRandomTime()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(min, max));
        spe.ChangeState(new ChaseState(spe));
    }
    public void EnterState()
    {
        Debug.Log("starting state :-)");
        StartCoroutine("WaitRandomTime");
    }

    public void ExitState()
    {
    }

    public void UpdateState()
    {
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

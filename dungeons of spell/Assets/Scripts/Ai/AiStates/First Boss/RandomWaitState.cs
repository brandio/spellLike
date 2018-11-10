using UnityEngine;
using System.Collections;
using System;

public class RandomWaitState : MonoBehaviour, IEnemyState {
    float min;
    float max;
    
    public StatePatternEnemy spe;
    IEnumerator WaitRandomTime()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(min, max));

    }
    public void EnterState()
    {
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

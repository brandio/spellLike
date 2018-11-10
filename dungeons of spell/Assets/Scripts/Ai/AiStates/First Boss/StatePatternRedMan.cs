using UnityEngine;
using System.Collections;

public class StatePatternRedMan : StatePatternEnemy
{
    public GameObject mirrorImages;
    public int numberMirrorImages;
    public float delayTime = 3;

    public float healthPercentDifference = .15f;
    float lastHealthPercent = 0;
    
    public Health health;
    public void SetLastHealth()
    {
        lastHealthPercent = health.GetPercent();
    }
    public bool ReadyToMirror()
    {
        if(lastHealthPercent - health.GetPercent() >= healthPercentDifference)
        {
            return true;
        }
        return false;
    }
}
